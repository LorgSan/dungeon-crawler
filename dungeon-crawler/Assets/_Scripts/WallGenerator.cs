using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVizualizer tilemapVizualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        CreateBasicWalls(tilemapVizualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVizualizer, cornerWallPositions, floorPositions);
    }

    private static void CreateBasicWalls(TilemapVizualizer tilemapVizualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighbourBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighbourBinaryType += "1";
                }
                else 
                {
                    neighbourBinaryType += "0";
                }
            }
            tilemapVizualizer.PaintSingleBasicWall(position, neighbourBinaryType);
        }
    }

    private static void CreateCornerWalls(TilemapVizualizer tilemapVizualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighbourBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighbourBinaryType += "1";
                } else
                {
                    neighbourBinaryType += "0";
                }
            }
            tilemapVizualizer.PaintSingleCornerWall(position, neighbourBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections (HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach(var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions;
    }
}
