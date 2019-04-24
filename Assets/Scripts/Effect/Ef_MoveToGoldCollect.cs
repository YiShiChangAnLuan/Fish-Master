using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_MoveToGoldCollect : MonoBehaviour {

    // 赋值为金币收集器
    private GameObject goldCollect;

	void Start () {
        goldCollect = GameObject.Find("GoldCollect");
	}

	void Update () {
        // 移动到金币收集器
        transform.position = Vector3.MoveTowards(transform.position, 
                                                 goldCollect.transform.position, 5 * Time.deltaTime);
	}
}
