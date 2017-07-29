using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField]
	float _projectileSpeed;
	public float ProjectileSpeed {

		get { return _projectileSpeed; }
		set { _projectileSpeed = value; }

	}

	[SerializeField]
	int _projectileDamage;
	public int ProjectileDamage {

		get { return _projectileDamage; }
		set { _projectileDamage = value; }

	}

	float _playerModifier;
	public float PlayerModifier {

		get { return _playerModifier; }
		set { _playerModifier = value; }

	}

	[SerializeField]
	Vector2 _projectileDirection;
	public Vector2 ProjectileDirection {

		get { return _projectileDirection; }
		set { _projectileDirection = value; }

  	}

	[SerializeField]
	Projectile _bulletHoleToSpawn;
	public Projectile BulletHoleToSpawn {

		get { return _bulletHoleToSpawn; }
		set { _bulletHoleToSpawn = value; }

	}


	float _despawnTime = 5f;

	Vector2 newPosition;

	void Start() {

		Invoke ("DespawnProjectile", _despawnTime);
		ProjectileDamage = 2;

	}

	void Update () {
		
		if (ProjectileSpeed > 0) {

			newPosition = ProjectileDirection * Time.deltaTime * ProjectileSpeed;

			transform.position = (Vector2) transform.position + newPosition;

		}

	}

	void OnTriggerEnter2D(Collider2D other) {

		Enemy enemyHit = other.GetComponent<Enemy> ();

		if (enemyHit != null) {

			float currentDamage = ProjectileDamage * PlayerModifier * enemyHit.WeaknessModifier;
			enemyHit.DamageCurrentHealth (Mathf.RoundToInt(currentDamage));

			DespawnProjectile ();

		}

		if (other.gameObject.tag == "DoorCollider") {

			DespawnProjectile ();

		}

		if (other.gameObject.tag == "Geometry" && other.GetComponent<Door> () != null) {

			return;

		}

		if (other.gameObject.tag == "Geometry" || other.gameObject.tag == "InteractiveGeometry") {

			DespawnProjectile ();

		}

	}

	void OnTriggerStay2D(Collider2D other) {

		OnTriggerEnter2D (other);

	}

	protected void DespawnProjectile() {

		if (BulletHoleToSpawn != null) {

			Instantiate (BulletHoleToSpawn, transform.position, Quaternion.identity);

		}

		Destroy (gameObject);

	}

}
