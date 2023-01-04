using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript: MonoBehaviour
{
    [SerializeField] private float BallSpeed;
    [SerializeField] private float CatchDistance;
    [SerializeField] private Transform FlyingParentTransform;
    private void Start()
    {
        GameManager_HotPotato.Instance.OnThrowBall += OnThrowBall;
        SqrCatchDistance = Mathf.Pow(CatchDistance, 2);
    }

    private void OnDestroy()
    {
        GameManager_HotPotato.Instance.OnThrowBall -= OnThrowBall;
    }


    private Vector3 BallVelocity;
    private Transform NextTarget;
    private bool IsFlying = false;
    private void OnThrowBall(int currentTarget, int nextTargetIndex, CharacterScript nextTarget)
    {
        IsFlying = true;
        NextTarget = nextTarget.HandPosition;

        BallVelocity = BallSpeed * GetPlayerDirection(transform.parent);

        transform.SetParent(FlyingParentTransform);
    }

    private Vector2 GetPlayerDirection(Transform playerTransform)
    {
        float rotateRadian = playerTransform.localEulerAngles.z * Mathf.PI / 180;
        float cosRotateRadian = Mathf.Cos(rotateRadian);
        float sinRotateRadian = Mathf.Sin(rotateRadian);
        return new Vector2(sinRotateRadian, -cosRotateRadian);
    }


    private void Update()
    {
        if (IsFlying == false) return;
        transform.localPosition += BallVelocity * Time.deltaTime;
        CheckCatch();
    }

    private float SqrCatchDistance;
    /// <summary>
    /// Kiểm tra khoảng cách với nextt target => nếu khoảng cách đủ bé thì sẽ cho bắt bóng
    /// </summary>
    private void CheckCatch()
    {
        float squareDistance = (NextTarget.position - transform.localPosition).sqrMagnitude;
        if (squareDistance <= SqrCatchDistance)
        {
            // catch
            GameManager_HotPotato.Instance.CatchBall();
            IsFlying = false;

            transform.SetParent(NextTarget.parent);
            transform.localPosition = NextTarget.localPosition;
        }
    }
}
