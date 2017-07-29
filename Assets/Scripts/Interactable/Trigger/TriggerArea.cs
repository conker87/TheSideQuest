using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : Interactable {

	[SerializeField]
	InteractableTriggers[] _interactableTriggers;
	public InteractableTriggers[] InteractableTriggers {

		get { return _interactableTriggers; } 
		set { _interactableTriggers = value; } 

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected void OnTriggerEnter2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player == null) {

			return;

		}

		foreach (InteractableTriggers trigger in InteractableTriggers) {

			switch (trigger.InteractableTrigger) {

			case InteractableTrigger.OPEN:

				if (trigger.Interactable is Door) {
					trigger.Interactable.SendMessage ("ForceDoorState", DoorState.OPENING, SendMessageOptions.DontRequireReceiver);
				}

				if (trigger.Interactable is Switch) {
					trigger.Interactable.SendMessage ("ForceSwitchState", SwitchState.ON, SendMessageOptions.DontRequireReceiver);
				}

				break;

			case InteractableTrigger.CLOSE:

				if (trigger.Interactable is Door) {
					trigger.Interactable.SendMessage ("ForceDoorState", DoorState.CLOSING, SendMessageOptions.DontRequireReceiver);
				}

				if (trigger.Interactable is Switch) {
					trigger.Interactable.SendMessage ("ForceSwitchState", SwitchState.OFF, SendMessageOptions.DontRequireReceiver);
				}

				break;

			}

		}

	}

}

[System.Serializable]
public struct InteractableTriggers {

	public Interactable Interactable;
	public InteractableTrigger InteractableTrigger;

}

public enum InteractableTrigger { OPEN, CLOSE, TOGGLE }; 