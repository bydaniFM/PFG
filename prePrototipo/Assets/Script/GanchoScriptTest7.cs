using UnityEngine;
using System.Collections;

public class GanchoScriptTest7 : MonoBehaviour 
{
	public GameObject player;
	public float distance = 30f;
	public float close = 0.1f;
	public float speed = 20f;

    Rigidbody rb;
    Vector3 origin;
    Vector3 destino;
    GameObject target; 
    RaycastHit hit;

    Transform tr;
	Vector3 offset = Vector3.zero;
	Vector3 objective;
	bool inHook;

	// Use this for initialization
	void Start () 
	{
		tr = this.gameObject.transform;
		offset = tr.position;
		objective = offset;
		inHook = false;
		rb = player.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{

        if (Input.GetKeyDown(KeyCode.F))
        {
            //f apretada
            target = checker(); 
            if (target != null)
            {
                if (target.tag.Contains("tag1"))
                {
                    move(player, target);
					inHook = true;
                    Debug.Log("success");
                }
                else Debug.Log("null");
            }  
        }

		if (inHook) 
		{
			if (Input.GetKeyDown (KeyCode.LeftControl))
				inHook = false;
			else
				move(player, target);


		}
			
	}

    GameObject checker()
    {
        origin = tr.position;
        destino = tr.forward * distance;
        Ray shootingRay = new Ray(origin, destino);

        Debug.DrawRay(origin, destino);

        if (Physics.Linecast(origin, destino, out hit))
        {
           return hit.collider.gameObject;
        }
        return null;
    }

    void move (GameObject origin, GameObject destiny)
    {
		Vector3 currentPosition = origin.transform.position;
		Vector3 finalPosition = destiny.transform.position;

		Vector3 moveVector = finalPosition - currentPosition;

		moveVector.Normalize();

		Vector3 velocidad = moveVector * speed * Time.deltaTime;

		rb.MovePosition(currentPosition + velocidad);

		// Vieja forma
        //rb = origin.GetComponent<Rigidbody>();
        //Vector3 destiny_pos = new Vector3(destiny.transform.position.x - 1f, destiny.transform.position.y + 3.6f, destiny.transform.position.z + 1f);
		//rb.velocity = velocidad;

        //player.transform.position = destiny.transform.position;
        
    }
    
   
}
