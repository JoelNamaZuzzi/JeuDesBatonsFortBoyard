using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BatonSysteme : MonoBehaviour
{
    enum Player {Player1 = 1, Player2};
    
    [SerializeField]
    private int _baton;

    [SerializeField]
    private Player _activePlayer = Player.Player1;

    [SerializeField]
    private int batonWanted = 1;

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

    public void RemoveBaton()
    {
        if (batonWanted >= 1 && batonWanted <= 3)
        {
            _baton -= batonWanted;
            if (GameOver())
            {
                Debug.Log("Joueur "+ _activePlayer + " à perdu");
            }
            else
            {
                ChangePlayer();
                LogNewTurn();
                
            }
        }
        else
        {
            Debug.Log("Nombre de baton retirer imposible (1-2-3 uniquement) ");
        }
    }

    public void LogNewTurn()
    {
        Debug.Log("Il reste "+_baton+" baton !");
        Debug.Log("C'est au player "+ _activePlayer + " de jouer " );
    }

    public bool GameOver()
    {
        if (_baton <= 0)
        {
            return true;
        }

        return false;
    }

    private void ChangePlayer()
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

    public void UpBatonWanted()
    {
        if (batonWanted < 3)
        {
            batonWanted++;
        }
    }

    public void DownBatonWanted()
    {
        if (batonWanted > 1)
        {
            batonWanted--;
        }
    }

    public int GetBatonWanted()
    {
        return batonWanted;
    }

}
