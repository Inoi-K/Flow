using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelChunkData")]
public class LevelChunkData : ScriptableObject
{
    public enum Direction {
        Forward, Left, Right
    }

    public Vector2 chunkSize;

    public GameObject[] levelBlocks;
    public Direction entryDirection;
    public Direction exitDirection;
}
