using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PorveduralGenerationAlgorithms;

public class CorridorFirstDungeonGeneratoer : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;


    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> porentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, porentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(porentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);


        CreateRoomsAtDeadEnd(deadEnds, roomPositions);
        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.paintFloorTiles(floorPositions);
        WallGenerator.CreatWalls(floorPositions, tilemapVisualizer);
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
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

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position+direction))
                {
                    neighboursCount++;
                }
            }
            if (neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> porentialRoomPositions)
    {
        HashSet<Vector2Int> RoomPositions = new HashSet<Vector2Int>();
        int roomTocreateCount = Mathf.RoundToInt(porentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomToCreate = porentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomTocreateCount).ToList();
        foreach (Vector2Int roomPosition in roomToCreate)
        {
            var RoomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            RoomPositions.UnionWith(RoomFloor);
        }
        return RoomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> porentialRoomPositions)
    {   
        var currentPosition = startPosition;
        porentialRoomPositions.Add(currentPosition);
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = PorveduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            porentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }
}
