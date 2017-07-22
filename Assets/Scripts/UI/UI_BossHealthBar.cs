using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHealthBar : MonoBehaviour {

	[SerializeField]
	Enemy enemy;

	public RectTransform HealthBar;
	public Text BossName;

	void Update() {
		
		if (enemy != null) {
			
			SetScaleToPercentage (HealthBar, ((float) enemy.CurrentHealth / (float) enemy.MaximumHealth));

		}

	}

	public void SetEnemy(Enemy e) {

		enemy = e;

	}

	public void SetScaleToPercentage(RectTransform scale, float value) {

		float percentage = Mathf.Clamp01 (value);

		Vector2 newScale = new Vector2 (percentage, 1f);

		scale.localScale = newScale;

	}

}
