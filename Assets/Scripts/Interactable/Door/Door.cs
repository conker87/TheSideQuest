using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {

	[SerializeField]
	DoorOperator _doorOperator;
	public DoorOperator DoorOperator {

		get { return _doorOperator; }
		private set { _doorOperator = value; }

	}

	[SerializeField]
	float _resetInSeconds = 0;
	float _resetTimer;
	bool _isResetDoor;
	public float ResetInSeconds {

		get { return _resetInSeconds; }
		set { _resetInSeconds = value; }

	}

	int _doorOperatorCount, _doorOperatorCountTotal = 0;


	public override void DoInteraction (bool sentFromPlayerInput = false)
	{
		base.DoInteraction (sentFromPlayerInput);

		if (GetOperatorCount () >= _doorOperatorCountTotal) {

			print ("Door will open");

		}

	}

	public void IncrementTotalOperatorCount() {

		_doorOperatorCountTotal++;

	}

	public int GetTotalOperatorCount() {

		return _doorOperatorCountTotal;

	}

	public void IncrementOperatorCount() {

		_doorOperatorCount++;

	}

	public void DecrementOperatorCount() {

		_doorOperatorCount--;

	}

	public int GetOperatorCount() {

		return _doorOperatorCount;

	}

	public void ResetOperatorCount () {

		_doorOperatorCount = 0;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum DoorOperator { OR, AND }