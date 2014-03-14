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

        var tmp = GameObject.Find("LoadedObjects");
        Destroy(tmp);
        _layerObject = new GameObject("LoadedObjects");
        
        /*
	    int x1 = 0;
	    int y1 = 0;
	    int x2 = 0;
	    int y2 = 2;
        _grid[x1, y1] =
            (GameObject)
                Instantiate(Tiles.First(t => t.name == "Tile 2"), new Vector3(Util.GetTilePositionX(x1), Util.GetTilePositionY(y1), -2),
                    new Quaternion());

        _grid[x2, y2] =
            (GameObject)
                Instantiate(Tiles.First(t => t.name == "Tile 2"), new Vector3(Util.GetTilePositionX(x2), Util.GetTilePositionY(y2), -2),
                    new Quaternion());
        //*/

        //*
	    int x1 = _random.Next(0, 4);
	    int y1 = _random.Next(0, 4);

        string tile1 = "Tile " + _random.Next(1, 3) * 2;
        string tile2 = "Tile " + _random.Next(1, 3) * 2;

	    _grid[x1, y1] =
	        (GameObject)
	            Instantiate(Tiles.First(t => t.name == tile1), new Vector3(Util.GetTilePositionX(x1), Util.GetTilePositionY(y1), -2),
	                new Quaternion());
	    _grid[x1, y1].transform.parent = _layerObject.transform;
        
	    int x2;
	    int y2;

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
        //*/
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            int mouvements = 0;

	        for (int j = 2; j >= 0; j--)
	        {
	            for (int i = 0; i < 4; i++)
	            {
	                if (_grid[i, j] != null)
	                {
	                    var aux = _grid[i, j].GetComponent<Tile>();
                        if (aux != null) mouvements+= aux.MoveDown(_grid, Tiles, _layerObject);
	                }
	            }
	        }

            if (mouvements > 0)
            {
                int x, y;

                int[,] tests = new int[4, 4];
                List<int[]> possibilities = new List<int[]>();

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_grid[i, j] == null)
                        {
                            tests[i, j] = 0;
                            possibilities.Add(new[] { i, j });
                        }
                        else
                        {
                            tests[i, j] = 1;
                        }
                    }
                }
                Debug.Log(possibilities.Count);

                int rand = _random.Next(0, possibilities.Count);
                x = possibilities.ElementAt(rand)[0];
                y = possibilities.ElementAt(rand)[1];

                /*Debug.Log("=====AVANT=====");
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Debug.Log("tests[" + i + ", " + j + "] = " + tests[i, j]);
                    }
                }

                Debug.Log(CountTotal(tests));
                int a = 0;
                do
                {
                    x = _random.Next(0, 4);
                    y = _random.Next(0, 4);
                    tests[x, y] = 1;
                    //Debug.Log(CountTotal(tests));
                    //Debug.Log(_grid[x, y]);
                    a++;
                    if (_grid[x, y] == null)
                    {
                        Debug.Log("QSDFGHJKLM");
                    }
                } while (CountTotal(tests) < 16 || _grid[x, y] == null);
                Debug.Log("A: " + a);
                Debug.Log("=====APRES=====");
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Debug.Log("tests[" + i + ", " + j + "] = " + tests[i, j]);
                    }
                }

                Debug.Log(CountTotal(tests));*/

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
	}

    private int CountTotal(int[,] tests)
    {
        int total = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                total += tests[i, j];
            }
        }

        return total;
    }
}
