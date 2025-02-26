using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{
    PlayerStat playerStat;
    RangeWeaponHandler rangeWeaponHandler;

    [Header("Dash")]
    private Rigidbody2D rb;
    private bool isDashing;
    //대쉬 거리는 playerStat에 있음
    private float dashDuration = 0.2f;  // 돌진 지속 시간
    private float dashCooldownTime = 3f; // 대시 기본 쿨타임
    private float dashCooldownTimer = 0f; // 쿨타임 타이머
    private bool isDashReady = true; // 쿨타임이 끝났는지 체크

    [Header("FeverTime")]
    [SerializeField] private bool isFeverTime;
    [SerializeField] private float feverTime;
    [SerializeField] private float feverTimeCooldownTime = 20f; //피버타임 기본 쿨타임
    [SerializeField] private float feverTimeCooldownTimer = 0f; //피버타임 타이머
    [SerializeField] private bool isFeverReady = true; // 피버타임 쿨타임이 끝났는지 체크

    float originalBulletSize;
    float originalBulletSpeed;
    float originalBulletDelay;
    int originalBulletNumber;

    float buffBulletSize;
    float buffBulletSpeed;
    float buffBulletDelay;
    int buffBulletNumber;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStat = GetComponent<PlayerStat>();
        rangeWeaponHandler = GetComponentInChildren<RangeWeaponHandler>();
    }
    private void Update()
    {
        if (!isDashReady) DashCoolDown();

        if (!isFeverReady) FeverCoolDown();
    }

    public void StartDash()
    {
        if (!isDashing && playerStat.Desh && isDashReady) StartCoroutine(DashCoroutine());
    }

    public void StartFeverTime()
    {
        if (!isFeverTime && playerStat.IsFeverTime && isFeverReady) StartCoroutine(DoFeverTime());
    }

    private IEnumerator DashCoroutine() // 대쉬 코루틴
    {
        isDashing = true;
        

        // 바라보는 방향으로 돌진
        Vector2 startPosition = rb.position;
        Vector2 dashDirection = GetComponent<PlayerController>().LookDirection.normalized; //대쉬 방향
        Vector2 targetPosition = startPosition + dashDirection * playerStat.DeshDistance; // 대쉬 도착 지점

        // 대시 중 벽에 충돌하는지 확인하기 위한 레이캐스트
        RaycastHit2D hit = Physics2D.Raycast(startPosition, dashDirection, playerStat.DeshDistance, LayerMask.GetMask("Wall"));
        if (hit.collider != null)
        {
            // 충돌이 발생한 경우, 벽과 충돌 지점까지 대시를 멈추고, 그 지점에서 0.2만큼 짧은 거리로 대시
            targetPosition = hit.point - dashDirection * 0.2f;
        }

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

    private IEnumerator DoFeverTime() //피버타임 코루틴
    {
        Debug.Log("FeverTime ON");
        isFeverTime = true;
        isFeverReady = false;

        feverTime = playerStat.FeverTime;
        feverTime -= Time.deltaTime;

        originalBulletSize = rangeWeaponHandler.BulletSize;
        originalBulletSpeed = rangeWeaponHandler.Speed;
        originalBulletDelay = playerStat.AttackFreq;
        originalBulletNumber = rangeWeaponHandler.NumberofProjectilesPerShot;

        buffBulletSize = rangeWeaponHandler.BulletSize *= 2f;
        buffBulletSpeed = rangeWeaponHandler.Speed *= 2f;
        buffBulletDelay = playerStat.AttackFreq /= 2f;
        buffBulletNumber = rangeWeaponHandler.NumberofProjectilesPerShot * 2;


        while (feverTime > 0)
        {
            feverTime -= Time.deltaTime;
            //능력치 추가
            StatUp();
            yield return null;
            //if (isClear) break;
        }
        //능력치 회수
        StatDown();
        isFeverTime = false;
        feverTime = playerStat.FeverTime;
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

    public void FeverCoolDown()
    {
        feverTimeCooldownTimer += Time.deltaTime;

        if (feverTimeCooldownTimer >= feverTimeCooldownTime)
        {
            feverTimeCooldownTimer = 0f;
            isFeverReady = true;
        }
    }

    void StatUp()
    {
        rangeWeaponHandler.BulletSize = buffBulletSize;
        rangeWeaponHandler.Speed = buffBulletSpeed;
        playerStat.AttackFreq = buffBulletDelay;
        rangeWeaponHandler.NumberofProjectilesPerShot = buffBulletNumber;
    }

    void StatDown()
    {
        rangeWeaponHandler.BulletSize = originalBulletSize;
        rangeWeaponHandler.Speed = originalBulletSpeed;
        playerStat.AttackFreq = originalBulletDelay;
        rangeWeaponHandler.NumberofProjectilesPerShot = originalBulletNumber;
    }
}
