using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    public GameObject settingPanel;
    public Toggle muteToggle;

	private void Start()
	{
        // 将Toggle同步
        muteToggle.isOn = !AudioManager.Instance.IsMute;
	}

	// 按下之后关闭声音
    // 动态函数，每次值发生改变，都会调用，系统自动将isOn传过来
	public void SwitchMute(bool isOn)
    {
        AudioManager.Instance.SwitchMuteState(isOn);
    }

    // 返回游戏，而且要保存
    public void OnBackButtonDown()
    {
        // Unity提供的保存信息的类
        // 金币
        PlayerPrefs.SetInt("gold", GameController.Instance.gold);
        // 等级
        PlayerPrefs.SetInt("lv", GameController.Instance.lv);
        // 左下倒计时
        PlayerPrefs.SetFloat("smallTimer", GameController.Instance.smallTimer);
        // 右下倒计时
        PlayerPrefs.SetFloat("bigTimer", GameController.Instance.bigTimer);
        // 经验
        PlayerPrefs.SetInt("exp", GameController.Instance.exp);
        int tmp = (AudioManager.Instance.IsMute == true) ? 1 : 0; 
        // 时候静音
        PlayerPrefs.SetInt("mute", tmp);
        // 跳转到开始场景
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

    // 设置面板关闭
    public void OnSettingButtonDown()
    {
        settingPanel.SetActive(true);
    }
    // 设置面板打开
    public void OnCloseButtonDown()
    {
        settingPanel.SetActive(false);
    }


}
