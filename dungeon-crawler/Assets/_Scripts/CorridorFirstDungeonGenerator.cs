using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : RandomWalkMapGenerator
{
    [SerializeField] private int corridorLength = 14, corridorCount = 5;
    [SerializeField] [Range (0.1f, 1)] private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    } 
    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialroomFloors = new HashSet<Vector2Int>();
        CreateCorridors(floorPositions, potentialroomFloors);
        HashSet<Vector2Int> roomFloors = CreateRooms(potentialroomFloors);
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomAtDeadEnds(deadEnds, roomFloors);
        floorPositions.UnionWith(roomFloors);
        tilemapVizualizer.PaintFloorTiles(floorPositions); //calling floor and walls painter using new created floorpositions
        WallGenerator.CreateWalls(floorPositions, tilemapVizualizer);
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialroomFloors)
    {
        var currentPosition = startPosition;
        potentialroomFloors.Add(currentPosition);
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgoritms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count-1]; //this is needed to make sure that the corridors are connected
            potentialroomFloors.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialroomFloors)
    {
        HashSet<Vector2Int> roomFloors = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialroomFloors.Count*roomPercent); 
        // ^^^ this line uses a percentage value to create only 80% of all possible rooms (basically a count of rooms we want to generate)

        List<Vector2Int> roomsToCreate = potentialroomFloors.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList(); //globally unique identifier
        // ^^ this line is a bit more complicated, but we are basically creating a unique ID for each value of potential room positions
        // and sort them so it gets smartly randomized!
        // we randomly sorted our potential room positions hashset and only took our roomstocreatecount and converted them into a list!

        foreach(var roomPosition in roomsToCreate) //and now we're going through each position and create a room there!
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition); //here! :)
            roomFloors.UnionWith(roomFloor); //this will allow is avoid repetitions, as it was before with our run generator thingy
        }
        return roomFloors;
    }

    private List<Vector2Int> FindAllDeadEnds (HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
            }

            if (neighboursCount == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private void CreateRoomAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }
}
