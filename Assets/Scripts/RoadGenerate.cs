using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class RoadGenerate : MonoBehaviour
{
    public int ChunkIndex => _oldRoadChunkIndex;

    [SerializeField] private ChunksPool _chunksPool;
    [SerializeField] private TraveledDistance _traveledDistance;
    [SerializeField] private Chunk _startChunk;

    [Space]
    [SerializeField] private float _offset = 5f;
    [SerializeField, Min(0)] private int _roadChunksCount = 5;

    private Queue<Chunk> _roadChunksQueue = new Queue<Chunk>();

    private int _oldRoadChunkIndex = 0;
    private int _index;
    private int _oldChunkType= 1;
    private int _chunkType = 1;
    private int _currentChunkType;

    private void Start()
    {
        StartGame();
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
                _currentChunkType = _oldChunkType;
            }
            else
            {
                chunk = _chunksPool.GetChunk(_chunkType, false);
                _currentChunkType = _chunkType;
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

                _currentChunkType = newChunkType;
                _oldChunkType = _chunkType;
                _chunkType = newChunkType;
                _index = _roadChunksCount;
            }

            _oldRoadChunkIndex = currentRoadChunkIndex;
        }
    }

    public Chunk FindNeedChunk(List<Chunk> otherChunksList)
    {
        Chunk newChunk = otherChunksList[_currentChunkType - 1];
        return newChunk;
    }

    public void ChangeChunk(Chunk chunk)
    {
        int currentRoadChunkIndex = 1;

        if (_traveledDistance.Distance % _offset < _offset / 2)
        {
            currentRoadChunkIndex--;
        }

        Chunk[] chunks = _roadChunksQueue.ToArray();
        Chunk oldChunk = chunks[currentRoadChunkIndex];

        oldChunk.gameObject.SetActive(false);

        chunk.transform.position = oldChunk.transform.position;
        chunk.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        _chunkType = 1;
        _oldChunkType = 1;
        _oldRoadChunkIndex = 0;
        _currentChunkType = _chunkType;

        while(_roadChunksQueue.Count > 0)
        {
            Chunk chunk = _roadChunksQueue.Dequeue();
            _chunksPool.ReturnChunk(chunk);
        }
        _roadChunksQueue?.Clear();

        _roadChunksQueue.Enqueue(_startChunk);
        _startChunk.gameObject.SetActive(true);

        for (int i = 0; i < _roadChunksCount; i++)
        {
            Chunk chunk = _chunksPool.GetChunk(_chunkType, true);
            chunk.TryGetComponent<UpdateChunk>(out UpdateChunk updateChunk);
            if (updateChunk != null)
            {
                updateChunk.UpdateChunkObjects();
            }

            chunk.transform.position = Vector3.forward * (i + 1) * _offset;
            _roadChunksQueue.Enqueue(chunk);
        }
        
    }
}
