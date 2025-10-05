using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    // 水果预制体数组
    public GameObject[] FruitPrefabs;
    // 准备水果的位置
    public Transform CreatFruitPoint;

    // 准备中的水果
    public GameObject ReadyFruit;

    /// <summary>
    /// 最开始
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    // 一开始就运行
    void Start()
    {
        // 创建一个准备水果
        CreatFruit();
    }

    // 每帧运行的
    void Update()
    {
        if (ReadyFruit == null)
        {
            return;
        }
        // 如果玩家按下左键中
        if (Input.GetMouseButton(0))
        {
            // 修改 准备水果的坐标
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPos = new Vector3(mousePos.x, ReadyFruit.transform.position.y, ReadyFruit.transform.position.z);
            // 边界值
            float maxX = 2.8f - ReadyFruit.GetComponent<CircleCollider2D>().radius;
            float minX = -2.8f + ReadyFruit.GetComponent<CircleCollider2D>().radius;
            // 做边界校验
            if (newPos.x > maxX)
            {
                newPos.x = maxX;
            }
            else if (newPos.x < minX)
            {
                newPos.x = minX;
            }

            ReadyFruit.transform.position = newPos;
        }
        // 如果弹起鼠标左键
        else if (Input.GetMouseButtonUp(0))
        {
            // 水果下落
            // 恢复水果的重力
            ReadyFruit.GetComponent<Rigidbody2D>().gravityScale = 1;
            Invoke("CreatFruit", 0.8F);
            ReadyFruit = null;
        }
    }
    /// <summary>
    /// 创建水果
    /// </summary>
    private void CreatFruit()
    {
        // 随机一个索引
        int index = Random.Range(0, 4);// 0,1,2,3
        // 获得水果预制体
        GameObject prefab = FruitPrefabs[index];
        // 预制体实例化-创建出来
        ReadyFruit = Instantiate(prefab);
        // 把水果移动到起点
        ReadyFruit.transform.position = CreatFruitPoint.position;
        // 取消水果的重力
        ReadyFruit.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}