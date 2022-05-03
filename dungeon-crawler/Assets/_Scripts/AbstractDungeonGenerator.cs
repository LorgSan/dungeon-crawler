using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TilemapVizualizer tilemapVizualizer = null;
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tilemapVizualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration(); // "parent" function

}
