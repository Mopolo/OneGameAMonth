using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject ShotTransform;
    public float ShootingRate = 0.25f;

    private float _shootCooldown;

	void Start ()
	{
        _shootCooldown = 0f;
	}
	
	void Update ()
	{
	
	}

    public void Shoot()
    {
        if (_shootCooldown > 0)
        {
            _shootCooldown -= Time.deltaTime;
        }
        if (CanShoot())
        {
            _shootCooldown = ShootingRate;

            var shot = Instantiate(ShotTransform,
                new Vector3(transform.position.x - 0.01f, transform.position.y + 0.3f, transform.position.z + 10f),
                new Quaternion()) as GameObject;

            if (shot)
            {
                var movement = shot.GetComponent<Movement>();
                if (movement)
                {
                    movement.Direction = transform.up;
                    movement.transform.rotation = transform.rotation;
                }
            }
        }
    }

    public bool CanShoot()
    {
        return _shootCooldown <= 0f;
    }
}
