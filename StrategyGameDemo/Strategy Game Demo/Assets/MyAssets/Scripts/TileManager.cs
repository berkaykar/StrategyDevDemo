using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap MainTilemap;

    public Tile redTile, greenTile, whiteTile;

    public Vector3Int loc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        loc = MainTilemap.WorldToCell(mousePos);

        if (MainTilemap.GetTile(loc) == whiteTile)
        {
            Debug.Log(1);
        }
    }

    public TileState GetTileState(Vector3Int loc)
    {
        if(MainTilemap.GetTile(loc) == whiteTile)
        {
            return TileState.White;
        }
        else if (MainTilemap.GetTile(loc) == redTile)
        {
            return TileState.Red;
        }
        else if (MainTilemap.GetTile(loc) == greenTile)
        {
            return TileState.Green;
        }

        return TileState.Null;
    }

    public void SetTileState(Vector3Int loc, Tile tile)
    {
            MainTilemap.SetTile(loc, tile);
    }

}

public enum TileState
{
    White,
    Red,
    Green,
    Null
}
