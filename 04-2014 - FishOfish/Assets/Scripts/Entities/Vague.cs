using UnityEngine;

public class Vague : MonoBehaviour
{
    private TerrainGenerator _terrainGenerator;

	void Start ()
	{
        _terrainGenerator = GameObject.Find("TerrainGenerator").GetComponent<TerrainGenerator>();
	}
	
	void Update ()
	{
	    if (transform.position.y < -6f)
	    {
	        _terrainGenerator.CurrentVagues--;
	        Destroy(gameObject, 0f);
	    }
	}
}
