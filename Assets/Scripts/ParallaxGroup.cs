using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public struct ParallaxObject
{
	public Transform MainObject;
	public Transform LeftObject;
	public Transform RightObject;
	public Vector3 ObjectBounds;
	public float ScrollSpeed;
}

public class ParallaxGroup : MonoBehaviour {

	public Camera TargetCamera;
	public bool AutoTile;

	public List<ParallaxObject> Objects = new List<ParallaxObject>();

	// Use this for initialization
	void Awake () {
		if(AutoTile)
		{
			CreateTiledObjects();
		}
	}

	private void CreateTiledObjects()
	{
		int i = 0,l = Objects.Count;
		for(;i<l;++i)
		{
			ParallaxObject obj = Objects[i];

			Vector3 size = GetSizeOfObject(obj.MainObject);
			obj.ObjectBounds = size;
//			Debug.Log(obj.ObjectBounds);

			GameObject cloneRight = GameObject.Instantiate( obj.MainObject.gameObject ) as GameObject;
			Vector3 pos = obj.MainObject.position;
			pos.x += size.x;
			cloneRight.transform.position = pos;
			obj.RightObject = cloneRight.transform;

			GameObject cloneLeft = GameObject.Instantiate( obj.MainObject.gameObject ) as GameObject;
			pos = obj.MainObject.position;
			pos.x -= size.x;
			cloneLeft.transform.position = pos;
			obj.LeftObject = cloneLeft.transform;

			cloneLeft.transform.SetParent(Objects[i].MainObject);
			cloneRight.transform.SetParent(Objects[i].MainObject);

			Objects[i] = obj;
		}
	}

	private Vector3 GetSizeOfObject(Transform t)
	{
		SpriteRenderer[] renderers = t.GetComponentsInChildren<SpriteRenderer>();
		Bounds b = new Bounds();
		foreach(SpriteRenderer r in renderers)
		{
			b.Encapsulate(r.bounds);
		}

		return b.size;
	}
	
	// Update is called once per frame
	private Vector3 _previousCameraPosition;
	void Update () {
		Vector3 cameraDelta = TargetCamera.transform.position - _previousCameraPosition;

		int i = Objects.Count-1,l = -1;
		int j = 0;
		for(;i>l;i--)
		{
			ParallaxObject obj = Objects[i];
			Vector3 pos = obj.MainObject.position;

			if(j == 0)
			{
				pos.x += cameraDelta.x;
			}
			else
			{
				pos.x += cameraDelta.x*(-(obj.ScrollSpeed-1));
			}

			obj.MainObject.position = pos;

			TilePosition(obj);
			j++;
		}

		_previousCameraPosition = TargetCamera.transform.position;
	}

	private void TilePosition(ParallaxObject obj)
	{
		Vector3 pos = obj.MainObject.position;
		if(AutoTile)
		{
			float p = TargetCamera.transform.position.x - pos.x;
			if(p > (obj.ObjectBounds.x/2))
			{
				pos.x += obj.ObjectBounds.x;
			}
			
			if(p < -(obj.ObjectBounds.x/2))
			{
				pos.x -= obj.ObjectBounds.x;
			}
		}
		obj.MainObject.position = pos;
	}
}
