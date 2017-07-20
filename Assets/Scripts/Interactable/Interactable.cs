using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
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

	[SerializeField][Range(0, 100)]
	float _resetInSeconds = 0;
	protected float _resetTimer;
	protected bool isResetInteractable;
	public float ResetInSeconds {

		get { return _resetInSeconds; }
		set { _resetInSeconds = value; }

	}

	protected bool _canContinue = true;

	protected Player player;

	protected virtual void Start () {

		player = FindObjectOfType<Player> ();

		if (IsOneUseOnly) {

			SaveStateToFile = true;

		}

		isResetInteractable = (ResetInSeconds > 0);

		RenameGameObject ();

	}
	
	protected virtual void Update () {

	}

	public virtual void DoInteraction(bool sentFromPlayerInput = false) {

		if (!IsCurrentlyInteractable) {

			print ("Interactable::Switch::DoInteraction -- Interactable is disabled.");
			_canContinue = false;

		}

		if (IsOneUseOnly && HasBeenUsedOnce) {

			print ("Interactable::Switch::DoInteraction -- Interactable has been used already and is now disabled.");
			_canContinue = false;

		}

  	}

	protected virtual void RenameGameObject() {

	}

}
