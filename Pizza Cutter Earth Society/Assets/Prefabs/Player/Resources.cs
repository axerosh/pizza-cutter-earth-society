using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Assertions;

public enum ResourceTypes { NONE, VESUVIUM, HAWAIIUM, FUNGHITE, SALAMITE, CAPRICCIONIUM, KEBABITE };


public class Resources : MonoBehaviour
{

    public List<GameObject> pickups;

	public static Color GetColor(ResourceTypes resource)
	{
		switch (resource)
		{
			case ResourceTypes.NONE:
				return Color.white;
			case ResourceTypes.VESUVIUM:
				return Color.red;
			case ResourceTypes.HAWAIIUM:
				return Color.yellow;
			case ResourceTypes.FUNGHITE:
				return new Color(0, 1, 11 / 255.0f);
			case ResourceTypes.SALAMITE:
				return new Color(197 / 255.0f, 64 / 255.0f, 90 / 255.0f);
			case ResourceTypes.CAPRICCIONIUM:
				return new Color(0, 1, 245 / 255.0f);
			case ResourceTypes.KEBABITE:
				return new Color(1, 0, 213 / 255.0f);
			default:
				Debug.LogError("Resourcetype " + resource.ToString() + " is not supported");
				return Color.black;
		}
	}
}
