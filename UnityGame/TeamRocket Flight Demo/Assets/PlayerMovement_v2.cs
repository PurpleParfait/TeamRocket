using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_v2 : MonoBehaviour {

	public SerialController serialController;
	private Rigidbody body;
	private float MaxSpeed = 100f;
	private float Acceleration = 3f;	
	private float Deceleration = -3f;
	Vector3 EulerAngleVelocity;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>(); //play with physics attributes to simulate soaring
		serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
		InvokeRepeating ("updateWingsuitValue", 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		//accelerate forward
		if (Input.GetKey("w")){
			if (body.velocity.z > MaxSpeed)
				body.velocity = new Vector3(body.velocity.x, 0, MaxSpeed);
			else if (!(body.velocity.z == MaxSpeed)){
				body.AddForce(0,0,Acceleration);
			}
		}
		//decelerate, stop fully once speed reaches certain level
		else if (Input.GetKey("s")){
			if (body.velocity.z > 0f){
				body.AddForce(0,0,Deceleration);
			}

			if (body.velocity.z < 0f){
				body.velocity = Vector3.zero;
			}
		}
		//move leftw
		if (Input.GetKey("a") && body.velocity.z > 0f) {
			body.velocity = new Vector3(-5f, 0, body.velocity.z);
			EulerAngleVelocity = new Vector3 (0, 0, 20);
			Quaternion deltaRotation = Quaternion.Euler (EulerAngleVelocity * Time.deltaTime);
			body.MoveRotation (body.rotation * deltaRotation);
		}
		//move right
		else if (Input.GetKey("d") && body.velocity.z > 0f){
			body.velocity = new Vector3(5f, 0, body.velocity.z);
			EulerAngleVelocity = new Vector3 (0, 0, -20);
			Quaternion deltaRotation = Quaternion.Euler (EulerAngleVelocity * Time.deltaTime);
			body.MoveRotation (body.rotation * deltaRotation);
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
		else body.velocity = new Vector3(0,0,body.velocity.z);
	}
	//control individual arms
	//not sure how to connect unity to arduino but use these functions to start
	//takes body object that has been manipulated in fixedUpdate fxn, configures and sends
	//as ints to SerialSend
	void updateWingsuitValue(){
		//use body.moveRotation for this instead of velocity
		//or EulerAngleVelocity.z
		int arm_height_L = Mathf.RoundToInt (326 - (EulerAngleVelocity.z * 15)); //why 15?
		int arm_height_R = Mathf.RoundToInt (326 + (EulerAngleVelocity.z * 15));
		SerialSend (arm_height_L, arm_height_R);
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
