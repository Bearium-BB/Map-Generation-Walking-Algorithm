using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PorveduralGenerationAlgorithms;

public static class WallGenerator
{
    public static void CreatWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositons = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        foreach (var Positon in basicWallPositons)
        {
            tilemapVisualizer.PaintSingleBasicWall(Positon);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourposition = position + direction;
                if (floorPositions.Contains(neighbourposition) == false)
                {
                    wallPositions.Add(neighbourposition);
                }

            }
        }
        return wallPositions;
    }
}
