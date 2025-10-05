using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fruit : MonoBehaviour
{
    // 等级
    public int LV;
    // 是否第一次触发
    private bool isFirstTrigger = true;
    // 如果碰撞则执行
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.y) != 0 && collision.gameObject.name == "Floor")
        {
            // 播放音效
            AudioManager.Instance.PlayAudio(AudioManager.Instance.AudioClips[1]);
        }
        // 我不是准备中的水果&&如果我碰到的对方水果是 Fruit
        if (PlayerManager.Instance.ReadyFruit != this.gameObject && collision.gameObject.tag == "Fruit")
        {
            // 如果我的等级和我碰撞到的水果等级一致
            if (this.LV == collision.gameObject.GetComponent<Fruit>().LV)
            {
                // 如果我的实例ID大于对方的
                if (this.gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())
                {
                    // 合成
                    // 获取比我高一级的水果
                    GameObject prefab = PlayerManager.Instance.FruitPrefabs[LV];
                    // 预制体实例化-创建出来
                    GameObject fruit = Instantiate(prefab);
                    // 把水果移动到指定位置
                    fruit.transform.position = this.gameObject.transform.position;
                   
                    // 播放音效
                    AudioManager.Instance.PlayAudio(AudioManager.Instance.AudioClips[0]);
                    // 销毁双方
                    Destroy(this.gameObject);
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                if (Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.y) > 0.8f)
                {
                    // 播放音效
                    AudioManager.Instance.PlayAudio(AudioManager.Instance.AudioClips[1]);
                }

            }
        }
    }
    // 如果触发则执行
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果我不是准备中水果,触发到的是DeadLine
        if (isFirstTrigger == false && PlayerManager.Instance.ReadyFruit != this.gameObject && collision.gameObject.name == "DeadLine")
        {
            // 游戏失败
            SceneManager.LoadScene("SampleScene");
        }
        else if (isFirstTrigger == true && collision.gameObject.name == "DeadLine")
        {
            isFirstTrigger = false;
        }
    }
}
