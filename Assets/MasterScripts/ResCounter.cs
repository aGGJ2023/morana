using MasterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResCounter : MonoBehaviour
{
    public Text CounterText;

    private void Start()
    {
        CounterText = GameObject.Find("UI/Panel/ResCounterTxt").GetComponent<Text>();
    }

    private void Update()
    {
        //CounterText.text = GameManager.Instance.;
    }
}
