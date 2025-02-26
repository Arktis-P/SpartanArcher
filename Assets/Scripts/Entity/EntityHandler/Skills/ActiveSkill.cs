using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{
    PlayerStat playerStat;

    [Header("Dash")]
    private Rigidbody2D rb;
    private bool isDashing;
    public float dashDistance = 2f;  // 돌진 거리
    public float dashDuration = 0.2f;  // 돌진 지속 시간
    public float dashCooldownTime = 3f; // 대시 기본 쿨타임
    public float dashCooldownTimer = 0f; // 쿨타임 타이머
    public bool isDashReady = true; // 쿨타임이 끝났는지 체크
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStat = GetComponent<PlayerStat>();
    }
    private void Update()
    {
        if (!isDashReady) DashCoolDown();
    }

    public void StartDash()
    {
        dashDistance = playerStat.DeshDistance;
        if (!isDashing && playerStat.Desh && isDashReady) StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine() // 대쉬 코루틴
    {
        isDashing = true;
        

        // 바라보는 방향으로 돌진
        Vector2 startPosition = rb.position;
        Vector2 dashDirection = GetComponent<PlayerController>().LookDirection.normalized; //대쉬 방향
        Vector2 targetPosition = startPosition + dashDirection * dashDistance; // 대쉬 도착 지점

        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            rb.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.position = targetPosition;  // 대시 종료 후 위치 보정
        isDashing = false;
        isDashReady = false;
    }

    public void DashCoolDown()
    {
        dashCooldownTimer += Time.deltaTime;

        if (dashCooldownTimer >= dashCooldownTime)
        {
            dashCooldownTimer = 0f;
            isDashReady = true;
        }
    }
}
