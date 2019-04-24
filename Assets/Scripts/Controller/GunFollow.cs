using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollow : MonoBehaviour {
    // 枪在Canvas中，需要取到Canvas
    public RectTransform UGUICanvas;
    // 方法需要传入Camera，可以用Camera.main代替，但是有多个Camera可能会出现问题
    public Camera mainCamera;
	void Start () {
		
	}
	
    void Update () {
        // 方法返回的计算好的值，鼠标在当前Canvas的坐标
        Vector3 mousePost;
        RectTransformUtility.ScreenPointToWorldPointInRectangle
                            (UGUICanvas,
                             new Vector2(Input.mousePosition.x, Input.mousePosition.y),
                             mainCamera,out mousePost);
        // 获得旋转的角度
        float z;
        // 判断鼠标在炮台的左方还是右方
        if (mousePost.x > transform.position.x)
        {
            // 返回的永远是正值,右边的角度是负值,所以要变号
            z = -Vector3.Angle(Vector3.up, mousePost - transform.position);
        }
        else
        {
            z = Vector3.Angle(Vector3.up, mousePost - transform.position);
        }
        // Quaternion四元数,localRotation只接受四元数
        // Quaternion.Euler:欧拉角对应的四元数
        transform.localRotation = Quaternion.Euler(0, 0, z);
	}
}
