using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    // 金币的文本框
    public Text goldText;
    // 等级文本框
    public Text lvText;
    // 等级名字文本框
    public Text lvNameText;
    // 小倒计时文本框
    public Text smallCountDownText;
    // 大倒计时文本框
    public Text bigCountDownText;

    // 加钱按钮
    public Button bigCountDownButton;
    // 返回按钮
    public Button backButton;
    // 设置按钮
    public Button settingButton;
    // 经验滑动器
    public Slider expSlider;

    // 初始等级
    public int lv = 0;
    // 初始经验
    public int exp = 0;
    // 初始金币
    public int gold = 500;
    // 右上倒计时初始时间，倒计时完发奖金
    public const int bigCountDown = 240;
    // 左下小时间倒计时初始时间，倒计时完发奖金
    public const int smallCountDown = 60;
    // 右上计时器
    public float bigTimer = bigCountDown;
    // 左下计时器
    public float smallTimer = smallCountDown;
    // 金币的颜色
    public Color goldTextColor;

    // 段位的字
    private string[] lvName = { "木材", "塑料", "钢铁", "青铜", "白银", "黄金", "铂金", "钻石", "大师", "王者" };

    // 升级提示
    public GameObject lvUpTip;
    // 开火特效
    public GameObject fireEffect;
    // 换枪特效
    public GameObject changeEffect;
    // 升级特效
    public GameObject lvEffect;
    // 发钱特效
    public GameObject goldEffect;
    // 切换屏幕特效
    public GameObject seaWaveEffect;

    // 渲染背景
    public Image backGroundsImage;
    // 存背景图片
    public Sprite[] backGroundsSprite;
    // 背景图片的索引值
    public int bgIndex = 0;

    // 装炮的数组
    public GameObject[] gunObjects;


    // 创建数组来放置炮台的子弹预制体，有五个炮台
    public GameObject[] bulletObjects1;
    public GameObject[] bulletObjects2;
    public GameObject[] bulletObjects3;
    public GameObject[] bulletObjects4;
    public GameObject[] bulletObjects5;
    // 持有BulletsHolder
    public Transform bulletsHolder;
    // text
    public Text shootCostText;

    // 当前使用的是什么等级的子弹
    private int costIndex = 0;
    // 一次射击消耗的金币，造成的伤害
    private int[] shootCosts = { 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

    // 单例模式
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }

    }
    private void Awake()
    {
        _instance = this;
    }

    // 加威力的按钮
    public void OnButtonIncreaseDown()
    {
        // 禁用炮台，判断更改炮台型号
        gunObjects[costIndex / 4].SetActive(false);

        // 按下按钮增加消耗金币
        costIndex++;

        // 换枪特效
        Instantiate(changeEffect);

        // 音效
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.exchangeClip);

        // 如果消耗金币超过1000，让他变回消耗5
        costIndex = (costIndex > shootCosts.Length - 1) ? 0 : costIndex;

        // 启用炮台
        gunObjects[costIndex / 4].SetActive(true);

        // 修改UI下方的金币消耗
        shootCostText.text = "￥" + shootCosts[costIndex];
    }
    // 减威力的按钮
    public void OnButtonDecreaseDown()
    {
        // 禁用炮台，判断更改炮台型号
        gunObjects[costIndex / 4].SetActive(false);

        // 按下按钮减少消耗金币
        costIndex--;

        // 换枪特效
        Instantiate(changeEffect);

        // 音效
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.exchangeClip);

        // 如果消耗金币低于5，让他变回消耗1000
        costIndex = (costIndex < 0) ? shootCosts.Length - 1 : costIndex;

        // 启用炮台
        gunObjects[costIndex / 4].SetActive(true);

        // 修改UI下方的金币消耗
        shootCostText.text = "￥" + shootCosts[costIndex];
    }

    // 通过鼠标滚轮来修改消耗金币的数量
    void ChangeBullietCostByMouse()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            OnButtonIncreaseDown();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnButtonDecreaseDown();
        }
    }

    void Update()
    {
        // 每帧调用
        ChangeBullietCostByMouse();
        // 开火
        Fire();
        // 初始化UI
        UpdateUI();
        // 判断10级是否切换背景
        ChangeBg();
    }

	private void Start()
	{
        // 读取储存的数据
        gold = PlayerPrefs.GetInt("gold", gold);
        lv = PlayerPrefs.GetInt("lv", lv);
        exp = PlayerPrefs.GetInt("exp", exp);
        smallTimer = PlayerPrefs.GetFloat("smallTimer", smallTimer);
        bigTimer = PlayerPrefs.GetFloat("bigTimer", bigTimer);
        // 初始化UI
        UpdateUI();
	}

	// 初始化UI
	void UpdateUI()
    {
        // 倒计时计数器变动

        bigTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;
        if (smallTimer <= 0)
        {
            // 归零计时器
            smallTimer = smallCountDown;
            // 倒计时归零之后给玩家发50工资
            gold += 50;
        }
        // 防止每帧调用加入判断
        if (bigTimer <= 0 && bigCountDownButton.gameObject.activeSelf == false)
        {
            // 这是救济金模式的发工资，所以需要玩家点击按钮获取
            // 先隐藏计时器
            bigCountDownText.gameObject.SetActive(false);
            // 显示按钮
            bigCountDownButton.gameObject.SetActive(true);
        }


        // 经验等级: 升级所需经验 = 1000 + 200 * 当前等级
        // 如果得到经验瞬间连升几级，用if判断就不足以完全判断
        while (exp >= 1000 + 200 * lv)
        {
            // 计算经验值
            exp = exp - (1000 + 200 * lv);
            // 加等级
            lv++;
            // 让升级提示显示出来
            lvUpTip.SetActive(true);
            // 将等级数字修改
            lvUpTip.transform.Find("Text").GetComponent<Text>().text = lv.ToString();
            // 调用隐藏的方法
            StartCoroutine(lvUpTip.GetComponent<Ef_Hide>().Hide(0.6f));
            // 音效
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.lvUpClip);
            // 实例化升级特效
            Instantiate(lvEffect);
        }
        // 设置金币text
        goldText.text = "￥" + gold;
        // 设置等级text
        lvText.text = lv.ToString();
        // 判断玩家超过99级
        if ((lv / 10) <= 9)
        {
            // 去数组寻找名字
            lvNameText.text = lvName[lv / 10];
        }
        else
        {
            lvNameText.text = lvName[9];
        }
        // 左下倒计时数字
        smallCountDownText.text = "  " + (int)smallTimer / 10 + "  " + (int)smallTimer % 10;
        // 右上倒计时数字
        bigCountDownText.text = (int)bigTimer + "s";
        // 经验条
        expSlider.value = ((float)exp) / (1000 + 200 * lv);

    }

    void Fire()
    {
        // 当前使用的子弹
        GameObject[] usingBullets = bulletObjects1;
        // 发射子弹中的哪一种
        int bulletIndex;
        // 如果按下鼠标左键,并且判断是否和UI相撞，解决UI穿透
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            // 判断金币是否足够用来打炮
            if (gold >= shootCosts[costIndex])
            {
                // 加四次钱换一次炮台
                switch (costIndex / 4)
                {
                    case 0:
                        usingBullets = bulletObjects1;
                        break;
                    case 1:
                        usingBullets = bulletObjects2;
                        break;
                    case 2:
                        usingBullets = bulletObjects3;
                        break;
                    case 3:
                        usingBullets = bulletObjects4;
                        break;
                    case 4:
                        usingBullets = bulletObjects5;
                        break;
                }
                // 如果等级大于20级，那资源就不够，数组就会越界，
                bulletIndex = (lv % 10 >= 9) ? 9 : lv % 10;

                // 扣钱
                gold -= shootCosts[costIndex];

                // 音效
                AudioManager.Instance.PlayEffectSound(AudioManager.Instance.fireClip);

                // 开火特效
                Instantiate(fireEffect);

                // 实例化
                GameObject bullet = Instantiate(usingBullets[bulletIndex]);
                // 设置父体，false:不保留世界坐标
                bullet.transform.SetParent(bulletsHolder, false);
                // 调节bullet的localPosition和localRotation
                bullet.transform.position =
                    gunObjects[costIndex / 4].transform.Find("FirePosition").transform.position;
                bullet.transform.rotation =
                    gunObjects[costIndex / 4].transform.Find("FirePosition").transform.rotation;

                // 给子弹添加伤害
                bullet.GetComponent<BulletAttribute>().damage = shootCosts[costIndex];


                // 给子弹加上直线移动脚本
                // 调整子弹方向
                bullet.AddComponent<Ef_AutoMove>().dir = Vector3.up;
                // 调整子弹速度
                bullet.GetComponent<Ef_AutoMove>().speed = bullet.GetComponent<BulletAttribute>().speed;
            }
            // 钱不够则闪烁金币
            else
            {
                StartCoroutine(GoldNotEnough());
            }
        }


    }
    // 点击奖金加钱
    public void OnBigCountDownButtonDown()
    {
        // 发奖金
        gold += 500;
        // 音效
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.rewardClip);
        // 特效
        Instantiate(goldEffect);
        // 隐藏按钮
        bigCountDownButton.gameObject.SetActive(false);
        // 显示倒计时
        bigCountDownText.gameObject.SetActive(true);
        // 计时器归零
        bigTimer = bigCountDown;
    }

    // 闪烁金币方法
    // 最好不要在协程里对变量进行复制
    IEnumerator GoldNotEnough()
    {
        goldText.color = goldTextColor;
        goldText.color = Color.red;
        // 延时0.2s
        yield return new WaitForSeconds(0.2f);
        goldText.color = goldTextColor;

    }

    // 10级时会切换背景
    void ChangeBg(){
        // 判断是否30级了

        if(bgIndex != lv / 30){

            bgIndex = lv / 30;
            // 播放印象
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.seaWaveClip);
            // 实例化水波特效
            Instantiate(seaWaveEffect);
            // 图片不够，到90级以后就不更换图片
            if (bgIndex >= 3)
            {
                // 切换图片
                backGroundsImage.sprite = backGroundsSprite[3];
            }
            else
            {
                // 切换图片
                backGroundsImage.sprite = backGroundsSprite[bgIndex];
            }
        }
    }
}