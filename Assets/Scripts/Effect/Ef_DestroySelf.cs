using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_DestroySelf : MonoBehaviour {

    // 消失时间
    public float delay = 1f;
	private void Start()
	{
        Destroy(gameObject, delay);
	}
}
