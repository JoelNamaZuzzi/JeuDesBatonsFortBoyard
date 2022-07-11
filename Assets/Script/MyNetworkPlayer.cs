using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar] [SerializeField] private string displayName = "No name";

    [SyncVar] [SerializeField] private int batonWanted = 1;

    #region Server
    
    [Server]
    public void SetDisplayName(string name)
    {
        displayName = name;
    }

    [Command]
    public void CmdRemoveBaton()
    {
        if (batonWanted >= 1 && batonWanted <= 3)
        {
            BatonSysteme.instance.RemoveBaton(batonWanted);
        }
        else
        {
            Debug.Log($"{batonWanted} est un nombre invalide de baton a prendre (1,2 ou 3 par tour uniquement)");
        }
    }

    #endregion

    #region Client

    [ClientCallback]
    private void Start()
    {
        if (!hasAuthority)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    [Command]
    public void UpBatonWanted()
    {
        if (batonWanted < 3)
        {
            batonWanted++;
        }
    }

    [Command]
    public void DownBatonWanted()
    {
        if (batonWanted > 1)
        {
            batonWanted--;
        }
    }

    [ClientCallback]
    public int GetBatonWanted()
    {
        return batonWanted;
    }

    #endregion
}
