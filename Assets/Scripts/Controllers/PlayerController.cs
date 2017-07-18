using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class PlayerController : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	// Wall sliding & jumping
	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	// Dashing
	public Vector2 dash;
	float dashCooldownTime;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	public Controller2D controller;
	PlayerDetails playerDetails;

	SpriteRenderer sr;

	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;

	public bool hasJumped, hasDoubleJumped, hasTripleJumped, hasWallJumped;

	void Start() {
		
		controller = GetComponent<Controller2D> ();
		playerDetails = GetComponent<PlayerDetails> ();
		sr = GetComponent<SpriteRenderer> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

	}

	void Update() {
		
		CalculateVelocity ();

		if (playerDetails.WallSlide) {
		
			HandleWallSliding ();

		}

		sr.flipX = (controller.collisions.faceDir > 0) ? false : true;

		controller.Move (velocity * Time.deltaTime, directionalInput);

		if (controller.collisions.below) {

			hasJumped = hasDoubleJumped = hasTripleJumped = hasWallJumped = false;

		}

		if (controller.collisions.above || controller.collisions.below) {

			if (controller.collisions.slidingDownMaxSlope) {
				
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;

			} else {
				
				velocity.y = 0;

			}

		}

	}

	public void SetDirectionalInput (Vector2 input) {
		
		directionalInput = input;

	}

	public void OnDashInput() {

		if (directionalInput.x == 0f) {

			Debug.Log ("PlayerController::OnDashInput - (directionalInput.x == 0f");
			// return;

		}

		if (!playerDetails.CheatDash && Time.time < dashCooldownTime) {

			Debug.Log ("PlayerController::OnDashInput - Dash is on cooldown");
			return;

		}

		if (playerDetails.Dash) {

			float direction = Mathf.Sign (directionalInput.x);

			velocity.x = direction * dash.x;
			velocity.y = dash.y;

			dashCooldownTime = Time.time + playerDetails.DashCooldown;

		}

	}

	public void OnJumpInputDown() {
		
		if (playerDetails.WallJump && wallSliding) {
			
			if (wallDirX == directionalInput.x) {
				
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;

				hasWallJumped = true;

			} else if (directionalInput.x == 0) {
				
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;

				hasWallJumped = true;

			} else {
				
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;

				hasWallJumped = true;
			}

		}

		if ((controller.collisions.below && !hasJumped) ||
			(playerDetails.DoubleJump && !hasDoubleJumped && hasJumped) ||
			(playerDetails.TripleJump && !hasTripleJumped && hasDoubleJumped && hasJumped)) {
			
			if (controller.collisions.slidingDownMaxSlope) {
				
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;

				}

			} else {
				
				velocity.y = maxJumpVelocity;

			}

			if (hasWallJumped) {

				hasJumped = hasDoubleJumped = hasTripleJumped = true;

			}

			if (!hasTripleJumped && hasDoubleJumped && hasJumped) {
				hasTripleJumped = true;
			}
			if (!hasDoubleJumped && hasJumped) { 
				hasDoubleJumped = true;
			}
			if (!hasJumped) {
				hasJumped = true;
			}

		}

	}
	public void OnJumpInputUp() {

		if (velocity.y > minJumpVelocity) {
			
			velocity.y = minJumpVelocity;

		}

	}
		
	void HandleWallSliding() {
		
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;

		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				
				velocity.y = -wallSlideSpeedMax;

			}

			if (timeToWallUnstick > 0) {
				
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					
					timeToWallUnstick -= Time.deltaTime;

				} else {
					
					timeToWallUnstick = wallStickTime;

				}

			} else {
				
				timeToWallUnstick = wallStickTime;

			}

		}

	}

	void CalculateVelocity() {
		
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;

	}
}
