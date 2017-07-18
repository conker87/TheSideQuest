﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	// VITALS INHERITS

	// ABILITIES

	[SerializeField]
	bool _jump = true;
	public bool Jump {

		get { return _jump; }
		set { _jump = value; }

	}

	[SerializeField]
	bool _doubleJump = false;
	public bool DoubleJump {

		get { return _doubleJump; }
		set { _doubleJump = value; }

	}

	[SerializeField]
	bool _tripleJump = false;
	public bool TripleJump {

		get { return _tripleJump; }
		set { _tripleJump = value; }

	}

	[SerializeField]
	bool _wallSlide = false;
	public bool WallSlide {

		get { return _wallSlide; }
		set { _wallSlide = value; }

	}

	[SerializeField]
	bool _wallJump = false;
	public bool WallJump {

		get { return _wallJump; }
		set { _wallJump = value; }

	}

	[SerializeField]
	bool _dashCheat = false, _dash = true, _megaDash = false;
	public bool Dash {

		get { return _dash; }
		set { _dash = value; }

	}
	public bool MegaDash {

		get { return _megaDash; }
		set { _megaDash = value; }

	}
	public bool CheatDash {

		get { return _dashCheat; }
		set { _dashCheat = value; }

	}

	[SerializeField]
	float _dashCooldown = 3f;
	public float DashCooldown {

		get			{ return _dashCooldown; }
		private set	{ _dashCooldown = value; }

	}

	// KEYS
	const int MINIMUM_KEY_COUNT = 0, MAXIMUM_KEY_COUNT = 10; // TODO: Decide whether to reduce this value to use it in a puzzle?
	const int GAME_DIRECTIONS = 7;

	[SerializeField]
	int[] _keys = new int[GAME_DIRECTIONS];
	public int[] Keys {

		get { return _keys; }
		set { _keys = value; }

	}
		
	public int GetKeyCount(KeyLocation location) {

		return Keys [(int) location];

	}

	public bool DecrementKeyCount(KeyLocation location, int value = 1) {

		ItterateThroughKeys ();

		if (value < 1) {

			Debug.Log ("Do not use PlayerDetails::DecrementKeyCount as an increment.");
			return false;

		}

		if (Keys [(int) location] - value < MINIMUM_KEY_COUNT) {

			Debug.Log ("Keys at Location: " + location.ToString() + " has hit MINIMUM_KEY_COUNT (" + MINIMUM_KEY_COUNT + ")");
			return false;

		} else {
		
			Keys [(int) location]--;

		}

		ItterateThroughKeys ();
		return true;

	}

	public bool IncrementKeyCount(KeyLocation location, int value = 1) {

		if (value < 1) {

			Debug.Log ("Do not use PlayerDetails::IncrementKeyCount as a decrement.");
			return false;

		}

		if (Keys [(int) location] + value > MAXIMUM_KEY_COUNT) {

			Debug.Log ("Keys at Location: " + location.ToString() + " has hit MAXIMUM_KEY_COUNT (" + MAXIMUM_KEY_COUNT + ")");
			return false;

		} else {

			Keys [(int) location]++;

		}

		ItterateThroughKeys ();
		return true;

	}

	void ItterateThroughKeys() {

		string debugLog = "PlayerDetails::ItterateThroughKeys -- Values: ";

		for (int i = 0; i < Keys.Length; i++) {

			debugLog += i + ": " + Keys [i] + ", ";

		}

		Debug.Log (debugLog);

	}

	[SerializeField]
	bool[] _artifacts = new bool[GAME_DIRECTIONS];
	public bool[] Artifacts {

		get { return _artifacts; }
		set { _artifacts = value; }

	}

	public bool GetArtifact(KeyLocation location) {

		return Artifacts [(int) location];

	}

	// TODO: Decide whether or not I really care if the player already has the artifact.
	public bool SetArtifact(KeyLocation location) {

		if (Artifacts [(int) location].Equals (true)) {

			Debug.Log ("layerDetails::SetArtifact - player already has artifact, hax?");
			return false;

		} else {

			Artifacts [(int) location] = true;

		}

		ItterateThroughArtifacts ();
		return true;

	}

	void ItterateThroughArtifacts() {

		string debugLog = "PlayerDetails::ItterateThroughArtifacts -- Values: ";

		for (int i = 0; i < Artifacts.Length; i++) {

			debugLog += i + ": " + Artifacts [i] + ", ";

		}

		Debug.Log (debugLog);

	}

















	// Make sure to load these in on LoadGame!

	public void LoadSettings() {

		// blah

	}

}

public enum KeyLocation { SOUTHWEST = 0, WEST = 1, NORTHWEST = 2, NORTH = 3, NORTHEAST = 4, EAST = 5, SOUTHEAST = 6 };