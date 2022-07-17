using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class MyNetworkPlayer : NetworkBehaviour
{
    
    [SerializeField]
    private Text yourTurnIndicator;

    [SerializeField] 
    private GameObject winIndicator;
    [SerializeField] 
    private GameObject looseIndicator;

    [SyncVar] [SerializeField] private int _playerId = 0;
    
    [SyncVar] [SerializeField] private string displayName = "No name";

    [SyncVar] [SerializeField] private int batonWanted = 1;

    #region Server
    
    [Server]
    public void SetDisplayName(string name)
    {
        displayName = name;
    }
    [Server]
    public void SetPlayerId(int Id)
    {
        _playerId = Id;
    }

    [Command]
    public void CmdRemoveBaton()
    {
        if (_playerId != BatonSysteme.instance.GetActualPlayerId())
        {
            RcpPasTonTourLog();
            return;
        }
        
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

    [ClientCallback]
    private void Update()
    {
        if (!hasAuthority) return;

        if (_playerId != BatonSysteme.instance.GetActualPlayerId())
        {
            yourTurnIndicator.gameObject.SetActive(false);
        }
        else
        {
            yourTurnIndicator.gameObject.SetActive(true);
        }

        if (GameOver())
        {
            if (_playerId != BatonSysteme.instance.GetActualPlayerId())
            {
                winIndicator.gameObject.SetActive(true);
                looseIndicator.gameObject.SetActive(false);
            }
            else
            {
                looseIndicator.gameObject.SetActive(true);
                winIndicator.gameObject.SetActive(false);
            }
        }
        else
        {
            looseIndicator.gameObject.SetActive(false);
            winIndicator.gameObject.SetActive(false);
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
    
    [TargetRpc]
    public void RcpPasTonTourLog()
    {
        Debug.Log("ce n'est pas votre tour");
    }

    
    public bool GameOver()
    {
        return BatonSysteme.instance.GetNbBaton() <= 0;
    }
}
