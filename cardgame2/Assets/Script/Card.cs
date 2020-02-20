using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//カードが置かれたときにjudgeが発動する
public class Card : MonoBehaviour
{
    Controller fleldjudge;
    public GameObject parent;

    void Start()
    {
        parent = transform.parent.gameObject;
        fleldjudge = GameObject.Find("EventSystem").GetComponent<Controller>();
        if (parent.name == " Field")
        {
            Debug.Log("起動");
            fleldjudge.Judge(transform.tag);
        }
    }
}
