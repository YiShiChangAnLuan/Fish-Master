using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {

    public void NewGame(){
        // 删除所有的值
        PlayerPrefs.DeleteKey("gold");
        PlayerPrefs.DeleteKey("lv");
        PlayerPrefs.DeleteKey("smallTimer");
        PlayerPrefs.DeleteKey("bigTimer");
        PlayerPrefs.DeleteKey("exp");
        // 加载第二个场景
        SceneManager.LoadScene(1);
    }

    // 继续游戏
    public void ContinueGame(){
        // 直接加载第二个场景
        SceneManager.LoadScene(1);
    }
    public void OnCloseButton()
    {
        // 退出游戏
        Application.Quit();
    }
}
