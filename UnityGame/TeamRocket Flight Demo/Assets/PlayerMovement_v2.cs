using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_v2 : MonoBehaviour {

	public SerialController serialController;
	private Rigidbody body;
	private float MaxSpeed = 100f;
	private float Acceleration = 3f;	
	private float Deceleration = -3f;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
		InvokeRepeating ("updateWingsuitValue", 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		if (Input.GetKey("w")){
			if (body.velocity.z > MaxSpeed)
				body.velocity = new Vector3(body.velocity.x, 0, MaxSpeed);
			else if (!(body.velocity.z == MaxSpeed)){
				body.AddForce(0,0,Acceleration);
			}
		}
		else if (Input.GetKey("s")){
			if (body.velocity.z > 0f){
				body.AddForce(0,0,Deceleration);
			}

			if (body.velocity.z < 0f){
				body.velocity = Vector3.zero;
			}
		}

		if (Input.GetKey("a") && body.velocity.z > 0f){
			body.velocity = new Vector3(-5f, 0, body.velocity.z);
		}
		else if (Input.GetKey("d") && body.velocity.z > 0f){
			body.velocity = new Vector3(5f, 0, body.velocity.z);
		}
		else body.velocity = new Vector3(0,0,body.velocity.z);
	}

	void updateWingsuitValue(){
		int arm_height_L = Mathf.RoundToInt (body.velocity.z * 15);
		int arm_height_R = Mathf.RoundToInt (body.velocity.z * 15);
		SerialSend (arm_height_L, arm_height_R);
	}

	void SerialSend(int message_L, int message_R){

		// FOR MOTOR BACKBACK
		// Send in int values from 0 - 654 for each arm

		message_L = Mathf.Clamp (message_L, 0, 654);
		message_R = Mathf.Clamp (message_R, 0, 654);

		serialController.SendSerialMessage ("L" + message_L);
		serialController.SendSerialMessage ("R" + message_R);
	}

	void OnApplicationQuit()
	{
		Debug.Log("Closing and resetting servos");
		SerialSend (0, 0);
	}

}
