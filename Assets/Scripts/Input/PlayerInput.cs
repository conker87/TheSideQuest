using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
public class PlayerInput : MonoBehaviour {

	PlayerController player;
	public float doubleTapSensativity = .25f;
	float doubleTapTime, doubleTabSign = 0f;
	bool doubleTapStart = false;


	void Start () {
		
		player = GetComponent<PlayerController> ();

	}

	void Update () {

		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (doubleTapTime < Time.time) {

			doubleTapStart = false;

		}

		if (doubleTapStart && Input.GetButtonDown("Horizontal") && Input.GetAxisRaw ("Horizontal") == doubleTabSign) {

			Debug.Log ("PlayerInput::Update - Input.GetAxisRaw (\"Horizontal\") == doubleTabSign ");

			if (doubleTapTime > Time.time) {

				player.OnDashInput ();
				doubleTapStart = false;

			}

		}

		if (Input.GetButtonDown("Horizontal")) {

			doubleTapStart = true;
			doubleTabSign = Mathf.Sign(Input.GetAxisRaw ("Horizontal"));
			doubleTapTime = Time.time + doubleTapSensativity;

		}
			
		if (Input.GetAxisRaw ("Horizontal") != 0f && Input.GetButtonDown("Dash")) {

			player.OnDashInput ();
			doubleTapStart = false;

		}

		if (Input.GetButtonDown ("Jump")) {

			player.OnJumpInputDown ();

		}

		if (Input.GetButtonUp ("Jump")) {

			player.OnJumpInputUp ();

		}

	}
}
