using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyControlle : MonoBehaviour
{

    public static int moneyCount;
    private Text moneyCounter;

    void Start()
    {
        moneyCounter = GetComponent<Text>();
        moneyCount = Manager.Instance.GetResources();
    }

    void Update()
    {
        moneyCount = Manager.Instance.GetResources();
        moneyCounter.text = "" + moneyCount;
    }
}
