using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Collections;

public class MakeFish : MonoBehaviour {
	// 存储生成位置的数组
	public Transform[] genPosition;
	// 存储鱼预制体的数组
	public GameObject[] fishPrefabs;
	// 事先创建的fishHolder
	public Transform fishHolder;

	// 生成一波鱼的时间间隔
	public float waveGenWaitingTime = 0.3f;
	// 生成一波鱼中一条鱼的间隔
	public float fishGenWaitingTime = 0.5f;


	void MakeFishs() {
		// 从该数组中随机挑选一个位置生成鱼
		int genPositionIndex = Random.Range(0, genPosition.Length);
		// 随机生成鱼的种类
		int fishPreIndex = Random.Range(0, fishPrefabs.Length);
		// todo:生成金鲨或者银鲨的概率降低
		// 获取生成鱼的最大数量
		int maxNum = fishPrefabs[fishPreIndex].GetComponent<FishAttribute>().maxNum;
		// 获取生成鱼的最大速度
		int maxSpeed = fishPrefabs[fishPreIndex].GetComponent<FishAttribute>().maxSpeed;
		// 生成鱼的数量
		// 因为金鲨的最大数是1，除以2之后没有，将就鱼
		int num = Random.Range((maxNum / 2) + 1, maxNum);
		// 生成鱼的速度
		int speed = Random.Range((maxSpeed / 2), maxSpeed);
		// 移动的模式:   0:直走;1:转弯
		int moveType = Random.Range(0, 2);
		// 直走的斜率
		int angleOffSet;
		// 转弯的角速度
		int angleSpeed;

		if(moveType == 0){
			// 生成直走鱼群

			// 22 —— -22 差不多就是45°的范围，可以将所有鱼生成到屏幕上
			angleOffSet = Random.Range(22, -22);

			// 调用函数生成直线的鱼
			// 使用IEnumerator的，调用方法要用StartCoroutine
			StartCoroutine(GenStraightFish(genPositionIndex, fishPreIndex, num, speed, angleOffSet));
		}
		else{
			// 生成转弯鱼群

			// 百分之五十的几率正方向的角速度
			if (Random.Range (0, 2) == 0) {
				// angleSpeed = Random.Range (-2, -1);
				// 旋转角度暂时没有是设置到合适的，就直接用1
				angleSpeed = -1;
			}
			// 百分之五十的几率反方向的角速度
			else{
				// angleSpeed = Random.Range (1, 2);
				// 旋转角度暂时没有是设置到合适的，就直接用1
                angleSpeed = Random.Range(1, 2);
			}
			// 调用函数生成转弯的鱼

			StartCoroutine(GenTurnFish(genPositionIndex, fishPreIndex, num, speed, angleSpeed));
		}
	}
	//p1:生成鱼的位置,p2:生成鱼的种类,p3:生成鱼的数量,p4:生成鱼移动的速度,p5:鱼直走的斜率
	IEnumerator  GenStraightFish(int genPosiIndex, int fishPreIndex, int num, int speed, int angleOffSet)
	{
		for (int i = 0; i < num; i++)
		{
			// 实例化
			GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);

			// 设置父体，false:不保留世界坐标的位置
			fish.transform.SetParent(fishHolder, false);

			// 调节fish的localPosition和localRotation
			fish.transform.localPosition = genPosition[genPosiIndex].localPosition;
			fish.transform.localRotation = genPosition[genPosiIndex].localRotation;

			// 让鱼旋转
			fish.transform.Rotate(0, 0, angleOffSet);

			// 有些鱼太长，重叠的鱼Unity并不知道渲染哪一个，会出现闪烁
			// 就在层内排序加i,让所有的鱼层级不一样，就不会闪烁
			fish.GetComponent<SpriteRenderer>().sortingOrder += i;

			// 给生成的鱼挂上移动的脚本
			fish.AddComponent<Ef_AutoMove>().speed = speed;

			// System.Collections中IEnumerator可以让函数等待时间
			yield return new WaitForSeconds(fishGenWaitingTime);

		}
	}

	IEnumerator  GenTurnFish(int genPosiIndex, int fishPreIndex, int num, int speed, int angleSpeed)
	{
		for (int i = 0; i < num; i++)
		{
			// 实例化
			GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);

			// 设置父体，false:不保留世界坐标的位置
			fish.transform.SetParent(fishHolder, false);

			// 调节fish的localPosition和localRotation
			fish.transform.localPosition = genPosition[genPosiIndex].localPosition;
			fish.transform.localRotation = genPosition[genPosiIndex].localRotation;

			// 有些鱼太长，重叠的鱼Unity并不知道渲染哪一个，会出现闪烁
			// 就在层内排序加i,让所有的鱼层级不一样，就不会闪烁
			fish.GetComponent<SpriteRenderer>().sortingOrder += i;

			// 给生成的鱼挂上移动的脚本
			fish.AddComponent<Ef_AutoMove>().speed = speed;

			// 给生成的鱼挂上旋转的脚本
			fish.AddComponent<Ef_AutoRotate> ().speed = angleSpeed;

			// System.Collections中IEnumerator可以让函数等待时间
			yield return new WaitForSeconds(fishGenWaitingTime);

		}
	}

	void Start () {
		InvokeRepeating("MakeFishs", 0, waveGenWaitingTime);

	}

	void Update () {

	}
}
