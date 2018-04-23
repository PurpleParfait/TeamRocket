using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	public SerialController serialController;
	private Rigidbody body;
 	private float MaxSpeed = 100f;
 	private float Acceleration = 5f;	
 	private float Deceleration = -5f;
	private bool playerControl = true;

	const int MaxTightenings = 1001;
    int Tightenings = 0;

    private void SendSerialMessage(string letter)
    {
		if (playerControl == false) return;
        switch (letter)
        {
            case "A":
                if (Tightenings < MaxTightenings){
                    Tightenings++;
                    serialController.SendSerialMessage("A");
                }
				break;

            case "D":
                if (Tightenings > 0){
                    Tightenings--;
                    serialController.SendSerialMessage("D");
                }
				break;

            case "X":
                Tightenings = 0;
                serialController.SendSerialMessage("X");
				break;
			
			case "B":
				if (Tightenings + 100 < MaxTightenings){
					Tightenings += 100;
					serialController.SendSerialMessage("B");
				}
				else if (Tightenings < MaxTightenings){
					Tightenings = MaxTightenings;
					serialController.SendSerialMessage("C");
				}
				break;
			
			case "O":
				if (Tightenings - 100 > 0){
					Tightenings -= 100;
					serialController.SendSerialMessage("O");
				}
				else if (Tightenings > 0){
					Tightenings = 0;
					serialController.SendSerialMessage("P");
				}
				break;
        }
    }

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
		serialController.SetTearDownFunction(shutDown);
	}
	
	void FixedUpdate () {
		if (Input.GetKey("w")){
			if (body.velocity.z > MaxSpeed)
				body.velocity = new Vector3(body.velocity.x, 0, MaxSpeed);
			else if (!(body.velocity.z == MaxSpeed)){
				body.AddForce(0,0,Acceleration);
				SendSerialMessage("A");
			}
		}
		else if (Input.GetKey("s")){
			if (body.velocity.z > 0f){
				body.AddForce(0,0,Deceleration);
				SendSerialMessage("D");
			}

			if (body.velocity.z < 0f){
				body.velocity = Vector3.zero;
				SendSerialMessage("X");
			}
		}

		if (Input.GetKey("a") && body.velocity.z > 0f){
			Debug.Log(body.velocity.z);
			body.velocity = new Vector3(-5f, 0, body.velocity.z);
			Debug.Log(body.velocity.z);
		}
		else if (Input.GetKey("d") && body.velocity.z > 0f){
			body.velocity = new Vector3(5f, 0, body.velocity.z);
		}
		else body.velocity = new Vector3(0,0,body.velocity.z);
	}

	IEnumerator OnTriggerEnter(Collider col){
		if (col.tag == "Obstacle"){
			SendSerialMessage("O");
			playerControl = false;
			body.AddForce(0,0,-500f);
			if (body.velocity.z < 0f)
				body.velocity = Vector3.zero;
			yield return new WaitForSeconds(0.001f);
			playerControl = true;
		}
		else if (col.tag == "Boost"){
			SendSerialMessage("B");
			playerControl = false;
			body.AddForce(0,0,500f);
			if (body.velocity.z > MaxSpeed)
				body.velocity = new Vector3(body.velocity.x, 0, MaxSpeed);
			yield return new WaitForSeconds(0.001f);
			playerControl = true;
		}
	}

	void shutDown(){
		Debug.Log("Executing teardown");
        serialController.SendSerialMessage("X");
	}
}
