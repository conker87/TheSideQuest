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

	public void SetHealthToMax() {

		CurrentHealth = MaximumHealth;

	}

	public void HealCurrentHealth(int value = 1) {

		if (value < 0) {

			Debug.Log ("Do not use PlayerDetails::HealCurrentHealth as a damage.");
			return;

		}

		if (CurrentHealth + value > MaximumHealth) {
			
			Debug.Log ("PlayerDetails::HealCurrentHealth -- CurrentHealth + value > MaximumHealth");
			return;

		}

		if (value.Equals (1)) {

			CurrentHealth++;

		} else {

			CurrentHealth += value;

		}
			
		CurrentHealth = Mathf.Clamp (CurrentHealth, 0, MaximumHealth);

	}

	public void DamageCurrentHealth(int value = 1, bool _ignoreInvulnerabilityFrames = false) {

		if (value < 0) {

			Debug.Log ("Do not use PlayerDetails::DamageCurrentHealth as a heal.");
			return;

		}

		if (_isCurrentlyInInvulnerabilityFrames && !_ignoreInvulnerabilityFrames) {

			return;

		}

		if (value.Equals (1)) {

			CurrentHealth--;

		} else {

			CurrentHealth -= value;

		}

		if (HasInvulnerabilityFrames) {

			_isCurrentlyInInvulnerabilityFrames = true;
			_invulnerabilityFramesTime = Time.time + InvulnerabilityFramesLength;

		}

		CurrentHealth = Mathf.Clamp (CurrentHealth, 0, MaximumHealth);

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

		MaximumHealth = Mathf.Clamp (MaximumHealth, BEGINNING_MAXIMUM_HEALTH, TOTAL_MAXIMUM_HEALTH);

	}

	[SerializeField]
	bool _hasInvulnerabilityFrames = false;
	bool _isCurrentlyInInvulnerabilityFrames;
	public bool HasInvulnerabilityFrames {

		get				{ return _hasInvulnerabilityFrames; }
		protected set	{ _hasInvulnerabilityFrames = value; }

	}

	[SerializeField]
	float _invulnerabilityFramesLength = 1.5f;
	float _invulnerabilityFramesTime;
	public float InvulnerabilityFramesLength {

		get				{ return _invulnerabilityFramesLength; }
		protected set	{ _invulnerabilityFramesLength = value; }

	}

	protected virtual void Start() {



	}

	protected virtual void Update() {

		if (_isCurrentlyInInvulnerabilityFrames && Time.time > _invulnerabilityFramesTime) {

			_isCurrentlyInInvulnerabilityFrames = false;

		}

		_currentHealth = Mathf.Clamp (_currentHealth, 0, _maximumHealth);
		_maximumHealth = Mathf.Clamp (_maximumHealth, BEGINNING_MAXIMUM_HEALTH, TOTAL_MAXIMUM_HEALTH);

	}

}
