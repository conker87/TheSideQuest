using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponModifierItem : Item {

	[SerializeField]
	int _weaponModifierValue;
	public int WeaponModifierValue {

		get { return _weaponModifierValue; }
		set { _weaponModifierValue = value; }

	}

	protected override void OnTriggerEnter2D(Collider2D other) {

		player = other.GetComponent<Player>();

		if (player != null) {

			player.WeaponProjectileModifier += 0.5f;
				
			DisableGameObject (true);

		}

	}

}




/* 
 * 2 
 * 
 * 
 * 
 */