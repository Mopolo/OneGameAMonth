using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using Random = System.Random;

public class Tile : MonoBehaviour
{
    public int Value;
    private Random _random;
    public bool Moved = false;

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

        return Move("down", originX, originY, newX, newY, grid, tiles, layer);
    }

    public int MoveRight(GameObject[,] grid, GameObject[] tiles, GameObject layer)
    {
        int originX = Util.GetTilePositionX(transform.position.x);
        int originY = Util.GetTilePositionY(transform.position.y);

        int newY = originY;
        int newX = originX + 1;

        return Move("right", originX, originY, newX, newY, grid, tiles, layer);
    }

    public int MoveUp(GameObject[,] grid, GameObject[] tiles, GameObject layer)
    {
        int originX = Util.GetTilePositionX(transform.position.x);
        int originY = Util.GetTilePositionY(transform.position.y);

        int newY = originY - 1;
        int newX = originX;

        return Move("up", originX, originY, newX, newY, grid, tiles, layer);
    }

    public int MoveLeft(GameObject[,] grid, GameObject[] tiles, GameObject layer)
    {
        int originX = Util.GetTilePositionX(transform.position.x);
        int originY = Util.GetTilePositionY(transform.position.y);

        int newY = originY;
        int newX = originX - 1;

        return Move("left", originX, originY, newX, newY, grid, tiles, layer);
    }

    public int Move(string direction, int originX, int originY, int newX, int newY, GameObject[,] grid, GameObject[] tiles, GameObject layer)
    {
        if (grid[newX, newY] != null)
        {
            int otherValue = grid[newX, newY].GetComponent<Tile>().Value;
            if (otherValue == Value && grid[newX, newY].GetComponent<Tile>().Moved == false)
            {
                Value = otherValue + Value;

                Destroy(grid[newX, newY].gameObject);

                grid[newX, newY] =
                    (GameObject)
                        Instantiate(tiles.First(t => t.name == "Tile " + Value),
                            new Vector3(Util.GetTilePositionX(newX), Util.GetTilePositionY(newY), -2),
                            new Quaternion());
                grid[newX, newY].transform.parent = layer.transform;
                grid[newX, newY].GetComponent<Tile>().Moved = true;

                Destroy(gameObject);

                grid[originX, originY] = null;

                return 1;
            }
        }
        else
        {
            GameObject aux = grid[originX, originY];
            grid[originX, originY] = null;
            grid[newX, newY] = aux;
            aux.transform.position = new Vector3(Util.GetTilePositionX(newX), Util.GetTilePositionY(newY), -2);

            switch (direction)
            {
                case "down":
                    if (newY < 3)
                    {
                        return MoveDown(grid, tiles, layer);
                    }
                    break;
                case "right":
                    if (newX < 3)
                    {
                        return MoveRight(grid, tiles, layer);
                    }
                    break;
                case "up":
                    if (newY > 0)
                    {
                        return MoveUp(grid, tiles, layer);
                    }
                    break;
                case "left":
                    if (newX > 0)
                    {
                        return MoveLeft(grid, tiles, layer);
                    }
                    break;
            }
            
            return 1;
        }

        return 0;
    }
}
