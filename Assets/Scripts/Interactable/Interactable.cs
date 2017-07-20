using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class Interactable : MonoBehaviour {

	[SerializeField]
	string _interactableName;
	public string InteractableName {

		get { return _interactableName; } 
		set { _interactableName = value; } 

	}

	[SerializeField]
	bool _isCurrentlyInteractable;
	public bool IsCurrentlyInteractable {

		get { return _isCurrentlyInteractable; } 
		set { _isCurrentlyInteractable = value; } 

	}

	[SerializeField]
	bool _isCurrentlyLocked;
	public bool IsCurrentlyLocked {

		get { return _isCurrentlyLocked; } 
		set { _isCurrentlyLocked = value; } 

	}

	[SerializeField]
	bool _isOneUseOnly, _hasBeenUsedOnce;
	public bool IsOneUseOnly {

		get { return _isOneUseOnly; } 
		set { _isOneUseOnly = value; } 

	}
	public bool HasBeenUsedOnce {

		get { return _hasBeenUsedOnce; } 
		set { _hasBeenUsedOnce = value; } 

	}

	[SerializeField]
	bool _saveStateToFile;
	public bool SaveStateToFile {

		get { return _saveStateToFile; } 
		set { _saveStateToFile = value; } 

	}

	protected bool _canContinue = true;

	public virtual void DoInteraction(bool sentFromPlayerInput = false) {

		if (!IsCurrentlyInteractable) {

			print ("Interactable::Switch::DoInteraction -- Interactable is disabled.");
			_canContinue = false;

		}

		if (IsCurrentlyLocked) {

			print ("Interactable::Switch::DoInteraction -- Interactable is currently locked.");
			_canContinue = false;

		}

		if (IsOneUseOnly && HasBeenUsedOnce) {

			print ("Interactable::Switch::DoInteraction -- Interactable has been used already and is now disabled.");
			_canContinue = false;

		}

	}

	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {

		if (IsOneUseOnly) {

			SaveStateToFile = true;

		}

	}

}
