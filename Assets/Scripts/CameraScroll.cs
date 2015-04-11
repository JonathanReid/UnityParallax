using UnityEngine;
using System.Collections;

public class CameraScroll : MonoBehaviour {

	public float Speed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x += Speed;
		transform.position = pos;
	}
}
