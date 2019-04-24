using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttribute : MonoBehaviour
{
    // 一波鱼生成的最大数量
    public int maxNum;
    // 最大速度
    public int maxSpeed;
    // 鱼的生命值
    public int hp;

    // 经验
    public int exp;
    //金币
    public int gold;

    // 鱼的死亡动画
    public GameObject diePrefab;
    // 鱼死亡后掉落的币
    public GameObject goldPrefab;

    // 判定鱼出界然后销毁
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
    }
    void ReceiveDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            // 使用单例修改GameController中的变量
            GameController.Instance.gold += gold;
            GameController.Instance.exp += exp;
            // 死亡动画初始化
            GameObject die = Instantiate(diePrefab);
            die.transform.SetParent(gameObject.transform.parent, false);
            // 位置
            die.transform.position = transform.position;
            die.transform.rotation = transform.rotation;

            // 死亡动画初始化,添加金币
            GameObject goldObject = Instantiate(goldPrefab);
            goldObject.transform.SetParent(gameObject.transform.parent, false);
            // 位置
            goldObject.transform.position = transform.position;
            goldObject.transform.rotation = transform.rotation;

            // 金鲨和银鲨播放音效
            if (gameObject.GetComponent<Ef_PlayEffect>() != null){
                AudioManager.Instance.PlayEffectSound(AudioManager.Instance.rewardClip);
                gameObject.GetComponent<Ef_PlayEffect>().PlayEffect();
               
            }

            Destroy(gameObject);
        }
    }
}
