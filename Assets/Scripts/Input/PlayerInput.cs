using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
public class PlayerInput : MonoBehaviour {

	// TODO: Maybe move the double jump code to PlayerController? Leave PlayerInput as a pure input class.

	PlayerController player;
	PlayerDetails playerDetails;

	public bool hasJumped = false, hasDoubleJumped = false, hasTripleJumped = false, hasWallJumped;

	void Start () {
		
		player = GetComponent<PlayerController> ();
		playerDetails = GetComponent<PlayerDetails> ();

	}

	void Update () {

		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (!player.controller.collisions.below) {

			if (!player.hasWallJumped) {

				if (Input.GetKeyDown (KeyCode.Space)) {

					player.OnJumpInputDown ();

				}

				if (Input.GetKeyUp (KeyCode.Space)) {

					player.OnJumpInputUp ();

					hasJumped = true;

				}

			} else {

				if (hasJumped) {

					if (playerDetails.TripleJump && !hasTripleJumped && hasDoubleJumped && hasJumped) {

						if (Input.GetKeyDown (KeyCode.Space)) {

							player.OnJumpInputDown ();

							hasTripleJumped = true;

						}

					}

					if (playerDetails.DoubleJump && !hasDoubleJumped && hasJumped) {

						if (Input.GetKeyDown (KeyCode.Space)) {

							player.OnJumpInputDown ();

							hasDoubleJumped = true;

						}

					}

					if (Input.GetKeyUp (KeyCode.Space)) {

						player.OnJumpInputUp ();

					}

				}

			}

		} else {

			if (playerDetails.Jump && !hasJumped) {
				
				if (Input.GetKeyDown (KeyCode.Space)) {
					
					player.OnJumpInputDown ();

				}

				if (Input.GetKeyUp (KeyCode.Space)) {
					
					player.OnJumpInputUp ();

					hasJumped = true;

				}

			}

		}

		if (player.controller.collisions.below) {

			hasJumped = hasDoubleJumped = hasTripleJumped = false;

		}

	}
}
