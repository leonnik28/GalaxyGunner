using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] public ChunksPool pool;
    void Start()
    {
        pool.GetChunk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
