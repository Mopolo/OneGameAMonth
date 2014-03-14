using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using Random = System.Random;

public class Tile : MonoBehaviour
{
    public int Value;
    private Random _random;

	// Use this for initialization
	void Start () {
        _random = new Random();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int MoveDown(GameObject[,] grid, GameObject[] tiles, GameObject layer)
    {
        int originX = Util.GetTilePositionX(transform.position.x);
        int originY = Util.GetTilePositionY(transform.position.y);

        int newY = originY + 1;
        int newX = originX;

        if (grid[newX, newY] != null)
        {
            int downValue = grid[newX, newY].GetComponent<Tile>().Value;
            if (downValue == Value)
            {
                Value = downValue + Value;

                Destroy(grid[newX, newY].gameObject);

                grid[newX, newY] =
                    (GameObject)
                        Instantiate(tiles.First(t => t.name == "Tile " + Value),
                            new Vector3(Util.GetTilePositionX(newX), Util.GetTilePositionY(newY), -2),
                            new Quaternion());
                grid[newX, newY].transform.parent = layer.transform;

                Destroy(gameObject);

                return 1;
            }
        }
        else
        {
            GameObject aux = grid[originX, originY];
            grid[originX, originY] = null;
            grid[newX, newY] = aux;
            aux.transform.position = new Vector3(Util.GetTilePositionX(newX), Util.GetTilePositionY(newY), -2);

            if (newY < 3)
            {
                return MoveDown(grid, tiles, layer);
            }

            return 1;
        }

        return 0;
    }
}
