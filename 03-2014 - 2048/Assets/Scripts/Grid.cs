﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using Random = System.Random;

public class Grid : MonoBehaviour
{
    public GameObject[] Tiles;
    public GameObject GameOverSprite;

    // The values grid
    private GameObject[,] _grid;
    private Random _random;
    private GameObject _layerObject;

	public void LaunchNewGame() {
        GameObject.Find("TextInfosGUI").GetComponent<Score>().ResetPoints();

        GameOverSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

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
        if (Input.GetKeyDown(KeyCode.N))
        {
            LaunchNewGame();
        }

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

            if (movements > 0)
            {
                LastStepAfterMovements();
            }

            if (IsItGameOver())
            {
                GameOverSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .8f);
            }
        }
	}

    private bool IsItGameOver()
    {
        List<int[]> emptyPossibilities = new List<int[]>();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_grid[i, j] == null)
                {
                    emptyPossibilities.Add(new[] { i, j });
                }
            }
        }

        // if there is no more empty cells, it may be a gameover
        if (emptyPossibilities.Count == 0)
        {
            // we have to check if there is no more possible combinations before
            // calling a gameover
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    GameObject top = null, right = null, bottom = null, left = null;
                    try
                    {
                        top = _grid[i, j - 1];
                        if (_grid[i, j].GetComponent<Tile>().Value == top.GetComponent<Tile>().Value)
                        {
                            return false;
                        }
                    }
                    catch (Exception e) { }

                    try
                    {
                        right = _grid[i + 1, j];
                        if (_grid[i, j].GetComponent<Tile>().Value == right.GetComponent<Tile>().Value)
                        {
                            return false;
                        }
                    }
                    catch (Exception e) { }

                    try
                    {
                        bottom = _grid[i, j + 1];
                        if (_grid[i, j].GetComponent<Tile>().Value == bottom.GetComponent<Tile>().Value)
                        {
                            return false;
                        }
                    }
                    catch (Exception e) { }

                    try
                    {
                        left = _grid[i - 1, j];
                        if (_grid[i, j].GetComponent<Tile>().Value == left.GetComponent<Tile>().Value)
                        {
                            return false;
                        }
                    }
                    catch (Exception e) { }
                }
            }

            return true;
        }

        return false;
    }

    private void LastStepAfterMovements()
    {
        List<int[]> emptyPossibilities = new List<int[]>();

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
                    emptyPossibilities.Add(new[] {i, j});
                }
            }
        }

        if (emptyPossibilities.Count > 0)
        {
            int rand = _random.Next(0, emptyPossibilities.Count);
            int x = emptyPossibilities.ElementAt(rand)[0];
            int y = emptyPossibilities.ElementAt(rand)[1];

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
