using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, WallTilemap;
    [SerializeField]
    private TileBase floorTile,wallTop; 
    
    public void paintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap Tilemap, TileBase Tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(Tilemap, Tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var TilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(TilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        WallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int positon)
    {
        PaintSingleTile(WallTilemap, wallTop, positon);
    }
}
