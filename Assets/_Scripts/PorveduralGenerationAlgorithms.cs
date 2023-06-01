using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PorveduralGenerationAlgorithms
{
public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition,int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);

        var previousPosition = startPosition;

        for(int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirections();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition,int corridorLength)
    {
        List<Vector2Int> Corridor = new List<Vector2Int>(); 
        var direction = Direction2D.GetRandomCardinalDirections();
        var currentPosition = startPosition;
        Corridor.Add(startPosition);
        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            Corridor.Add(currentPosition);
        }
        return Corridor;
    }
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }else if(room.size.y >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if(room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.y >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        throw new System.NotImplementedException();
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        throw new System.NotImplementedException();
    }

    public static class Direction2D
    {
        public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
        {
            new Vector2Int(0,1),//up
            new Vector2Int(1,0),//right
            new Vector2Int(0,-1),//down
            new Vector2Int(-1,0),//left
        };

        public static Vector2Int GetRandomCardinalDirections()
        {
            return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
        }
    }
}
