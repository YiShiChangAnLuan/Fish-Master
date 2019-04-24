using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_Hide : MonoBehaviour {

    public IEnumerator Hide(float delay){
        // 调用协程延迟
        yield return new WaitForSeconds(delay); 
        // 隐藏
        gameObject.SetActive(false);
    }
}
