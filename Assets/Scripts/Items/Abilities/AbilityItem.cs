using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityItem : Item {

	[SerializeField]
	string _abilityName;
	public string AbilityName {

		get { return _abilityName; }
		set { _abilityName = value; }

	}

	protected override void OnTriggerEnter2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player != null) {

			if (player.AbilityCollect(AbilityName, true)) {

				SendInformationText ("You have collected: " + AbilityName);
				DisableGameObject (true);

			}

		}

	}

}
