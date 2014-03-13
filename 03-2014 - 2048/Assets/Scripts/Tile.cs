using System.Linq;
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

    public int MoveDown(GameObject[,] grid, GameObject[] tiles)
    {
        int originX = Util.GetTilePositionX(transform.position.x);
        int originY = Util.GetTilePositionY(transform.position.y);

        int downY = originY + 1;
        int downX = originX;

        if (grid[downX, downY] != null)
        {
            int downValue = grid[downX, downY].GetComponent<Tile>().Value;
            if (downValue == Value)
            {
                Value = downValue + Value;

                Destroy(grid[downX, downY].gameObject);

                grid[downX, downY] =
                    (GameObject)
                        Instantiate(tiles.First(t => t.name == "Tile " + Value),
                            new Vector3(Util.GetTilePositionX(downX), Util.GetTilePositionY(downY), -2),
                            new Quaternion());

                Destroy(gameObject);

                return 1;
            }
        }
        else
        {
            GameObject aux = grid[originX, originY];
            grid[originX, originY] = null;
            grid[downX, downY] = aux;
            aux.transform.position = new Vector3(Util.GetTilePositionX(downX), Util.GetTilePositionY(downY), -2);

            if (downY < 3)
            {
                return MoveDown(grid, tiles);
            }

            return 1;
        }

        return 0;
    }
}
