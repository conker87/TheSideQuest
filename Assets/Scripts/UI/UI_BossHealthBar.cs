using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHealthBar : MonoBehaviour {

	[SerializeField]
	Enemy enemy = null;

	public Image HealthBar;
	public Text BossName;

	void Update() {
		
		if (enemy != null) {
			
			HealthBar.fillAmount = ((float) enemy.CurrentHealth / (float) enemy.MaximumHealth);

		}

	}

	public void SetEnemy(Enemy e) {

		enemy = e;

	}

}
