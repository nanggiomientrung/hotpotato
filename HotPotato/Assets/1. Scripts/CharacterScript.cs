using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript: MonoBehaviour
{
    /// <summary>
    /// Vị trí bóng trên tay
    /// </summary>
    [SerializeField] public Transform HandPosition;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private int PlayerIndex;

    private void Start()
    {
        GameManager_HotPotato.Instance.OnThrowBall += OnThrowBall;
        GameManager_HotPotato.Instance.OnCatchBall += OnCatchBall;
        if (PlayerIndex == 0)
        {
            IsBallInHand = true;
        }
    }

    private void OnDestroy()
    {
        GameManager_HotPotato.Instance.OnThrowBall -= OnThrowBall;
        GameManager_HotPotato.Instance.OnCatchBall -= OnCatchBall;
    }

    private int NextTargetIndex;
    private void OnThrowBall(int currentTargetIndex, int nextTargetIndex, CharacterScript nextTargetTransform)
    {
        if (currentTargetIndex == PlayerIndex)
        {
            IsBallInHand = false;
        }
        NextTargetIndex = nextTargetIndex;
    }

    private void OnCatchBall()
    {
        if (NextTargetIndex == PlayerIndex)
        {
            IsBallInHand = true;
        }
    }

    private bool IsBallInHand = false;
    private void Update()
    {
        if (IsBallInHand)
        {
            transform.localEulerAngles += new Vector3(0, 0, RotateSpeed * Time.deltaTime);
        }
    }
}
