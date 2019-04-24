using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_AutoRotate : MonoBehaviour {
	// 自动旋转的脚本

	public float speed = 1f;

	void Start () {
		
	}
	

	void Update () {
		// 随着时间转动一定角度
		transform.Rotate(Vector3.forward, speed + Time.deltaTime);
	}
}
