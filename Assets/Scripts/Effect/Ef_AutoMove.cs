using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_AutoMove : MonoBehaviour {

    // 移动的速度
    public float speed = 1f;
    // 移动的方向
    public Vector3 dir = Vector3.right;

	void Start () {
		
	}
	

	void Update () {
        // 自动移动
        transform.Translate(dir * speed * Time.deltaTime);
	}
}
