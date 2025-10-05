using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    // ˮ��Ԥ��������
    public GameObject[] FruitPrefabs;
    // ׼��ˮ����λ��
    public Transform CreatFruitPoint;

    // ׼���е�ˮ��
    public GameObject ReadyFruit;

    /// <summary>
    /// �ʼ
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    // һ��ʼ������
    void Start()
    {
        // ����һ��׼��ˮ��
        CreatFruit();
    }

    // ÿ֡���е�
    void Update()
    {
        if (ReadyFruit == null)
        {
            return;
        }
        // �����Ұ��������
        if (Input.GetMouseButton(0))
        {
            // �޸� ׼��ˮ��������
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPos = new Vector3(mousePos.x, ReadyFruit.transform.position.y, ReadyFruit.transform.position.z);
            // �߽�ֵ
            float maxX = 2.8f - ReadyFruit.GetComponent<CircleCollider2D>().radius;
            float minX = -2.8f + ReadyFruit.GetComponent<CircleCollider2D>().radius;
            // ���߽�У��
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
        // �������������
        else if (Input.GetMouseButtonUp(0))
        {
            // ˮ������
            // �ָ�ˮ��������
            ReadyFruit.GetComponent<Rigidbody2D>().gravityScale = 1;
            Invoke("CreatFruit", 0.8F);
            ReadyFruit = null;
        }
    }
    /// <summary>
    /// ����ˮ��
    /// </summary>
    private void CreatFruit()
    {
        // ���һ������
        int index = Random.Range(0, 4);// 0,1,2,3
        // ���ˮ��Ԥ����
        GameObject prefab = FruitPrefabs[index];
        // Ԥ����ʵ����-��������
        ReadyFruit = Instantiate(prefab);
        // ��ˮ���ƶ������
        ReadyFruit.transform.position = CreatFruitPoint.position;
        // ȡ��ˮ��������
        ReadyFruit.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}