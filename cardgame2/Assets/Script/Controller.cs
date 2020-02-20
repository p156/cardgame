using System.Collections.Generic;
using UnityEngine;
public class Controller : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] GameObject[] card;
    [SerializeField] Transform cardParent;
    [SerializeField] Transform HandParent;
    [SerializeField] GameObject[] fleldObjects;
    public List<GameObject> test = new List<GameObject>();
    public int count = -1;
    public int plans = 0;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "deck")
                {
                    //デッキにタッチしたら
                    DeckDorw();
                }
            }
        }
    }
    //配列を作る
    //デッキからフィールドへ
    public void DeckDorw()
    {
        count -= plans;
        count++;
        test[count] = Instantiate(card[Random.Range(0, 8)], new Vector3(-1.8f + (1.2f * (count % 4)), 0.2f, 4f - (2f * (count / 4))), Quaternion.identity, cardParent);
        plans = 0;
    }
    //引く
    public void HandDorw(int number)
    {
        Instantiate(card[number], new Vector3(-10f, 0f, 1f), Quaternion.Euler(270f, 0f, 0f), HandParent);
    }
    //手札から召喚する
    public void Summon(int number)
    {
        count -= plans;
        count++;
        test[count] = Instantiate(card[number], new Vector3(-1.8f + (1.2f * (count % 4)), 0.2f, 4f - (2f * (count / 4))), Quaternion.identity, cardParent);
        plans = 0;
    }
    //ジャッチ２
    public void Judge(string tag)
    {
        int keep = count;
        if (count == 0)
        {
        }
        else if (count == 1 || count == 2 || count == 3)
        {
            if (test[keep - 1].tag == tag)
            {
                Destroy(test[keep - 1].gameObject);
                Destroy(test[keep].gameObject);
                test[keep - 1] = null;
                test[keep] = null;
                count -= 2;
            }
        }
        else if (count % 4 == 0)
        {
            if (test[keep - 4].tag == tag)
            {
                Destroy(test[keep - 4].gameObject);
                Destroy(test[keep].gameObject);
                test[keep - 4] = null;
                test[keep] = null;
                count -= 2;
            }
            else if (test[keep - 3].tag == tag)
            {
                Destroy(test[keep - 3].gameObject);
                Destroy(test[keep].gameObject);
                test[keep - 3] = null;
                test[keep] = null;
                count -= 2;
            }
        }
        else if (count % 4 == 1 || count % 4 == 2)
        {
            if (test[keep - 5].tag == tag)
            {
                Destroy(test[keep - 5].gameObject);
                Destroy(test[keep].gameObject);
                test[keep - 5] = null;
                test[keep] = null;
                count -= 2;
            }
            else if (test[keep - 4].tag == tag)
            {
                Destroy(test[keep - 4].gameObject);
                Destroy(test[keep].gameObject);
                test[keep - 4] = null;
                test[keep] = null;
                count -= 2;
            }
            else if (test[keep - 3].tag == tag)
            {
                Destroy(test[keep - 3].gameObject);
                Destroy(test[keep].gameObject);
                test[keep - 3] = null;
                test[keep] = null;
                count -= 2;
            }
            else if (test[keep - 1].tag == tag)
            {
                Destroy(test[keep - 1].gameObject);
                Destroy(test[keep].gameObject);
                test[keep - 1] = null;
                test[keep] = null;
                count -= 2;
            }
        }
        else if (count % 4 == 3)
        {
            if (test[keep - 5].tag == tag)
            {
                Destroy(test[keep - 5].gameObject);
                Destroy(test[keep].gameObject);
                test[keep - 5] = null;
                test[keep] = null;
                count -= 2;
            }
            else if (test[keep - 4].tag == tag)
            {
                Destroy(test[keep - 4].gameObject);
                Destroy(test[keep].gameObject);
                test[keep - 4] = null;
                test[keep] = null;
                count -= 2;
            }
            else if (test[keep - 1].tag == tag)
            {
                Destroy(test[keep - 1].gameObject);
                Destroy(test[keep].gameObject);
                test[keep - 1] = null;
                test[keep] = null;
                count -= 2;
            }
        }
        Shorten(keep);
    }
    //フィールドを詰める
    public void Shorten(int card)
    {
        Debug.Log("A");
        for (int X = 0; X < card; X++)
        {
            Debug.Log("A");
            Debug.Log(test[X]);
            if (test[X] == null)
            {
                for (int Y = X; Y < card; Y++)
                {
                    if (test[Y] != null)
                    {
                        Debug.Log(test[X]);
                        test[X] = Instantiate(test[Y], new Vector3(-1.8f + (1.2f * (X % 4)), 0.2f, 4f - (2f * (X / 4))), Quaternion.identity, cardParent);
                        Destroy(test[Y].gameObject);
                        test[Y] = null;
                        break;
                    }
                }
                //Judge(test[X].gameObject.tag);
            }
        }
    }
}
