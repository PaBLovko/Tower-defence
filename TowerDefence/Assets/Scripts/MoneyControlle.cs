using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyControlle : MonoBehaviour
{

    public static int moneyCount;
    private Text moneyCounter;

    // Use this for initialization
    void Start()
    {
        moneyCounter = GetComponent<Text>();
        moneyCount = Manager.Instance.GetResources();
    }

    // Update is called once per frame
    void Update()
    {
        moneyCount = Manager.Instance.GetResources();
        moneyCounter.text = "" + moneyCount;
    }
}
