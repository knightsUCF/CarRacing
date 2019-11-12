using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Game : MonoBehaviour
{
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private static Game instance;

    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Game();
            }
            return instance;
        }
    }



    // game initialization

    protected Game()
    {
        GameState = GameState.Start;
        CanSwipe = false;
    }

    public GameState GameState { get; set; }

    public bool CanSwipe { get; set; }



    // methods


    public void Die()
    {
        UI.Instance.SetStatus(Constants.StatusDeadTapToStart);
        this.GameState = GameState.Dead;
    }

}
