using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

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


}

public enum LevelDirection { SOUTHWEST = 0, WEST = 1, NORTHWEST = 2, NORTH = 3, NORTHEAST = 4, EAST = 5, SOUTHEAST = 6, SOUTH = 7, CENTER = 8 };