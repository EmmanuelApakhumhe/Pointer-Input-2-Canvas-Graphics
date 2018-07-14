using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

///<summary>Class to analyse pointer's input(Touch or Mouse Input) position on UI elements</summary>
/// 
/// Instructions...
/* 
	To use this class, attach this script as a component to the Canvas GameObject that parents the UI elements.
	Ensure there is a GraphicRaycaster component attached to this canvas GameObject.
	Finally, ensure there be an EventSystem GameObject present somewhere in the hierarchy. you're good to go :)

	NOTE:
	This version of this class can be used with only one Canvas GameObject.
	Ensure this script is attached to only one Canvas GameObject.
	
	To access the public methods of this class, use the singleton "Instance" variable.
*/

[RequireComponent(typeof(GraphicRaycaster))]
public class CanvasGraphicsRaycaster : MonoBehaviour 
{
	public static CanvasGraphicsRaycaster Instance;

	private GraphicRaycaster gCaster;
	private PointerEventData pData;
	private EventSystem eSystem;

	int Count;

	void Awake()
	{
		//Limits instances of this class to this public static "Instance" variable. 
		//Use it from other classes to access public methods of this class.
		if (Instance == null) 
		{
			Instance = this;
		} 
		else if (Instance != this) 
		{
			Destroy (gameObject);
			return;
		}

		gCaster = GetComponent<GraphicRaycaster> ();
		eSystem = GetComponent<EventSystem> ();
	}

	/// <summary>
	/// Determines whether pointer's current position is over any UI element.
	/// </summary>
	/// <param name="PointerInputPosition">Pointer's Input position.</param>
	public bool IsPointerOverUI(Vector2 PointerInputPosition)
	{
		Count = 0;

		pData = new PointerEventData(eSystem);

		pData.position = PointerInputPosition;

		List<RaycastResult> results = new List<RaycastResult>();

		gCaster.Raycast(pData, results);

		bool returnResult = (results.Count > 0) ? true : false;

		return returnResult;
	}

	/// <summary>
	/// Determines whether pointer's current position is over the specified UI layer.
	/// </summary>
	/// <param name="UIlayer">UI layer.</param>
	/// <param name="PointerInputPosition">Pointer's Input position.</param>
	public bool IsPointerOverUI(int UIlayer, Vector2 PointerInputPosition)
	{
		Count = 0;

		pData = new PointerEventData(eSystem);

		pData.position = PointerInputPosition;

		List<RaycastResult> results = new List<RaycastResult>();

		gCaster.Raycast(pData, results);

		foreach (RaycastResult result in results)
		{
			if (result.gameObject.layer == UIlayer) 
			{
				Count++;
			}
		}

		bool returnResult = (Count > 0) ? true : false;

		return returnResult;
	}

	/// <summary>
	/// Determines whether pointer's current position is over the specified UI layerName.
	/// </summary>
	/// <param name="UIlayerName">UI layer name.</param>
	/// <param name="PointerInputPosition">Pointer's Input position.</param>
	public bool IsPointerOverUI(string UIlayerName, Vector2 PointerInputPosition)
	{
		Count = 0;

		pData = new PointerEventData(eSystem);

		pData.position = PointerInputPosition;

		List<RaycastResult> results = new List<RaycastResult>();

		gCaster.Raycast(pData, results);

		foreach (RaycastResult result in results)
		{
			if (result.gameObject.layer == LayerMask.NameToLayer(UIlayerName)) 
			{
				Count++;
			}
		}

		bool returnResult = (Count > 0) ? true : false;

		return returnResult;
	}
		
	/// <summary>
	/// Determines whether pointer's current position is over the specified UI tag.
	/// </summary>
	/// <param name="Tag">Tag name.</param>
	/// <param name="PointerInputPosition">Pointer's Input position.</param>
	public bool IsPointerOverUITag(string Tag, Vector2 PointerInputPosition)
	{
		Count = 0;

		pData = new PointerEventData(eSystem);

		pData.position = PointerInputPosition;

		List<RaycastResult> results = new List<RaycastResult>();

		gCaster.Raycast(pData, results);

		foreach (RaycastResult result in results)
		{
			if (result.gameObject.CompareTag(Tag)) 
			{
				Count++;
			}
		}

		bool returnResult = (Count > 0) ? true : false;

		return returnResult;
	}

	/// <summary>
	/// Returns List of RaycastResult of pointer's current position.
	/// </summary>
	/// <param name="PointerInputPosition">Pointer's Input position.</param>
	public List<RaycastResult> PointerOverUI(Vector2 PointerInputPosition)
	{
		pData = new PointerEventData(eSystem);

		pData.position = PointerInputPosition;

		List<RaycastResult> results = new List<RaycastResult>();

		gCaster.Raycast(pData, results);

		return results;
	}
}
