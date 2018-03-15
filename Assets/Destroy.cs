using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
	private GameObject unitychan;
	public GameObject carPrefab;

	// Use this for initialization
	void Start () {
		
		this.unitychan = GameObject.Find ("unitychan");
	}
	
	// Update is called once per frame
	void Update () {
		if (this.unitychan.transform.position.z -5 > this.transform.position.z) {
			Destroy (this.gameObject);
		}
			
		
	}
}
