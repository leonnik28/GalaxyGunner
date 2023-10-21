using System.Collections.Generic;
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

    private void Start()
    {
        _roadChunksQueue.Enqueue(_startChunk);

        for(int i = 0;  i < _roadChunksCount; i++)
        {
            Chunk chunk = _chunksPool.GetChunk();
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

            Chunk chunk = _chunksPool.GetChunk();
            chunk.transform.position = Vector3.forward * _offset * (currentRoadChunkIndex + _roadChunksCount);

            _roadChunksQueue.Enqueue(chunk);

            _oldRoadChunkIndex = currentRoadChunkIndex;
        }
    }
}
