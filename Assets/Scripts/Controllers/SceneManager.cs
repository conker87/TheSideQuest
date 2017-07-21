using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

	[SerializeField]
	static List<SaveStation> _saveStationLocations = new List<SaveStation> ();
	public static List<SaveStation> SaveStationLocations {

		get { return _saveStationLocations; }
		protected set { _saveStationLocations = value; }

	}

	public static void AddSaveLocationToSceneManager(SaveStation ss) {

		SaveStationLocations.Add (ss);

	}

}
