using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Constants : MonoBehaviour {

	public static Color[] LevelDirectionColor = {
		new Color(85f / 255f, 107f / 255f, 247f / 255f),
		Color.blue,
		Color.cyan,
		Color.green,
		Color.magenta,
		Color.red,
		new Color(247f / 255f, 107f / 255f, 85f / 255f),
		Color.yellow
	};

	public static IEnumerator FadeTextElement(Text element, float totalTime, Color startColor, Color endColor, bool waitForSeconds = false, float waitForSecondsT = 0f) {

		if (element == null) {

			yield return null;

		}

		if (waitForSeconds) {

			yield return new WaitForSeconds (waitForSecondsT);

		}

		float elapsedTime = 0f;

		while(elapsedTime < totalTime) {

			elapsedTime += Time.deltaTime;
			element.color = Color.Lerp(startColor, endColor, elapsedTime/totalTime);
			yield return null;

		}

	}

	public static IEnumerator DisableElement(GameObject element, float seconds) {

		if (element == null) {

			print (element + " is null");

			yield return null;

		}

		yield return new WaitForSeconds (seconds);

		element.SetActive (false);

	}

	public static IEnumerable<T> GetValues<T>() {
		
		return System.Enum.GetValues(typeof(T)).Cast<T>();

	}

}

public enum LevelDirection { SOUTHWEST = 0, WEST = 1, NORTHWEST = 2, NORTH = 3, NORTHEAST = 4, EAST = 5, SOUTHEAST = 6, SOUTH = 7, CENTER = 8 };