using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using Random = System.Random;

public class Grid : MonoBehaviour
{
    public GameObject[] Tiles;

    // The values grid
    private GameObject[,] _grid;
    private Random _random;
    private GameObject _layerObject;

	void OnMouseDown() {
        _grid = new GameObject[4, 4];
        _random = new Random();

        // First of all we create an empty gameObject to put all
        // generated gameObjects in it.
        // This way we only have to delete this one to delete them all
        // each time a new game is launched
        var tmp = GameObject.Find("LoadedObjects");
        Destroy(tmp);
        _layerObject = new GameObject("LoadedObjects");
        
	    int x1 = _random.Next(0, 4);
	    int y1 = _random.Next(0, 4);

        // The two first tiles can be 2 or 4
        string tile1 = "Tile " + _random.Next(1, 3) * 2;
        string tile2 = "Tile " + _random.Next(1, 3) * 2;

        // We randomly place a tile anywhere on the grid
	    _grid[x1, y1] =
	        (GameObject)
	            Instantiate(Tiles.First(t => t.name == tile1), new Vector3(Util.GetTilePositionX(x1), Util.GetTilePositionY(y1), -2),
	                new Quaternion());
	    _grid[x1, y1].transform.parent = _layerObject.transform;
        
	    int x2;
	    int y2;

        // And then we randomly place a second tile anywhere but on the first
	    do
	    {
            x2 = _random.Next(0, 4);
            y2 = _random.Next(0, 4);
	    } while (x2 == x1 || y2 == y1);

	    _grid[x2, y2] =
	        (GameObject)
	            Instantiate(Tiles.First(t => t.name == tile2), new Vector3(Util.GetTilePositionX(x2), Util.GetTilePositionY(y2), -2),
                    new Quaternion());
        _grid[x2, y2].transform.parent = _layerObject.transform;
	}

    void Update()
    {
        int movements = 0;
        string direction = null;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            direction = "down";
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            direction = "right";
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z))
        {
            direction = "up";
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q))
        {
            direction = "left";
        }

        // TODO faire une boucle par direction
        if (direction != null)
        {
            switch (direction)
            {
                case "down":
                    for (int j = 2; j >= 0; j--)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (_grid[i, j] != null)
                            {
                                var aux = _grid[i, j].GetComponent<Tile>();
                                if (aux != null)
                                {
                                    if (j < 3) movements += aux.MoveDown(_grid, Tiles, _layerObject);
                                }
                            }
                        }
                    }
                    break;
                
                case "right":
                    for (int i = 2; i >= 0; i--)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (_grid[i, j] != null)
                            {
                                var aux = _grid[i, j].GetComponent<Tile>();
                                if (aux != null)
                                {
                                    if (i < 3) movements += aux.MoveRight(_grid, Tiles, _layerObject);
                                }
                            }
                        }
                    }
                    break;

                case "up":
                    for (int j = 1; j < 4; j++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (_grid[i, j] != null)
                            {
                                var aux = _grid[i, j].GetComponent<Tile>();
                                if (aux != null)
                                {
                                    if (j > 0) movements += aux.MoveUp(_grid, Tiles, _layerObject);
                                }
                            }
                        }
                    }
                    break;

                case "left":
                    for (int i = 1; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (_grid[i, j] != null)
                            {
                                var aux = _grid[i, j].GetComponent<Tile>();
                                if (aux != null)
                                {
                                    movements += aux.MoveLeft(_grid, Tiles, _layerObject);
                                }
                            }
                        }
                    }
                    break;
            }

            /*
            for (int j = 2; j >= 0; j--)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (_grid[i, j] != null)
                    {
                        var aux = _grid[i, j].GetComponent<Tile>();
                        if (aux != null)
                        {
                            switch (direction)
                            {
                                case "down":
                                    if (j < 3) movements += aux.MoveDown(_grid, Tiles, _layerObject);
                                    break;
                                case "right":
                                    if (i < 3) movements += aux.MoveRight(_grid, Tiles, _layerObject);
                                    break;
                                case "up":
                                    movements += aux.MoveUp(_grid, Tiles, _layerObject);
                                    break;
                                case "left":
                                    movements += aux.MoveLeft(_grid, Tiles, _layerObject);
                                    break;
                            }
                        }
                    }
                }
            }
            //*/

            if (movements > 0)
            {
                LastStepAfterMovements();
            }
        }
	}

    private void LastStepAfterMovements()
    {
        List<int[]> possibilities = new List<int[]>();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                try
                {
                    _grid[i, j].GetComponent<Tile>().Moved = false;
                }
                catch (Exception)
                {
                    
                }
                if (_grid[i, j] == null)
                {
                    possibilities.Add(new[] {i, j});
                }
            }
        }

        int rand = _random.Next(0, possibilities.Count);
        int x = possibilities.ElementAt(rand)[0];
        int y = possibilities.ElementAt(rand)[1];

        if (possibilities.Count == 0)
        {
            // GAMEOVER
            Debug.Log("GAMEOVER");
        }
        else
        {
            string tile1 = "Tile " + _random.Next(1, 3)*2;
            _grid[x, y] =
                (GameObject)
                    Instantiate(Tiles.First(t => t.name == tile1),
                        new Vector3(Util.GetTilePositionX(x), Util.GetTilePositionY(y), -2),
                        new Quaternion());
            _grid[x, y].transform.parent = _layerObject.transform;
        }
    }
}
