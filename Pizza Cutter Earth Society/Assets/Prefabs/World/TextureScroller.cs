using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour
{
	public List<Vector2> scrollSpeeds = new List<Vector2>();

	new Renderer renderer;

	void Start()
	{
		renderer = GetComponentInChildren<Renderer>();
	}
	
	void Update()
	{
		for (int i = 0; i < renderer.materials.Length; ++i)
		{
			Material mat = renderer.materials[i];
			if (i < scrollSpeeds.Count)
			{
				mat.SetTextureOffset("_MainTex", Time.time * scrollSpeeds[i]);
			}
		}
	}
}