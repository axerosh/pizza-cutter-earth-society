using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressUI : MonoBehaviour
{
	public ResourceUI resourcePrefab;

	private BuildingPlot buildingPlot;
	private Dictionary<ResourceTypes, ResourceUI> resourceLabels;

	void Update()
	{
		foreach (var resource in resourceLabels)
		{
			resource.Value.SetResource(resource.Key, buildingPlot.gatheredResources[resource.Key] + "/" + buildingPlot.buildRequirements[resource.Key]);
		}
	}

	public void SetPlot(BuildingPlot plot)
	{
		buildingPlot = plot;

		resourceLabels = new Dictionary<ResourceTypes, ResourceUI>();
		foreach (var req in plot.buildRequirements)
		{
			ResourceUI resourceUI = Instantiate(resourcePrefab, Vector3.zero, Quaternion.identity, transform);
			resourceUI.gameObject.SetActive(true);
			resourceUI.Init();
			resourceLabels.Add(req.Key, resourceUI);
		}
	}
}
