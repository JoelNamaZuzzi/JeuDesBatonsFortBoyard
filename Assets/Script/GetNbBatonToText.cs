using System;
using UnityEngine;
using UnityEngine.UI;


public class GetNbBatonToText : MonoBehaviour
{
    private Text nbBatonChoose;

    [SerializeField]
    private MyNetworkPlayer _player;

    private void Start()
    {
        nbBatonChoose=GetComponent<Text>();
    }

    private void Update()
    {
        nbBatonChoose.text = _player.GetBatonWanted().ToString();
    }
}
