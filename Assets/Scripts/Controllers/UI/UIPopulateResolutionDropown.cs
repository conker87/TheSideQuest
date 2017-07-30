using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopulateResolutionDropown : MonoBehaviour {
	
	public Dropdown ResolutionD;

	Resolution[] resolutions;

	void Start() {

		resolutions = Screen.resolutions;

		for (int i = 0; i < resolutions.Length; i++) {
			
			ResolutionD.options.Add (new Dropdown.OptionData (ResToString (resolutions [i])));

			ResolutionD.value = i;

			ResolutionD.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[ResolutionD.value].width, resolutions[ResolutionD.value].height, true);});

		}
	}

	// this goes outside of "void start". it puts resolution options into a string to be displayed in drop menu
	string ResToString(Resolution res) {
		
		return res.width + " x " + res.height;

	}

}
