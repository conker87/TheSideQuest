using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class InputManager {

	// TODO: Finish this.

	static List<KeyCode> _bannedKeybinds = new List<KeyCode> () { KeyCode.Break, KeyCode.Pause } ;

	static Dictionary<string, Keybinds> _currentKeybindings = new Dictionary<string, Keybinds>();
	public static Dictionary<string, Keybinds> CurrentKeybindings {

		get { return _currentKeybindings; }
		private set { _currentKeybindings = value; }

	}

	static Dictionary<string, Keybinds> _unsavedKeybindings = new Dictionary<string, Keybinds>();
	public static Dictionary<string, Keybinds> UnsavedKeybindings {

		get { return _unsavedKeybindings; }
		private set { _unsavedKeybindings = value; }

	}

	static bool _isUsingController = false;
	public static bool IsUsingController {

		get { return _isUsingController; }
		private set { _isUsingController = value; }

	}

	public static float GetAxis(string axisName) {

		return Input.GetAxisRaw (axisName);

	}

	public static bool GetButton(string buttonName)  {
		
		if (!CurrentKeybindings.ContainsKey (buttonName)) return false;
			
		return (IsUsingController ? Input.GetKey(CurrentKeybindings[buttonName].ControllerBinds) : Input.GetKey(CurrentKeybindings[buttonName].KeyboardBinds));

	}

	public static bool GetButtonDown(string buttonName) {

		if (!CurrentKeybindings.ContainsKey (buttonName)) return false;

		return (IsUsingController ? Input.GetKeyDown(CurrentKeybindings[buttonName].ControllerBinds) : Input.GetKeyDown(CurrentKeybindings[buttonName].KeyboardBinds));

	}

	public static bool GetButtonUp(string buttonName)  {

		if (!CurrentKeybindings.ContainsKey (buttonName)) return false;

		return (IsUsingController ? Input.GetKeyUp(CurrentKeybindings[buttonName].ControllerBinds) : Input.GetKeyUp(CurrentKeybindings[buttonName].KeyboardBinds));

	}

	public static string[] GetButtonNames() {

		return CurrentKeybindings.Keys.ToArray();

    }

	public static string[] GetNamesForButton( string buttonName ) {
        
		if (CurrentKeybindings.ContainsKey(buttonName) == false) {
			
            Debug.LogError("InputManager::GetKeyNameForButton -- No button named: " + buttonName);
            return null;

        }

		return new string[] { CurrentKeybindings[buttonName].KeyboardBinds.ToString(), CurrentKeybindings[buttonName].ControllerBinds.ToString() } ;
    }

	public static Dictionary<string, Keybinds> GetKeybindings() {

		return CurrentKeybindings;

	}

	public static Dictionary<string, Keybinds> GetUnsavedKeybindings() {

		return UnsavedKeybindings;

	}

	public static void SaveKeybindings() {

		CurrentKeybindings = UnsavedKeybindings;

	}

	// TODO: The PauseManager Controller area should probably use this?
	public static void SetUnsavedKeybindings(string keybindsID, Keybinds keybind) {

		UnsavedKeybindings[keybindsID] = keybind;

	}

	public static void RevertToDefaultBindings() {

		CurrentKeybindings.Clear ();
		UnsavedKeybindings.Clear ();

		CurrentKeybindings.Add("Up", 		new Keybinds(KeyCode.W, 		KeyCode.Break,			false));
		CurrentKeybindings.Add("Down", 		new Keybinds(KeyCode.S, 		KeyCode.Break, 			false));
		CurrentKeybindings.Add("Left", 		new Keybinds(KeyCode.A, 		KeyCode.Break, 			false));
		CurrentKeybindings.Add("Right", 	new Keybinds(KeyCode.D, 		KeyCode.Break, 			false));
		CurrentKeybindings.Add("Jump", 		new Keybinds(KeyCode.Space, 	KeyCode.JoystickButton0,false));	// A
		CurrentKeybindings.Add("Fire", 		new Keybinds(KeyCode.Return, 	KeyCode.JoystickButton3,false));	// Y
		CurrentKeybindings.Add("Dash", 		new Keybinds(KeyCode.E, 		KeyCode.JoystickButton2,false));	// X

		CurrentKeybindings.Add("ResetScene",new Keybinds(KeyCode.P, 		KeyCode.Break,			true));
		CurrentKeybindings.Add("UIBack", 	new Keybinds(KeyCode.Escape,	KeyCode.JoystickButton1,true));
		CurrentKeybindings.Add("Pause",		new Keybinds(KeyCode.Escape, 	KeyCode.JoystickButton7,true));

		UnsavedKeybindings.Add("Up", 		new Keybinds(KeyCode.W, 		KeyCode.Break, 			false));
		UnsavedKeybindings.Add("Down", 		new Keybinds(KeyCode.S, 		KeyCode.Break, 			false));
		UnsavedKeybindings.Add("Left", 		new Keybinds(KeyCode.A, 		KeyCode.Break, 			false));
		UnsavedKeybindings.Add("Right", 	new Keybinds(KeyCode.D, 		KeyCode.Break, 			false));
		UnsavedKeybindings.Add("Jump", 		new Keybinds(KeyCode.Space, 	KeyCode.JoystickButton0,false));	// A
		UnsavedKeybindings.Add("Fire", 		new Keybinds(KeyCode.Return, 	KeyCode.JoystickButton3,false));	// Y
		UnsavedKeybindings.Add("Dash", 		new Keybinds(KeyCode.E, 		KeyCode.JoystickButton2,false));	// X

		UnsavedKeybindings.Add("ResetScene",new Keybinds(KeyCode.P, 		KeyCode.Break,			true));
		UnsavedKeybindings.Add("UIBack", 	new Keybinds(KeyCode.Escape,	KeyCode.JoystickButton1,true));
		UnsavedKeybindings.Add("Pause",		new Keybinds(KeyCode.Escape, KeyCode.JoystickButton7, 	true));

		RevertChangesToCurrent ();

	}

	public static void RevertChangesToCurrent() {

		UnsavedKeybindings = CurrentKeybindings;

	}

	public static void SetButtonForKey_Unsaved(string keybindsID, KeyCode KeyboardBinds, KeyCode ControllerBinds, bool IgnoreInSettings = false) {

		Keybinds newKeybinding = new Keybinds (KeyboardBinds, ControllerBinds, IgnoreInSettings);

		UnsavedKeybindings [keybindsID] = newKeybinding;

    }

}

[Serializable]
public struct Keybinds {

	public Keybinds(KeyCode KeyboardBinds, KeyCode ControllerBinds, bool IgnoreInSettings = false) {

		// this.key_id = Key_ID;
		this.KeyboardBinds = KeyboardBinds;
		this.ControllerBinds = ControllerBinds;
		this.ignoreInSettings = IgnoreInSettings;

	}

	public void Add(KeyCode KeyboardBinds, KeyCode ControllerBinds, bool IgnoreInSettings = false) {

		// this.key_id = Key_ID;
		this.KeyboardBinds = KeyboardBinds;
		this.ControllerBinds = ControllerBinds;
		this.ignoreInSettings = IgnoreInSettings;

	}

	// public string 		key_id;
	public KeyCode		KeyboardBinds;
	public KeyCode		ControllerBinds;
	public bool			ignoreInSettings;

}

public enum KeybindingScheme { KEYBOARD, CONTROLLER };