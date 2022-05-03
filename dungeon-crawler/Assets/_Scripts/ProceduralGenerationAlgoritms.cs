using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgoritms
{

    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength) //hashset is a collection that lets 
    //us hold unique values cause if the type implements hashcode methods and equals methods we can easily remove dublicates! 
    //and let the walker walk the same position twice to not process it again
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>(); //creating a new hashset of vectors2d 
        path.Add(startPosition); //adding our first start position
        var previousPosition = startPosition; //and setting the previous position to the startposition 

        for (int i = 0; i < walkLength; i++) //then we do the for loop for how long we want the length be
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }   
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength) //this method lets us create corridors between rooms
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }
}

public static class Direction2D //this method holds all the possible movements of the walker (so in our case, grid being rectangular)
{ //it's just up down right left 
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int> 
    {
         Vector2Int.up, //this is just replacing the (0,1) and etc.
         Vector2Int.right,
         Vector2Int.down, 
         Vector2Int.left
    };

    public static Vector2Int GetRandomCardinalDirection() //and then gets us a randomized next position
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}