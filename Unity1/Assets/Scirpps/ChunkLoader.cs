using UnityEngine;
using System.Collections.Generic;

public class ChunkLoader : MonoBehaviour
{
    public GameObject chunkPrefab;
    public float bufferDistance = 10f;
    public float chunkSize = 16f;
    private Vector2 lastChunkLoaded;
    private Dictionary<Vector2, GameObject> loadedChunks = new Dictionary<Vector2, GameObject>();

    private void Update()
    {
        Vector2 currentChunk = new Vector2(Mathf.FloorToInt(transform.position.x / chunkSize), Mathf.FloorToInt(transform.position.y / chunkSize));

        if (currentChunk != lastChunkLoaded)
        {
            LoadChunks(currentChunk);
            UnloadChunks(currentChunk);
            lastChunkLoaded = currentChunk;
        }
    }

    private void LoadChunks(Vector2 currentChunk)
    {
        if (!IsChunkLoaded(currentChunk))
        {
            GameObject newChunk = Instantiate(chunkPrefab, currentChunk * chunkSize, Quaternion.identity);
            loadedChunks.Add(currentChunk, newChunk);
        }
    }

    private void UnloadChunks(Vector2 currentChunk)
    {
        List<Vector2> chunksToRemove = new List<Vector2>();

        foreach (var loadedChunkPosition in loadedChunks.Keys)
        {
            float distance = Vector2.Distance(currentChunk, loadedChunkPosition);

            if (distance > bufferDistance)
            {
                chunksToRemove.Add(loadedChunkPosition);
            }
        }

        foreach (var chunkToRemove in chunksToRemove)
        {
            GameObject chunkObject = loadedChunks[chunkToRemove];
            loadedChunks.Remove(chunkToRemove);

            Destroy(chunkObject);
        }
    }


    private bool IsChunkLoaded(Vector2 chunkPosition)
    {
        return loadedChunks.ContainsKey(chunkPosition);
    }
}

