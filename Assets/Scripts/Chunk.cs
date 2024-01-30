using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private bool _isStartChunk;
    [SerializeField] private bool _isEndChunk;
    [SerializeField] private bool _isSpecialChunk;

    public bool CheckChunkToStartChunk()
    {
        return _isStartChunk;
    }

    public bool CheckChunkToEndChunk()
    {
        return _isEndChunk;
    }

    public bool CheckChunkToSpecialChunk()
    {
        return _isSpecialChunk;
    }
}
