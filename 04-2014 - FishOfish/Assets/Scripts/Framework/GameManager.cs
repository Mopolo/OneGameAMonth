using UnityEngine;
using Random = System.Random;

public class GameManager : FishOfishBehaviour
{
    public Random Random;

    public static GameManager GetManager()
    {
        return GameObject.Find("GameManager").GetComponent<GameManager>();
    }

	void Start ()
	{
        Random = new Random();
	    Screen.showCursor = false;
	}
	
	void Update ()
	{
	
	}
}
