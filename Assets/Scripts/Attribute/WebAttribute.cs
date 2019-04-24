using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAttribute : MonoBehaviour
{
    // 消失时间
    public float disapperTime;
    // 网的伤害
    public int damage;

    private void Start()
    {
        // 时间一到就销毁自己
        Destroy(gameObject, disapperTime);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Fish")
        {
            // 给鱼发消息让鱼受伤，传入伤害值
            collision.SendMessage("ReceiveDamage", damage);
        }
	}

}
