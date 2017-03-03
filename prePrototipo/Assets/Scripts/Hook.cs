using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour 
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
	bool inHook;    //En fase de lanzamiento
    bool hooked;    //Cuando el personaje está enganchado
    bool hooking;   //El gancho está siendo lanzado;
    RigidbodyConstraints originalConstraints;
    public LineRenderer line;


	// Use this for initialization
	void Start () 
	{
		tr = this.gameObject.transform;
		offset = tr.position;
		objective = offset;
		inHook = false;
        hooked = false;
		rb = player.GetComponent<Rigidbody>();
        originalConstraints = rb.constraints;   //Debería ser con la rotación bloqueada en los tres ejes
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        //line.startWidth = 0.1f;
        //line.endWidth = 0.1f;
        //line.startColor = Color.black;
        //line.endColor = Color.black;
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
                    StartCoroutine(throwHook(target));
                    //move(player, target);
					inHook = true;
                    Debug.Log("success");
                }
                else Debug.Log("null");
            }  
        }

		if (inHook) 
		{
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				UnFreeze ();
				inHook = false;
				hooked = false;

				rb.AddForce(new Vector3(0, 500, 0));
			} 
			else if (Input.GetKeyDown (KeyCode.LeftControl)) 
			{
				inHook = false;
				hooked = false;
				UnFreeze ();
			}
			else if (!hooking) {
                //StopCoroutine(throwHook(target));
                move(player, target);
            }

		}
			
	}

    GameObject checker()
    {
        origin = tr.position;
        destino = tr.forward * distance;
        Ray shootingRay = new Ray(origin, destino);

        Debug.DrawRay(origin, destino);

        if (Physics.Linecast(origin, destino, out hit)) {
            return hit.collider.gameObject;
        }
        return null;
    }

    void move (GameObject origin, GameObject destiny)
    {
        if (!hooked) {
            Vector3 currentPosition = origin.transform.position;
            Vector3 finalPosition = destiny.transform.position;

            Vector3 moveVector = finalPosition - currentPosition;

            moveVector.Normalize();

            Vector3 velocidad = moveVector * speed * Time.deltaTime;

            rb.MovePosition(currentPosition + velocidad);

            //Debug.DrawRay(currentPosition, finalPosition,Color.black);
            //{ player.transform.position, target.transform.position });
            //line.enabled = true;
            
            // Comprobar si ha llegado al destino
            Vector3 minDistance = new Vector3(1, 1, 1);
            if (Mathf.Abs((currentPosition - finalPosition).x) <= Mathf.Abs(minDistance.x) && Mathf.Abs((currentPosition - finalPosition).y) <= Mathf.Abs(minDistance.y) && Mathf.Abs((currentPosition - finalPosition).z) <= Mathf.Abs(minDistance.z)) {
                hooked = true;
                line.enabled = false;
            }
        }else {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            //rb.freezeRotation = true;
        }



        // Vieja forma
        //rb = origin.GetComponent<Rigidbody>();
        //Vector3 destiny_pos = new Vector3(destiny.transform.position.x - 1f, destiny.transform.position.y + 3.6f, destiny.transform.position.z + 1f);
        //rb.velocity = velocidad;

            //player.transform.position = destiny.transform.position;

    }
    public void UnFreeze() {
        rb.constraints = originalConstraints;
    }
    
    // El cálculo de la trayectoria de la cuerda no es correcto
    IEnumerator throwHook(GameObject destiny) {
        Vector3 finalPosition = destiny.transform.position;
        hooking = true;
        line.enabled = true;
        Debug.Log("Lanzando gancho de " + transform.position + " a " + finalPosition);
        //for (int i = 1; i < 5; i++) {
        for(int i = 6; i > 0; i--) {            
            line.SetPositions(new Vector3[] { transform.position, transform.position + finalPosition/i });  // --> No es correcto
            //Debug.Log("throwing hook");
            yield return new WaitForSeconds(.1f);
        }
        line.SetPositions(new Vector3[] { transform.position, finalPosition});

        //Debug.Log("throwing hook");
        hooking = false;
    }
   
}
