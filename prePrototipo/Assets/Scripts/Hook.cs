using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hook : MonoBehaviour 
{
	public GameObject player;
    public Camera camera;
	public float distance = 30f;
	public float close = 0.1f;
	public float speed = 20f;

    Rigidbody rb;
    Vector3 origin;
    Vector3 destiny;
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
	public GameObject pointer= null;
	Image pointerImage = null;
	HookPointers points = null;

	public float secondsCooldown = 2.0f;
	bool IsCooldown = false;

	public float waitingAfterHook = 0.5f;

	// Use this for initialization
	void Start () 
	{
        //tr = this.gameObject.transform;
        tr = camera.gameObject.transform;
		offset = tr.position;
		objective = offset;
		inHook = false;
        hooked = false;
		rb = player.GetComponent<Rigidbody>();
        originalConstraints = rb.constraints;   //Debería ser con la rotación bloqueada en los tres ejes
        line = GetComponent<LineRenderer>();
        line.enabled = false;

		pointerImage = pointer.GetComponent<Image> ();
		points = pointer.GetComponent<HookPointers> ();
        //line.startWidth = 0.1f;
        //line.endWidth = 0.1f;
        //line.startColor = Color.black;
        //line.endColor = Color.black;
    }

	void Update()
	{
		if (!inHook) 
		{
			target = checker ();

			if (target == null)
				pointerImage.sprite = points.normal;
			else {
				if (target.tag.Contains ("tag1"))
					pointerImage.sprite = points.toHang;
				else if (target.name == "Terrain") // Arreglo rápido, cuando todos los objetos tengan los tags que deberian tener borramos estoy y lo hacemos bien
					pointerImage.sprite = points.normal;
				else
					pointerImage.sprite = points.notToHang;
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (!IsCooldown) {
			if (Input.GetKeyDown (KeyCode.F)) {
				//f apretada
				target = checker (); 
				if (target != null) {
					if (target.tag.Contains ("tag1")) {
						StartCoroutine (throwHook (/*target*/));
						//move(player, target);
						inHook = true;
						IsCooldown = true;
						Debug.Log ("success");
					} else if (target.tag.Contains ("Enemy")) {
						EnemyController ec = target.gameObject.GetComponent<EnemyController> ();
						ec.getHit (1);
						IsCooldown = true;
						StartCoroutine (cooldown ());
					} else {
						StartCoroutine (throwHook ());
						Debug.Log ("null");
						IsCooldown = true;
						StartCoroutine (cooldown ());
					}
				}  
			}
		}
			if (inHook) 
			{
				if (Input.GetKeyDown (KeyCode.Space)) 
				{
					UnFreeze ();
					inHook = false;
					hooked = false;

					rb.AddForce (new Vector3 (0, 500, 0));
					StartCoroutine (cooldown ());
				} 
				else if (Input.GetKeyDown (KeyCode.LeftControl)) 
				{
					inHook = false;
					hooked = false;
					UnFreeze ();

					StartCoroutine (cooldown ());
				}
				else if (!hooking) 
				{
					//StopCoroutine(throwHook(target));
					move (player);
				}

			}
	}

    GameObject checker()
    {
        origin = tr.position;
        destiny = tr.forward * distance;
        Ray shootingRay = new Ray(origin, destiny);

        Debug.DrawRay(origin, destiny);

        if (Physics.Linecast(origin, destiny, out hit)) {
            destiny = hit.point;
            return hit.collider.gameObject;
        }
        return null;
    }

    void move (GameObject origin)
    {
        if (!hooked) {
            Vector3 currentPosition = origin.transform.position;
            //Vector3 finalPosition = destiny.transform.position;

            Vector3 moveVector = destiny - currentPosition;

            moveVector.Normalize();

            Vector3 velocidad = moveVector * speed * Time.deltaTime;

            rb.MovePosition(currentPosition + velocidad);

            //Debug.DrawRay(currentPosition, finalPosition,Color.black);
            //{ player.transform.position, target.transform.position });
            //line.enabled = true;
            
            // Comprobar si ha llegado al destiny
            Vector3 minDistance = new Vector3(1, 1, 1);
            if (Mathf.Abs((currentPosition - destiny).x) <= Mathf.Abs(minDistance.x) && Mathf.Abs((currentPosition - destiny).y) <= Mathf.Abs(minDistance.y) && Mathf.Abs((currentPosition - destiny).z) <= Mathf.Abs(minDistance.z)) {
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
    IEnumerator throwHook() {
        Debug.Log("Travelling to: " + destiny);
        hooking = true;
        GameObject Rope = (GameObject)Instantiate(Resources.Load("RopeCreator"));
        Rope.transform.position = this.transform.position;
        Rope.transform.parent = this.transform;
        //yield return new WaitForSeconds(0.01f);
        yield return new WaitForEndOfFrame();
        Rope.GetComponent<RopePhysics>().prevSegment.GetComponent<LastRopeSegmentController>().destiny = destiny;
		yield return new WaitForSeconds(waitingAfterHook);
        Destroy(Rope);



        //hooking = true;
        //line.enabled = true;
        //Vector3 origin = this.transform.position;
        //Debug.Log("Lanzando gancho de " + origin + " a " + destiny);
        ////for (int i = 1; i < 5; i++) {
        //for(int i = 6; i > 0; i--) {            
        //    line.SetPositions(new Vector3[] { origin, /*origin +*/ destiny / i });  // --> No es correcto
        //    //Debug.Log("throwing hook");
        //    yield return new WaitForSeconds(1f);
        //}
        //line.SetPositions(new Vector3[] { origin, destiny });

        ////Debug.Log("throwing hook");
        hooking = false;
        
    }
   	
	IEnumerator cooldown ()
	{
		yield return new WaitForSeconds(secondsCooldown);
		IsCooldown = false;
	}
}
