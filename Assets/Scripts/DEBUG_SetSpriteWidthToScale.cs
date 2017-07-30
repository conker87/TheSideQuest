using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DEBUG_SetSpriteWidthToScale : MonoBehaviour {

	SpriteRenderer sr;

	void Start () {

		sr = GetComponent<SpriteRenderer> ();
		sr.size = Vector2.one;

	}

}
