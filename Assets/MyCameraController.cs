using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

	private GameObject unitychan;  //Unityちゃんのオブジェクト
	private float difference;  //Unityちゃんとカメラの位置

	// Use this for initialization
	void Start () {
		this.unitychan = GameObject.Find ("unitychan");  //Unityちゃんのオブジェクトを取得
		this.difference=unitychan.transform.position.z-this.transform.position.z;  //Unityちゃんとカメラの位置（z軸）の差を求める

		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (0, this.transform.position.y, this.unitychan.transform.position.z - difference);
	}
}
