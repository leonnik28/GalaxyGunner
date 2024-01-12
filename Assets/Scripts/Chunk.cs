using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private bool _isStartChunk;
    [SerializeField] private bool _isEndChunk;

    public bool CheckChunkToStartChunk()
    {
        return _isStartChunk;
    }

    public bool CheckChunkToEndChunk()
    {
        return _isEndChunk;
    }
}
