using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoadGenerate : MonoBehaviour
{
    [SerializeField] private ChunksPool _chunksPool;
    [SerializeField] private TraveledDistance _traveledDistance;
    [SerializeField] private Chunk _startChunk;

    [Space]
    [SerializeField] private float _offset = 5f;
    [SerializeField, Min(0)] private int _roadChunksCount = 5;

    private Queue<Chunk> _roadChunksQueue = new Queue<Chunk>();

    private int _oldRoadChunkIndex = 0;
    private int _oldChunkType= 1;
    private int _index;
    private int _chunkType = 1;

    private void Start()
    {
        _roadChunksQueue.Enqueue(_startChunk);

        for(int i = 0;  i < _roadChunksCount; i++)
        {
            Chunk chunk = _chunksPool.GetChunk(_chunkType, true);       

            chunk.transform.position = Vector3.forward * (i + 1) * _offset;
            _roadChunksQueue.Enqueue(chunk);                     
        }
    }

    private void LateUpdate()
    {
        int currentRoadChunkIndex = (int)(_traveledDistance.Distance / _offset);

        if(_oldRoadChunkIndex != currentRoadChunkIndex) 
        {
            Chunk oldChunk = _roadChunksQueue.Dequeue();
            _chunksPool.ReturnChunk(oldChunk);

            Chunk chunk;

            if (_index > 0)
            {
                chunk = _chunksPool.GetChunk(_chunkType, true);
                _index--;
            }
            else
            {
                chunk = _chunksPool.GetChunk(_chunkType, false);
            }

            chunk.TryGetComponent<UpdateChunk>(out UpdateChunk updateChunk);
            if(updateChunk != null)
            {
                updateChunk.UpdateChunkObjects();
            }

            chunk.transform.position = Vector3.forward * _offset * (currentRoadChunkIndex + _roadChunksCount);
            _roadChunksQueue.Enqueue(chunk);

            if (chunk.CheckChunkToStartChunk() || chunk.CheckChunkToEndChunk())
            {
                int newChunkType;

                do
                {
                    newChunkType = Random.Range(1, 4);
                } while (newChunkType == _chunkType || newChunkType == _oldChunkType);

                _oldChunkType = _chunkType;
                _chunkType = newChunkType;
                _index = _roadChunksCount;
            }

            _oldRoadChunkIndex = currentRoadChunkIndex;
        }
    }
}
