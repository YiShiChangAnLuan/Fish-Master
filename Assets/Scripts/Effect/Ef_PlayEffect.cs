using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_PlayEffect : MonoBehaviour
{
    // 持有特效动画
    public GameObject[] effectsPrefabs;
    // 播放特效方法
    public void PlayEffect()
    {
        foreach (GameObject effectsPrefab in effectsPrefabs)
        {
            // 数组里的特效分别实例化
            Instantiate(effectsPrefab);
        }
    }
}