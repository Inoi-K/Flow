using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    public LevelChunkData[] levelChunkData;
    public LevelChunkData sampleChunk;

    public Vector3 spawnOrigin;
    public int chunksToSpawn = 5;

    Vector3 spawnPosition;
    LevelChunkData previousChunk;

    void OnEnable() {
        TriggerExit.OnChunkExited += PickAndSpawnChunk;
    }

    private void OnDisable() {
        TriggerExit.OnChunkExited -= PickAndSpawnChunk;
    }

    private void Start() {
        previousChunk = sampleChunk;

        for (int i = 0; i < chunksToSpawn; ++i)
            PickAndSpawnChunk();
    }

    LevelChunkData PickNextChunk() {
        List<LevelChunkData> allowedChunkList = new List<LevelChunkData>();

        LevelChunkData.Direction nextEntryDirection = LevelChunkData.Direction.Forward;

        switch (previousChunk.exitDirection) {
            case LevelChunkData.Direction.Forward:
                spawnPosition += new Vector3(0f, 0f, previousChunk.chunkSize.y);
                break;
            case LevelChunkData.Direction.Left: // mb only chunkSize.x is not good for angled platform
                spawnPosition += new Vector3(-previousChunk.chunkSize.x, 0f, previousChunk.chunkSize.y);
                break;
            case LevelChunkData.Direction.Right:
                spawnPosition += new Vector3(previousChunk.chunkSize.x, 0f, previousChunk.chunkSize.y);
                break;
        }

        for (int i = 0; i < levelChunkData.Length; ++i)
            if (levelChunkData[i].entryDirection == nextEntryDirection)
                allowedChunkList.Add(levelChunkData[i]);

        return allowedChunkList[Random.Range(0, allowedChunkList.Count)];
    }

    void PickAndSpawnChunk() {
        LevelChunkData chunkToSpawn = PickNextChunk();
        GameObject variantToSpawn = chunkToSpawn.levelBlocks[Random.Range(0, chunkToSpawn.levelBlocks.Length)];
        Instantiate(variantToSpawn, spawnPosition + spawnOrigin, Quaternion.identity);
        previousChunk = chunkToSpawn;
    }

    public void UpdateSpawnOrigin(Vector3 originDelta) {
        spawnOrigin += originDelta;
    }
}
