using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstPersonController : MonoBehaviour {

	//movements of the player/camera
	public float movementSpeed = 4.0f; //arrow speed
	public float mouseSensitivity = 5.0f; //mouse speed

	public int timeLeft = 240;
	public Text countdownText;

	// Use this for initialization
	void Start()
	{
		StartCoroutine("LoseTime");
	}
	// Update is called once per frame
	void Update (){
		//Rotation
		float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate (0, rotLeftRight, 0);

		//Movement
		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;
		Vector3 speed = new Vector3 (sideSpeed, 0, forwardSpeed);

		speed = transform.rotation * speed;

		CharacterController cc = GetComponent<CharacterController> ();
		cc.SimpleMove( speed );

		//CountDown
		countdownText.text = ("Time Left: " + timeLeft);

		if (timeLeft <= 0)
		{
			StopCoroutine("LoseTime");
			countdownText.text = "Times Up!";
			SceneManager.LoadScene("GameOver");

		}
        
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "finish") {
			SceneManager.LoadScene("YouWon");
		}
	}

	IEnumerator LoseTime()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			timeLeft--;
		}
	}
}
