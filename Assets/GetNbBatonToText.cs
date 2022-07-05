using System;
using UnityEngine;
using UnityEngine.UI;


public class GetNbBatonToText : MonoBehaviour
{
    private Text nbBatonChoose;

    private void Start()
    {
        nbBatonChoose=GetComponent<Text>();
    }

    private void Update()
    {
        nbBatonChoose.text = BatonSysteme.instance.GetBatonWanted().ToString();
    }
}
