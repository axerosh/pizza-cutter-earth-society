using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
	TextMeshProUGUI nameText, valueText;

	public void SetResource(ResourceTypes type, string value)
	{
		Color resourceColor = Resources.GetColor(type);
		nameText.text = type.ToString() + ":";
		nameText.color = resourceColor;
		valueText.text = value;
		valueText.color = resourceColor;
	}

	public void Init()
	{
		nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
		valueText = transform.Find("ValueText").GetComponent<TextMeshProUGUI>();
	}
}
