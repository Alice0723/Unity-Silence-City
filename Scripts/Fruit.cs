using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fruit : MonoBehaviour
{
    // �ȼ�
    public int LV;
    // �Ƿ��һ�δ���
    private bool isFirstTrigger = true;
    // �����ײ��ִ��
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.y) != 0 && collision.gameObject.name == "Floor")
        {
            // ������Ч
            AudioManager.Instance.PlayAudio(AudioManager.Instance.AudioClips[1]);
        }
        // �Ҳ���׼���е�ˮ��&&����������ĶԷ�ˮ���� Fruit
        if (PlayerManager.Instance.ReadyFruit != this.gameObject && collision.gameObject.tag == "Fruit")
        {
            // ����ҵĵȼ�������ײ����ˮ���ȼ�һ��
            if (this.LV == collision.gameObject.GetComponent<Fruit>().LV)
            {
                // ����ҵ�ʵ��ID���ڶԷ���
                if (this.gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())
                {
                    // �ϳ�
                    // ��ȡ���Ҹ�һ����ˮ��
                    GameObject prefab = PlayerManager.Instance.FruitPrefabs[LV];
                    // Ԥ����ʵ����-��������
                    GameObject fruit = Instantiate(prefab);
                    // ��ˮ���ƶ���ָ��λ��
                    fruit.transform.position = this.gameObject.transform.position;
                   
                    // ������Ч
                    AudioManager.Instance.PlayAudio(AudioManager.Instance.AudioClips[0]);
                    // ����˫��
                    Destroy(this.gameObject);
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                if (Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.y) > 0.8f)
                {
                    // ������Ч
                    AudioManager.Instance.PlayAudio(AudioManager.Instance.AudioClips[1]);
                }

            }
        }
    }
    // ���������ִ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����Ҳ���׼����ˮ��,����������DeadLine
        if (isFirstTrigger == false && PlayerManager.Instance.ReadyFruit != this.gameObject && collision.gameObject.name == "DeadLine")
        {
            // ��Ϸʧ��
            SceneManager.LoadScene("SampleScene");
        }
        else if (isFirstTrigger == true && collision.gameObject.name == "DeadLine")
        {
            isFirstTrigger = false;
        }
    }
}
