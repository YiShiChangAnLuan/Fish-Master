using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttribute : MonoBehaviour {
    // 子弹速度
    public float speed;
    // 子弹伤害
    public int damage;
    // 子弹爆炸后网的形状
    public GameObject webPrefab;

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Border"){
            Destroy(gameObject);
        }
        if (collision.tag == "Fish"){
            // 碰到鱼的时候就实例化网
            GameObject web = Instantiate(webPrefab);

            web.transform.SetParent(gameObject.transform.parent, false);
            web.transform.position = gameObject.transform.position;

            // 获取到网的属性,给网附加伤害
            web.GetComponent<WebAttribute>().damage = damage;

            // 销毁鱼
            Destroy(gameObject);
        }
	}
}
