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
}

public static class Direction2D //this class will let us specify the end position of the vector2d
{ //Vector2Int is basically a representation of a vector2D, but in ints
    //so the int is to understand which way we're moving, uo down left or right
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
         Vector2Int.up, //this is just replacing the (0,1) and etc.
         Vector2Int.right,
         Vector2Int.down, 
         Vector2Int.left
    }; //we're basically declaring this list in class to then use in method to randomize the next position

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}