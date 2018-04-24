using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_v2 : MonoBehaviour {

	public SerialController serialController;
	private Rigidbody body;
	private float MaxSpeed = 100f;
	private float Acceleration = 5f;	
	private float Deceleration = -5f;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
		InvokeRepeating ("SerialSend", 1.0f, 0.5f);
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

	void SerialSend(){
		
		int message = Mathf.RoundToInt(body.velocity.z * 10);
		Debug.Log (message);
		serialController.SendSerialMessage(message.ToString());
	}
}
