using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TerrainGenerator : MonoBehaviour
{
    public int MaxVagues = 10;
    public List<GameObject> VagueTransforms;
    public int CurrentVagues = 0;

	void Start ()
	{
	
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
        // TODO : réutiliser les vagues au lieu de les supprimer
        var random = new Random();

        float x, y;
        x = random.Next(-70, 70) / 10f;
        y = random.Next(60, 150) / 10f;

        var vague = Instantiate(VagueTransforms[random.Next(0, 2)], new Vector3(x, y, random.Next(-5, 5)), new Quaternion()) as GameObject;
        if (vague)
        {
            float scale = random.Next(10, 80) / 10f;
            vague.transform.localScale = new Vector3(scale, scale, 1f);
            var color = vague.transform.renderer.material.color;
            vague.transform.renderer.material.color = new Color(color.r, color.g, color.b, random.Next(1, 3)/10f);
            
            var movement = vague.GetComponent<Movement>();
            if (movement)
            {
                movement.Direction = -transform.up;
                movement.transform.rotation = transform.rotation;
                movement.Speed = new Vector2(0, random.Next(1, 8));
            }
        }

        CurrentVagues++;
    }
}
