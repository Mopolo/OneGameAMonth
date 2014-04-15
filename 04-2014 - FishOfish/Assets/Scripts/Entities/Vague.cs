using UnityEngine;
using Random = System.Random;

public class Vague : MonoBehaviour
{
	void Start ()
	{
        InitVague();
	}
	
	void Update ()
	{
	    if (transform.position.y < -8f)
	    {
            MoveToTop(GameManager.GetManager().Random);
	    }
	}

    void InitVague()
    {
        var movement = GetComponent<Movement>();
        if (movement)
        {
            movement.Direction = -transform.up;
            movement.transform.rotation = transform.rotation;
        }
    }

    public void MoveToTop(Random random)
    {
        // first we randomly place the vague above the camera
        float x = random.Next(-70, 70) / 10f;
        float y = random.Next(60, 150) / 10f;
        float z = random.Next(-5, 5);
        transform.position = new Vector3(x, y, z);

        // next we change the scale to have different sizes
        float scale = random.Next(10, 80) / 10f;
        transform.localScale = new Vector3(scale, scale, 1f);

        // then we change the opacity
        var color = transform.renderer.material.color;
        transform.renderer.material.color = new Color(color.r, color.g, color.b, random.Next(1, 3) / 10f);

        // and finally we pick a random speed
        GetComponent<Movement>().Speed = new Vector2(0, random.Next(1, 8));
    }
}
