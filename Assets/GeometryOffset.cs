using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryOffset : MonoBehaviour {

	public Vector2 offset;

	// Use this for initialization
	void Start () {

		Vector2 current = transform.position;

		transform.position = current + offset;

	}
}
