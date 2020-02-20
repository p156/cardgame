using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] GameObject[] card;
    [SerializeField] Transform cardParent;
    [SerializeField] Transform HandParent;
    int number;

    public GameObject controller;
    Controller controllerscript;
    //最初の手札
    void Start()
    {
        number = Random.Range(0, 8);
        Instantiate(card[number], new Vector3(-10f, 0f, 1f), Quaternion.Euler(270f, 0f, 0f), HandParent);
        controllerscript = controller.GetComponent<Controller>();
    }
    //手札にタッチしたとき
    public void Touch()
    {
        //子オブジェクトのみにする
        foreach (Transform child in HandParent)
        {
            Destroy(child.gameObject);
        }
        controllerscript.Summon(number);
        number = Random.Range(0, 8);
        controllerscript.HandDorw(number);
    }
}
