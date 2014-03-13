using System;
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

	// Use this for initialization
	void Start () {
        _grid = new GameObject[4, 4];

        _random = new Random();

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

        /*
	    int x1 = random.Next(0, 4);
	    int y1 = random.Next(0, 4);

	    _grid[x1, y1] =
	        (GameObject)
	            Instantiate(Tiles.First(t => t.name == "Tile " + random.Next(1, 3)*2), new Vector3(_x[x1], _y[y1], -2),
	                new Quaternion());
        
	    int x2;
	    int y2;

	    do
	    {
            x2 = random.Next(0, 4);
            y2 = random.Next(0, 4);
	    } while (x2 == x1 || y2 == y1);

	    _grid[x2, y2] =
	        (GameObject)
	            Instantiate(Tiles.First(t => t.name == "Tile " + random.Next(1, 3)*2), new Vector3(_x[x2], _y[y2], -2),
	                new Quaternion());
        //*/
	}
	
	// Update is called once per frame
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
                        if (aux != null) mouvements+= aux.MoveDown(_grid, Tiles);
	                }
	            }
	        }
            Debug.Log("mouv " + mouvements);
            //if (mouvements > 0)
            //{

                int x, y;

                int[,] tests = new int[4, 4];

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_grid[i, j] != null) tests[i, j] = 1;
                        tests[i, j] = 0;
                    }
                }

                do
                {
                    x = _random.Next(0, 4);
                    y = _random.Next(0, 4);
                    tests[x, y] = 1;
                    //Debug.Log(CountTotal(tests));
                    //Debug.Log(_grid[x, y]);
                } while (CountTotal(tests) < 16 || _grid[x, y] != null);

                //if (tested == 0)
                //{
                // GAMEOVER
                //Debug.Log("GAMEOVER");
                //}
                //else
                //{
                _grid[x, y] =
                    (GameObject)
                        Instantiate(Tiles.First(t => t.name == "Tile " + _random.Next(1, 3)*2),
                            new Vector3(Util.GetTilePositionX(x), Util.GetTilePositionY(y), -2),
                            new Quaternion());
                //}
            //}
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
