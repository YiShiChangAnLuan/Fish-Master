using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_WaterWave : MonoBehaviour {
    // 材质
    public Material material;
    // 材质里面的图片数组
    public Texture[] textures;
    // 当前已经放到图片的编号
    private int index = 0;

	void Start () {
        // 获取当前plane的材质
        material = GetComponent<MeshRenderer>().material;
        // 因为Update是按照一帧一帧计算的，太快了所以使用InvokeRepeating
        // p1:方法名 p2:时间间隔 p3:速率
        InvokeRepeating("changeText", 0, 0.1f);
	}
    // 切换材质里图片的函数
    void changeText(){
        // 主材质里的索引图
        material.mainTexture = textures[index];
        // index按照textures长度循坏
        index = (index + 1) % textures.Length;

    }
	void Update () {
		
	}
}
