using UnityEngine;

public class Hero : FishOfishBehaviour
{
    private Shooter _shooter;

	void Start ()
	{
	    _shooter = GetComponent<Shooter>();
	}
	
	void Update ()
	{
        MoveFish();

	    if (IsShooting())
	    {
	        _shooter.Shoot();
	    }
	}

    void MoveFish()
    {
        var mousePos = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
    }

    private bool IsShooting()
    {
        return Input.GetMouseButton(0);
    }
}
