using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Transform tr;
    Vector3 moveDirection = Vector3.zero;
    Rigidbody rb;
    Collider coll;
    bool isFalling;
    bool isRolling;
    bool invincible;

    //public float gravity = 1f;
    public float speed = 8;
    public float maxVelocityChange = 10f;
    public float gravity = 9.8f;
    public float jumpHeight = 3f;
    public float rotationSpeed;
    public float umbralPrecision = 0.05f;

	public int lives = 3; 

    float velocity;
    float distToGround;

	public int floorMask;
	public float canRayLength = 100f;

    Renderer rend;
    float rollSpeed = 1f;
    void Start()
    {
        rotationSpeed = 5f;
        tr = this.gameObject.transform;

        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        coll = GetComponent<BoxCollider>();
        distToGround = coll.bounds.extents.y;

        isFalling = false;
        isRolling = false;
        invincible = false;

		floorMask = LayerMask.GetMask ("Floor");
    }

    void FixedUpdate()
    {
        float move_x = Input.GetAxis("Horizontal");
        float move_z = Input.GetAxis("Vertical");
		
		moveDirection.Set(move_x, 0f, move_z);
		
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isRolling)
            {
                isRolling = true;
                StartCoroutine("Roll");
            }
        }
			
        moveDirection = transform.TransformDirection(moveDirection);
		moveDirection = moveDirection * speed * rollSpeed * Time.deltaTime;
		rb.MovePosition(tr.position + moveDirection);

        /*Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (moveDirection - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);*/

        //rb.velocity = moveDirection  speed  rollSpeed;
        // Salto (Si es posible)
        if (grounded())
        {
            isFalling = false;

            if (Input.GetButtonDown("Jump"))
            {
                isFalling = true;
				rb.AddForce(new Vector3(0, 1500, 0));
                //rb.velocity = new Vector3(velocity.x, Mathf.Sqrt(2 * jumpHeight * gravity), velocity.z);
            }
        }
        //else
        //{
            //rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));
        //}

        //Me oriento hacia moveDirection
        //if (moveDirection.z != 0 || moveDirection.x != 0)
          //  LookThrough((moveDirection));

        //Aplico gravedad
        //moveDirection.y -= gravity;
        //moveDirection.y = Mathf.Clamp(moveDirection.y, -gravity * 20, jumpSpeed);

        //Descarto cualquier rotacion escepto la del eje Y
        //tr.rotation = Quaternion.Euler(new Vector3(0, tr.eulerAngles.y, 0));

        //Muevo al personaje
        //rb.velocity = moveDirection;

		//Rotation
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;

		if(Physics.Raycast(camRay, out hit, canRayLength, floorMask))
		{
			Vector3 playerToMouse = hit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

			rb.MoveRotation(newRotation);
		}
    }

    bool grounded()
    {
        return Physics.Raycast(tr.position, Vector3.down, distToGround + 0.1f);
    }

    // Aumenta la velocidad del jugador, cambia el color indicando momento de invencibilidad
    IEnumerator Roll()
    {
        var auxColor = rend.material.color;
        var c = Color.green;
        rend.material.color = c;
        rollSpeed = 2f;
        invincible = true;

        yield return new WaitForSeconds(0.75f);

        rend.material.color = auxColor;
        isRolling = false;
        invincible = false;
        rollSpeed = 1f;
    }

    void LookThrough(Vector3 direction)
    {
        //Rotación que debo adoptar para mirar hacia direction
        Quaternion rotation = Quaternion.LookRotation(direction);
        tr.rotation = Quaternion.Euler(new Vector3(0, tr.eulerAngles.y, 0));

        //Rotacion interpolada a una velocidad de rotationSpeed
        if (Mathf.Abs(tr.eulerAngles.y - rotation.eulerAngles.y) > umbralPrecision)
        {
            tr.rotation = Quaternion.RotateTowards(tr.rotation, rotation, /*Time.deltaTime **/ rotationSpeed);
        }

    }
}