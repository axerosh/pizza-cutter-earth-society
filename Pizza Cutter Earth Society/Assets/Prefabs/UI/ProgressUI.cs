using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressUI : MonoBehaviour
{
	public ResourceUI resourcePrefab;
	
	private Building building;
	private Dictionary<ResourceTypes, ResourceUI> resourceLabels;
	private bool hasDetectedBuilt = false;

	void Update()
	{
		if (building)
		{
			transform.position = Camera.main.WorldToScreenPoint(building.transform.position);

				if (building.built)
			{
				// Change to Processing
				if (!hasDetectedBuilt)
				{
					foreach(var oldUI in transform.GetComponentsInChildren<ResourceUI>())
					{
						Destroy(oldUI.gameObject);
					}

					hasDetectedBuilt = true;
					resourceLabels = new Dictionary<ResourceTypes, ResourceUI>();
					offsetY = 0.0f;
					foreach (var resource in building.gatheredProcessingResources)
					{
						AddResourceLabel(resource);
					}
				}

				// Processing
				foreach (var resource in resourceLabels)
				{
					resource.Value.SetResource(resource.Key, building.gatheredProcessingResources[resource.Key].ToString());
				}
			}
			else
			{
				// Construction
				foreach (var resource in resourceLabels)
				{
					resource.Value.SetResource(resource.Key, building.gatheredConstructionResources[resource.Key] + "/" + building.constructionRequirements[resource.Key]);
				}
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void AddResourceLabel(KeyValuePair<ResourceTypes, int> resource)
	{
		ResourceUI resourceUI = Instantiate(resourcePrefab, transform, false);
		resourceUI.transform.localPosition += new Vector3(0, offsetY, 0);
		offsetY += 30.0f;
		resourceUI.gameObject.SetActive(true);
		resourceUI.Init();
		resourceLabels.Add(resource.Key, resourceUI);
	}

	private float offsetY = 0.0f;

	public void Init(Building building)
	{
		this.building = building;
		resourceLabels = new Dictionary<ResourceTypes, ResourceUI>();
		offsetY = 0.0f;
		foreach (var resource in building.constructionRequirements)
		{
			AddResourceLabel(resource);
		}
	}
}
