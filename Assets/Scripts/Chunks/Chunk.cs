using UnityEngine;

public class Chunk : MonoBehaviour
{
    public bool IsEndChunk => _isEndChunk;
    public bool IsSpecialChunk => _isSpecialChunk;

    [SerializeField] private bool _isEndChunk;
    [SerializeField] private bool _isSpecialChunk;
}
