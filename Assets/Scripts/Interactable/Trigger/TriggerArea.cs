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

	[SerializeField]
	int timesToFireStayPerSecond = 8;

	float stayFireTimer;

	protected void OnTriggerEnter2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player == null) {

			return;

		}

		foreach (InteractableTriggers trigger in InteractableTriggers) {

			if (!trigger.onTriggerEnter) {

				continue;

			}

			switch (trigger.InteractableTrigger) {

			case InteractableTrigger.OPEN:

				if (trigger.Interactable is Door) {
					trigger.Interactable.SendMessage ("ForceDoorState", DoorState.OPENING, SendMessageOptions.DontRequireReceiver);
				}

				if (trigger.Interactable is Switch) {
					// (Switch) trigger.Interactable.
					trigger.Interactable.SendMessage ("ForceSwitchState", true, SendMessageOptions.DontRequireReceiver);
				}

				break;

			case InteractableTrigger.CLOSE:

				if (trigger.Interactable is Door) {
					trigger.Interactable.SendMessage ("ForceDoorState", DoorState.CLOSING, SendMessageOptions.DontRequireReceiver);
				}

				if (trigger.Interactable is Switch) {
					trigger.Interactable.SendMessage ("ForceSwitchState", false, SendMessageOptions.DontRequireReceiver);
				}

				break;

			case InteractableTrigger.TOGGLE:

				trigger.Interactable.DoInteraction ();

				break;

			}

		}

	}

	protected void OnTriggerStay2D(Collider2D other) {
		
		player = other.GetComponent<Player>();

		if (player == null) {

			return;

		}

		if (stayFireTimer > Time.time) {

			return;

		}

		foreach (InteractableTriggers trigger in InteractableTriggers) {

			if (!trigger.onTriggerStay) {

				continue;

			}

			switch (trigger.InteractableTrigger) {

			case InteractableTrigger.OPEN:

				if (trigger.Interactable is Door) {
					trigger.Interactable.SendMessage ("ForceDoorState", DoorState.OPENING, SendMessageOptions.DontRequireReceiver);
				}

				if (trigger.Interactable is Switch) {
					trigger.Interactable.SendMessage ("ForceSwitchState", true, SendMessageOptions.DontRequireReceiver);
				}

				break;

			case InteractableTrigger.CLOSE:

				if (trigger.Interactable is Door) {
					trigger.Interactable.SendMessage ("ForceDoorState", DoorState.CLOSING, SendMessageOptions.DontRequireReceiver);
				}

				if (trigger.Interactable is Switch) {
					trigger.Interactable.SendMessage ("ForceSwitchState", false, SendMessageOptions.DontRequireReceiver);
				}

				break;

			case InteractableTrigger.TOGGLE:

				trigger.Interactable.DoInteraction ();

				break;

			}

		}

		stayFireTimer = Time.time + (1f / (float)timesToFireStayPerSecond);

	}

	protected void OnTriggerExit2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player == null) {

			return;

		}

		foreach (InteractableTriggers trigger in InteractableTriggers) {

			if (!trigger.onTriggerExit) {

				continue;

			}

			switch (trigger.InteractableTrigger) {

			case InteractableTrigger.OPEN:

				if (trigger.Interactable is Door) {
					trigger.Interactable.SendMessage ("ForceDoorState", DoorState.OPENING, SendMessageOptions.DontRequireReceiver);
				}

				if (trigger.Interactable is Switch) {
					trigger.Interactable.SendMessage ("ForceSwitchState", true, SendMessageOptions.DontRequireReceiver);
				}

				break;

			case InteractableTrigger.CLOSE:

				if (trigger.Interactable is Door) {
					trigger.Interactable.SendMessage ("ForceDoorState", DoorState.CLOSING, SendMessageOptions.DontRequireReceiver);
				}

				if (trigger.Interactable is Switch) {
					trigger.Interactable.SendMessage ("ForceSwitchState", false, SendMessageOptions.DontRequireReceiver);
				}

				break;

			case InteractableTrigger.TOGGLE:

				trigger.Interactable.DoInteraction ();

				break;

			}

		}

	}

	void OnDrawGizmos() {

		if (InteractableTriggers != null && InteractableTriggers.Length > 0) {

			foreach (InteractableTriggers interactableTrigger in InteractableTriggers) {

				Gizmos.color = Color.blue;
				Gizmos.DrawWireSphere (transform.position, 0.2f);

				Gizmos.color = Color.green;
				Gizmos.DrawLine (transform.position, interactableTrigger.Interactable.transform.position);

			}

		}


	}

}

[System.Serializable]
public struct InteractableTriggers {

	public Interactable Interactable;
	public bool onTriggerEnter, onTriggerStay, onTriggerExit;
	public InteractableTrigger InteractableTrigger;

}

public enum InteractableTrigger { OPEN, CLOSE, TOGGLE }; 