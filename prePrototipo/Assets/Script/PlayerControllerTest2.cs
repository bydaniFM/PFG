using UnityEngine;
using System.Collections;

/// <summary>
/// Clase simple para el movimiento del player
/// </summary>
public class PlayerControllerTest2 : MonoBehaviour 
{
	Transform tr;
	Vector3 moveDirection = Vector3.zero;
	CharacterController controller;
    bool isFalling;

    public float speed = 2;
	public float jumpSpeed = 2;
	public float gravity = 1;
	public float rotationSpeed = 500;
	public float umbralPrecision = 0.05f;
	
	float velocity;

	void Start()
	{
		tr = this.gameObject.transform;

		controller = GetComponent<CharacterController>();

		isFalling = false;		
	}

	void Update() 
	{
		float move_x = Input.GetAxis("Horizontal");
        float move_z = Input.GetAxis("Vertical");

        moveDirection = new Vector3(move_x * speed, moveDirection.y, move_z * speed);

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
}

//Mathf.Clamp


























