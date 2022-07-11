using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mirror;
using Telepathy;
using UnityEngine;

public class BatonSysteme : NetworkBehaviour
{
    enum Player {Player1 = 1, Player2};
    
    [SyncVar(hook = nameof(SetBaton))]
    [SerializeField]
    private int _baton;
    
    [SyncVar]
    [SerializeField]
    private Player _activePlayer = Player.Player1;

    [SerializeField] private List<GameObject> batons;

    public static BatonSysteme instance;



    void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        _baton = 21;
        
    }

    [Server]
    public void RemoveBaton(int mbaton)
    {
        _baton -= mbaton;
        if (GameOver())
        {
            GameEndAction();
        }
        else
        {
           ChangePlayer();
           LogNewTurn();
        }
        
    }

    [ClientRpc]
    public void LogNewTurn()
    {
        Debug.Log("Il reste "+_baton+" baton !");
        Debug.Log("C'est au player "+ _activePlayer + " de jouer " );
    }

    [ClientCallback]
    private void Update()
    {
        ShowBaton();
    }

    void ShowBaton()
    {
        for (int i = 0; i < batons.Count; i++)
        {
            if (i >= _baton)
            {
                batons[i].SetActive(false);
            }
            else {
                batons[i].SetActive(true);    
            }
        }
    }

    [ClientRpc]
    public void GameEndAction()
    {
        Debug.Log("Joueur "+ _activePlayer + " à perdu");
    }

    [Server]
    public bool GameOver()
    {
        if (_baton <= 0)
        {
            return true;
        }

        return false;
    }

    [Server]
    public void ChangePlayer()
    {
        if (_activePlayer == Player.Player1)
        {
            _activePlayer = Player.Player2;
        }
        else
        {
            _activePlayer = Player.Player1;
        }
    }

    public void SetBaton(int baton, int newbaton)
    {
        _baton = newbaton;
    }

}
