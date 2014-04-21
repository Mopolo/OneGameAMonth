using UnityEngine;
using Random = System.Random;

public class BadFish : MonoBehaviour
{
    private Shooter _shooter;
    private Random _random;

	void Start ()
	{
        _shooter = GetComponent<Shooter>();
        _random = new Random();
	}
	
	void Update ()
	{
        if (IsShooting())
        {
            _shooter.Shoot();
        }

        if (transform.position.y < -7f)
        {
            MoveToTop(GameManager.GetManager().Random);
        }
	}

    public void MoveToTop(Random random)
    {
        float x = random.Next(-60, 60) / 10f;
        float y = random.Next(60, 150) / 10f;
        float z = random.Next(-5, 5);
        transform.position = new Vector3(x, y, z);

        GetComponent<Movement>().Speed = new Vector2(0, random.Next(3, 6));
    }

    private bool IsShooting()
    {
        return false;
    }
}
