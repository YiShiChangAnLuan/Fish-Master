using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_SeaWave : MonoBehaviour {

    // 将起始位置拿到手
    private Vector3 tmp;

	private void Start()
	{
        // 水花要将
        tmp = -transform.position;
	}

	private void Update()
	{
        transform.position = Vector3.MoveTowards(transform.position, tmp, 10 * Time.deltaTime);
	}
}
