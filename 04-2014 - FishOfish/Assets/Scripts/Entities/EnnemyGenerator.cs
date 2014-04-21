using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnnemyGenerator : MonoBehaviour
{
    public int MaxBadFishes = 5;
    public List<GameObject> BadFishes;

    private Random _random;
    private List<GameObject> _badFishes;

	void Start ()
	{
        _random = new Random();
        _badFishes = new List<GameObject>();
	}
	
	void Update ()
	{
	    GenerateBadFish();
	}

    void GenerateBadFish()
    {
        if (_badFishes.Count < MaxBadFishes)
        {
            var badFish = Instantiate(BadFishes[_random.Next(0, BadFishes.Count)], new Vector3(0, 2, 0), new Quaternion()) as GameObject;
            badFish.GetComponent<BadFish>().MoveToTop(GameManager.GetManager().Random);
            _badFishes.Add(badFish);
        }
    }
}
