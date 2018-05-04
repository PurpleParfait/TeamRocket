using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_v3 : MonoBehaviour {

	public SerialController serialController;
	private Rigidbody body;
	private float MaxSpeed = 100f;
	private float RotationSpeed = 30f;
	//private float Acceleration = 5f;	
	//private float Deceleration = -5f;
	Vector3 EulerAngleVelocity;

	// Use this for initialization
	void Start () {
		Screen.fullScreen = !Screen.fullScreen;
		body = GetComponent<Rigidbody>(); //play with physics attributes to simulate soaring
		serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
		InvokeRepeating ("updateWingsuitValue", 1.0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		//accelerate forward
		if (Input.GetKey("w")){
			MaxSpeed = 150f;
		}
		//decelerate, stop fully once speed reaches certain level
		else if (Input.GetKey("s")){
			MaxSpeed = 50f;
		}
		else MaxSpeed = 50f;
		//move left
		if (Input.GetKey("a")) {
			//body.velocity = new Vector3(-5f, 0, body.velocity.z);
			AddRoll(1.0f);
		}
		//move right
		else if (Input.GetKey("d")){
			AddRoll(-1.0f);
		}

		//tilt on z-axis
		//perhaps this can be triggered by the green and red blocks
		/*
		if (Input.GetKey ("q")) {
			EulerAngleVelocity = new Vector3 (0, 0, 10);
			Quaternion deltaRotation = Quaternion.Euler (EulerAngleVelocity * Time.deltaTime);
			body.MoveRotation (body.rotation * deltaRotation);
		} else if (Input.GetKey ("e")) {
			EulerAngleVelocity = new Vector3 (0, 0, -10);
			Quaternion deltaRotation = Quaternion.Euler (EulerAngleVelocity * Time.deltaTime);
			body.MoveRotation (body.rotation * deltaRotation);
		}*/
		updateYawFromRoll();
		body.MovePosition(body.position + transform.forward * Time.deltaTime * MaxSpeed);
	}
	//control individual arms
	//not sure how to connect unity to arduino but use these functions to start
	//takes body object that has been manipulated in fixedUpdate fxn, configures and sends
	//as ints to SerialSend
	void updateWingsuitValue(){
		//use body.moveRotation for this instead of velocity
		//or EulerAngleVelocity.z
		//print("updateWingsuitValue");
		//print(transform.rotation);
		int arm_height_L = Mathf.RoundToInt (326 - (transform.rotation.z * 400)); //why 15?
		int arm_height_R = Mathf.RoundToInt (326 + (transform.rotation.z * 400));
		SerialSend (arm_height_L, arm_height_R);
	}

	void updateYawFromRoll(){
		float upSign = 1;

		if (transform.up.y < 0)
		{
			upSign = -1;
		}

		float yawSensibility = 0.33f;
		Vector3 rightNoY = transform.right;
		rightNoY.y = 0;
		rightNoY.Normalize();
		float dot = Vector3.Dot(transform.up, rightNoY);

		yawSensibility *= dot * upSign;

		Quaternion rotator = Quaternion.AngleAxis(yawSensibility, Vector3.up);
		transform.rotation = rotator * transform.rotation;
	}

	void AddRoll(float RollChange){
		//print("addroll");
		RollChange *= Time.deltaTime * RotationSpeed;
		Quaternion rotator = Quaternion.AngleAxis(RollChange, transform.forward);
		transform.rotation = rotator * transform.rotation;
	}
	//only takes two ints- can probably be sent in any part of the program
	//can run game w/o Arduino connection, play w/ message_L and message_R and get that working in Unity
	//test tomorrow with hardware
	void SerialSend(int message_L, int message_R){

		// FOR MOTOR BACKBACK
		// Send in int values from 0 - 654 for each arm

		message_L = Mathf.Clamp (message_L, 0, 654);
		message_R = Mathf.Clamp (message_R, 0, 654);

		//these "L" and "R" chars are how MotorParty.ino knows which int goes to which motor
		serialController.SendSerialMessage ("L" + message_L);
		serialController.SendSerialMessage ("R" + message_R);
	}

	void OnApplicationQuit()
	{
		Debug.Log("Closing and resetting servos");
		SerialSend (0, 0); 
	}

}
