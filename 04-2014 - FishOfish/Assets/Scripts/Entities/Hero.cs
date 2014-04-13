using UnityEngine;

public class Hero : FishOfishBehaviour
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
        MoveFish();
        Shoot();
	}

    void MoveFish()
    {
        var mousePos = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        Debug.Log(transform.position);
    }

    void Shoot()
    {
        if (_shootCooldown > 0)
        {
            _shootCooldown -= Time.deltaTime;
        }

	    bool isShooting = Input.GetMouseButton(0);

	    if (isShooting)
	    {
	        if (CanAttack())
	        {
	            _shootCooldown = ShootingRate;

	            var shot = Instantiate(ShotTransform,
	                new Vector3(transform.position.x + 0.05f, transform.position.y + 0.3f, transform.position.z + 10f),
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
    }

    bool CanAttack()
    {
        return _shootCooldown <= 0f;
    }
}
