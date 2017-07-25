using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DisableGameObjectOnAwake : MonoBehaviour {

	void Awake() {

		gameObject.SetActive (false);

	}

}
