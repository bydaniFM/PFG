using UnityEngine;
using System.Collections;

/// <summary>
/// Clase simple para el movimiento del player
/// </summary>
public class playerControllerClass : MonoBehaviour 
{
	/// <summary>
	/// Indica si estoy en modo depuración.
	/// </summary>
	public bool trace;
	
	/// <summary>
	/// Mi Transform
	/// </summary>
	protected Transform tr;
	
	/// <summary>
	/// Velocidad de desplazamiento
	/// </summary>
	public float speed = 15;
	
	/// <summary>
	/// Velocidad de salto
	/// </summary>
	public float jumpSpeed = 30;
	
	/// <summary>
	/// Gravedad
	/// </summary>
	public float gravity = 1;
	
	/// <summary>
	/// Dirección del movimiento
	/// </summary>
	protected Vector3 moveDirection = Vector3.zero;
	
	/// <summary>
	/// Character controller
	/// </summary>
	protected CharacterController controller;

	/// <summary>
	/// The mecanim.
	/// </summary>
	//protected Animator mecanim;
	
	/// <summary>
	/// Velocidad de rotación
	/// </summary>
	public float rotationSpeed = 800;
	
	/// <summary>
	/// Precisión en las rotaciones.
	/// </summary>
	public float umbralPrecision = 0.05f;

	public bool platformController;
	bool isFalling;

	float velocity;

	void Start()
	{
		//Asigno mi transform
		tr = this.gameObject.transform;
		
		//Asigno mi character controller
		controller = GetComponent<CharacterController>();

		//Asigno el animator
		//mecanim = GetComponent<Animator>();

		isFalling = false;
		
	}	//	end	Start()
	
	

	void Update() 
	{
		#region Obtengo la dirección de movimiento y parametrizo Mecanim

		//Leo la velocidad del mando
		float move_x = Input.GetAxis("Horizontal");


		if (platformController)
		{
			moveDirection = new Vector3(move_x * speed, moveDirection.y, 0);
		}
		else
		{
			//Leo la velocidad del mando
			float move_z = Input.GetAxis("Vertical");
			moveDirection = new Vector3(move_x * speed, moveDirection.y, move_z * speed);
		}


		//¿El personaje esta en el suelo?
		if (controller.isGrounded)
		{
			isFalling = false;
			
			//¿Se produce un salto?
			if (Input.GetButtonDown("Jump"))
			{
				//Se establecen los parametros del mecanim
				//mecanim.SetTrigger("Salto");
				
				//Se aplica la fuerza de salto
				moveDirection.y = jumpSpeed;
			}
		}
		else
		{
			if(controller.velocity.y < 0 && !isFalling)
			{
				//Se establecen los parametros del mecanim
				//mecanim.SetTrigger("fall");
				//Impide el redisparo del trigger fall mientras el personaje esté cayendo
				isFalling = true;
			}
			
		}



		//Me oriento hacia moveDirection
		if(moveDirection.z != 0 || moveDirection.x != 0)
			LookThrough((moveDirection));
			
		//Aplico la gravedad
		moveDirection.y -= gravity;
		moveDirection.y = Mathf.Clamp(moveDirection.y, -gravity * 20, jumpSpeed);

		velocity = Mathf.SmoothStep(velocity, Mathf.Abs(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude), Time.deltaTime * rotationSpeed / 10);
		//velocity = Mathf.Abs(new Vector2(controller.velocity.x, controller.velocity.z).magnitude)
		//if(Input.GetAxis("Horizontal") != 0)
		//	velocity = Mathf.SmoothStep(velocity, Mathf.Abs((float)Input.GetAxis("Horizontal")), Time.deltaTime * rotationSpeed / 10 );
		//else if(Input.GetAxis("Vertical") != 0)
		//	velocity = Mathf.SmoothStep(velocity, Mathf.Abs((float)Input.GetAxis("Vertical")), Time.deltaTime * rotationSpeed / 10 );
		#endregion
		
		#region Desplazamiento
		//Descarto cualquier rotacion escepto la del eje Y
		tr.rotation = Quaternion.Euler( new Vector3(0, tr.eulerAngles.y, 0));
		//Muevo al personaje
		controller.Move((moveDirection) * Time.deltaTime);
		#endregion
		
	}	//	end Update()
	
	
	
	/// <summary>
	/// Función que hace que el player se oriente progresivamente hacia la dirección pasada como parámetro
	/// </summary>
	/// <param name="direction">Direction.</param>
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

    } // end LookThrough()

	/*void OnAnimatorMove()
	{
		//velocity = Mathf.Abs(new Vector2(controller.velocity.x, controller.velocity.z).magnitude);

		//velocity = Mathf.SmoothStep(0, 1 , Time.deltaTime * speed / 10);
		mecanim.SetFloat("Speed", velocity/*Input.GetAxis("Vertical") * Input.GetAxis("Horizontal")*///);
		/*mecanim.SetBool("OnFloor", controller.isGrounded);
	}*/
}


// GIRO CON MOUSE
// Arreglar giro estraño





























