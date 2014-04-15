using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TerrainGenerator : MonoBehaviour
{
    public int MaxVagues = 10;
    public List<GameObject> VagueTransforms;
    public int CurrentVagues = 0;

    private List<GameObject> _vagues;
    private Random _random;

	void Start ()
	{
	    _random = new Random();
        _vagues = new List<GameObject>();
	}
	
	void Update ()
	{
	    if (CurrentVagues < MaxVagues)
	    {
	        GenerateVague();
	    }
	}

    void GenerateVague()
    {
        if (_vagues.Count < MaxVagues)
        {
            var vague = Instantiate(VagueTransforms[_random.Next(0, 2)], new Vector3(0, 2, 0), new Quaternion()) as GameObject;
            vague.GetComponent<Vague>().MoveToTop(GameManager.GetManager().Random);
            _vagues.Add(vague);
        }
    }
}
