using UnityEngine;
using System.Collections;

/// <summary>
/// Clase simple para el movimiento del player
/// </summary>
public class PlayerControllerTest10 : MonoBehaviour 
{
	Transform tr;
	Vector3 moveDirection = Vector3.zero;
	CharacterController controller;
    bool isFalling;
	bool isRolling;
	bool invincible;

    public float speed = 8;
	public float jumpSpeed = 14;
	public float gravity = 1;
	public float rotationSpeed = 500;
	public float umbralPrecision = 0.05f;
	
	float velocity;

	Renderer rend;
	float rollSpeed = 1f;

	void Start()
	{
		tr = this.gameObject.transform;

		controller = GetComponent<CharacterController>();
		rend = GetComponent<Renderer> ();

		isFalling = false;	
		isRolling = false;
		invincible = false;
	}

	void Update() 
	{
		float move_x = Input.GetAxis("Horizontal");
        float move_z = Input.GetAxis("Vertical");

		if (Input.GetKeyDown (KeyCode.LeftShift)) 
		{
			if (!isRolling) 
			{
				isRolling = true;
				StartCoroutine ("Roll");
			}
		}

        moveDirection = new Vector3(move_x * speed * rollSpeed, moveDirection.y, move_z * speed * rollSpeed);

		// Salto (Si es posible)
		if (controller.isGrounded)
		{
			isFalling = false;

			if (Input.GetButtonDown("Jump"))
				moveDirection.y = jumpSpeed;
		}
		else
		{
			if(controller.velocity.y < 0 && !isFalling)
			{
				isFalling = true;
			}
		}
			
		//Me oriento hacia moveDirection
		if(moveDirection.z != 0 || moveDirection.x != 0)
			LookThrough((moveDirection));
			
		//Aplico la gravedad
		moveDirection.y -= gravity;
		moveDirection.y = Mathf.Clamp(moveDirection.y, -gravity * 20, jumpSpeed);

		//Descarto cualquier rotacion escepto la del eje Y
		tr.rotation = Quaternion.Euler( new Vector3(0, tr.eulerAngles.y, 0));

		//Muevo al personaje
		controller.Move((moveDirection) * Time.deltaTime);
		
	}

	// Aumenta la velocidad del jugador, cambia el color indicando momento de invencibilidad
	IEnumerator Roll()
	{
		var auxColor = rend.material.color;
		var c = Color.green;
		rend.material.color = c;
		rollSpeed = 2f;
		invincible = true;

		/*for (int i = 0; i < 360; i += 20) 
		{
			tr.Rotate (Vector3.right, i, Space.Self);
			yield return new WaitForSeconds (0.1f);
		}*/

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
		tr.rotation = Quaternion.Euler( new Vector3(0, tr.eulerAngles.y, 0));

		//Rotacion interpolada a una velocidad de rotationSpeed
        if (Mathf.Abs(tr.eulerAngles.y - rotation.eulerAngles.y) > umbralPrecision)
		{
			tr.rotation = Quaternion.RotateTowards(tr.rotation, rotation, Time.deltaTime * rotationSpeed);
		}

    }

	void OnParticleCollision (GameObject go)
	{
		if (!invincible) {
			Debug.Log ("Recibido daño por " + go.name);
		} else
			Debug.Log ("Bloqueado por invencibilidad");
	}
}




























