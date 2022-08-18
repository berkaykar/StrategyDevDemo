using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tiles _tilePrefab;
    [SerializeField] private Transform _cam;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++) 
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x + 0.5f, y + 0.5f), Quaternion.identity);

                spawnedTile.name = $"Tile {x} {y}";

                spawnedTile.transform.parent = GameObject.Find("Tiles").transform; 

                var isOffset = ((x%2 == 0 && y%2 != 0) || (x%2!=0 && y%2==0));
                spawnedTile.Init(isOffset);
            }
        }


        _cam.transform.position = new Vector3((float)_width / 2, (float)_height / 2, -10);
    }
}
