using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	const int BEGINNING_MAXIMUM_HEALTH = 3, TOTAL_MAXIMUM_HEALTH = 10;

	[SerializeField]
	int _currentHealth = BEGINNING_MAXIMUM_HEALTH;
	public int CurrentHealth {

		get { return _currentHealth; }
		protected set { _currentHealth = value; }

	}

	[SerializeField]
	int _maximumHealth = BEGINNING_MAXIMUM_HEALTH;
	public int MaximumHealth {

		get { return _maximumHealth; }
		protected set { _maximumHealth = value; }

	}

	public void HealCurrentHealth(int value = 1) {

		if (value < 0) {

			Debug.Log ("Do not use PlayerDetails::HealCurrentHealth as a damage.");
			return;

		}

		if (value.Equals (1)) {

			CurrentHealth++;

		} else {

			CurrentHealth += value;

		}

	}

	public void DamageCurrentHealth(int value = 1) {

		if (value < 0) {

			Debug.Log ("Do not use PlayerDetails::DamageCurrentHealth as a heal.");
			return;

		}

		if (value.Equals (1)) {

			CurrentHealth--;

		} else {

			CurrentHealth -= value;

		}

	}

	public void IncreaseMaximumHealth(int value = 0) {

		if (MaximumHealth + value > TOTAL_MAXIMUM_HEALTH) {

			Debug.Log ("PlayerDetails::IncreaseMaximumHealth - MaximumHealth + value > TOTAL_MAXIMUM_HEALTH");
			return;

		}

		if (value.Equals (0)) {

			MaximumHealth++;

		} else {

			MaximumHealth += value;

		}

	}

}
