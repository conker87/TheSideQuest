using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEntity : Door {

	[SerializeField]
	List<Enemy> _entitiesToKill = new List<Enemy>();
	public List<Enemy> EntitiesToKill {

		get { return _entitiesToKill; }
		set { _entitiesToKill = value; }

	}

	[SerializeField]
	bool _autoOpenDoor;
	public bool AutoOpenDoor {

		get { return _autoOpenDoor; }
		set { _autoOpenDoor = value; }

	}

	float entityChecksPerSecond = 2f, entityCheckTime;

	protected override void Start() {

		base.Start ();

		FindEnemiesInRoom ();

	}

	protected override void Update () {

		DoReset ();

		if (!RoomDoorIsIn.IsCurrentlyInRoom) {

			return;

		}

		if (AutoOpenDoor && Time.time > entityCheckTime) {

			if (EntitiesToKill.Count == 0) {

				FindEnemiesInRoom ();

			}

			IsOn = CheckForKilledEntities();

			print ("AutoOpenDoor && Time.time > entityCheckTime " + IsOn);

			entityCheckTime = Time.time + (1f / entityChecksPerSecond);

		}

		doorAnimator.SetBool ("isOn", IsOn);

	}

	public override void DoInteraction (bool sentFromPlayerInput = false) {

		CheckInteraction ();

		if (!_canContinue) return;

		if (IsOneUseOnly) HasBeenUsedOnce = true;

		switch (IsOn) {

		case true: // Door is OPEN

			if (!CheckForPlayerInRadius ()) {
				IsOn = false;
			}
			break;

		case false:

			if (!IsCurrentlyLocked && CheckForKilledEntities()) {

				if (IsCurrentlyLocked) {

					IsCurrentlyLocked = false;

				}

				StartResetTimer ();
				IsOn = true;

			}
				
			break;

		}
			
	}

	bool CheckForKilledEntities() {

		int entityCount = 0;

		foreach (Entity e in EntitiesToKill) {

			if (e.isActiveAndEnabled == false) {

				entityCount++;

			} else {

				entityCount = 0;

			}

		}

		return (entityCount == EntitiesToKill.Count) ? true : false;

	}

	void FindEnemiesInRoom() {

		if (RoomDoorIsIn != null) {

			foreach (RoomSpawnEnemy rpe in RoomDoorIsIn.EnemiesInRoom) {

				foreach (Enemy e in RoomDoorIsIn.GetComponentsInChildren<Enemy>()) {

					if (rpe.RequiredToOpenDoor && e.GetInstanceID() == rpe.EnemyID) {

						EntitiesToKill.Add (e);

					}

				}

			}

			IsCurrentlyInteractable = (EntitiesToKill.Count == 0) ? false : true;

		} else {

			Debug.LogError (string.Format("Interactable::Door::Entity -- RoomDoorIsIn is null on {0} and will be disabled.", InteractableID));
			IsCurrentlyInteractable = false;

		}

	}

}