using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//カードが二枚あったら消える奴
public class Judge : MonoBehaviour
{
    [SerializeField] private Transform fleld;
    private int count;

    public void judge(string tag)
    {
        count = 0; //初期化
        foreach (Transform child in fleld)//fleld内を全部調べる
        {
            Debug.Log(child.tag);

            if (child.tag == tag) //タグが同じか判断
            {
                count++;
                if (count == 2)//二枚あたら？
                {
                    var childTransforms = fleld.GetComponentsInChildren<Transform>().Where(t => t.tag == tag);//フィールドの中の同じタグのやつを取得

                    foreach (var chain in childTransforms)
                    {
                        Destroy(chain.gameObject);//そして消す
                    }
                    count = 0;//一応初期化
                }
            }
        }
    }
}