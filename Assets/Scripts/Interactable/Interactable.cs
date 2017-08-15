using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class Interactable : MonoBehaviour {

	[SerializeField]
	string _interactableName;
	public string InteractableID {

		get { return _interactableName; } 
		set { _interactableName = value; } 

	}

	[SerializeField]
	bool _isCurrentlyInteractable = true;
	public bool IsCurrentlyInteractable {

		get { return _isCurrentlyInteractable; } 
		set { _isCurrentlyInteractable = value; } 

	}

	[SerializeField]
	bool _isCurrentlyBusy = false;
	public bool IsCurrentlyBusy {

		get { return _isCurrentlyBusy; } 
		set { _isCurrentlyBusy = value; } 

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

		if (InteractableID == "") {

			Debug.LogWarning ("Interactable::Start -- '" + gameObject.name + "' at " + transform.position.ToString() +
				" has blank InteractableID! It really shouldn't. Setting ID to GameObject name.");
			InteractableID = gameObject.name;

		}

		if (IsOneUseOnly) {

			SaveStateToFile = true;

		}

		isResetInteractable = (ResetInSeconds > 0);

		RenameGameObject ();

	}
	
	protected virtual void Update () {

	}

	public virtual void DoInteraction(bool sentFromPlayerInput = false) {

		CheckInteraction ();

  	}

	protected void CheckInteraction() {

		_canContinue = true;

		if (!IsCurrentlyInteractable) {

			UIController.instance.ShowInformationText ("This device is disabled.", new Color(1f, 0f, 0f, 1f), new Color(1f, 0f, 0f, 0f));
			_canContinue = false;

		}

		if (IsCurrentlyBusy) {

			UIController.instance.ShowInformationText ("This device is busy [PH][DNU].", new Color(1f, 1f, 0f, 1f), new Color(1f, 1f, 0f, 0f));
			_canContinue = false;

		}

		if (IsOneUseOnly && HasBeenUsedOnce) {

			UIController.instance.ShowInformationText ("This device is now disabled.", new Color(1f, 0f, 0f, 1f), new Color(1f, 0f, 0f, 0f));
			_canContinue = false;

		}

	}

	protected virtual void RenameGameObject() {

	}

}
