using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_HotPotato: MonoBehaviour
{
    [SerializeField] private List<CharacterScript> PlayerTransformList;
    public static GameManager_HotPotato Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// parameter int-int-Script: current target - nextt target - next target script
    /// </summary>
    public Action<int, int, CharacterScript> OnThrowBall;
    public Action OnCatchBall;


    private bool IsBallFlying = false;
    private int CurrentTarget = 0;
    private int NextTarget = 1; // mặc định con đầu tiên là 0


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // chỉ cho ném bóng khi bóng ko bay
            if (IsBallFlying) return;

            // throw ball
            if (OnThrowBall != null)
            {
                OnThrowBall(CurrentTarget, NextTarget, PlayerTransformList[NextTarget]);
            }
            IncreaseNextTarget();
            IsBallFlying = true;
        }
    }

    private void IncreaseNextTarget()
    {
        NextTarget++;
        if (NextTarget >= PlayerTransformList.Count)
        {
            NextTarget = 0;
        }

        CurrentTarget++;
        if (CurrentTarget >= PlayerTransformList.Count)
        {
            CurrentTarget = 0;
        }
    }

    public void CatchBall()
    {
        IsBallFlying = false;
        if (OnCatchBall != null)
        {
            OnCatchBall();
        }
    }
}
