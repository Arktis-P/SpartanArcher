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
    //�뽬 �Ÿ��� playerStat�� ����
    public float dashDuration = 0.2f;  // ���� ���� �ð�
    public float dashCooldownTime = 3f; // ��� �⺻ ��Ÿ��
    public float dashCooldownTimer = 0f; // ��Ÿ�� Ÿ�̸�
    public bool isDashReady = true; // ��Ÿ���� �������� üũ
    

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
        if (!isDashing && playerStat.Desh && isDashReady) StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine() // �뽬 �ڷ�ƾ
    {
        isDashing = true;
        

        // �ٶ󺸴� �������� ����
        Vector2 startPosition = rb.position;
        Vector2 dashDirection = GetComponent<PlayerController>().LookDirection.normalized; //�뽬 ����
        Vector2 targetPosition = startPosition + dashDirection * playerStat.DeshDistance; // �뽬 ���� ����

        // ��� �� ���� �浹�ϴ��� Ȯ���ϱ� ���� ����ĳ��Ʈ
        RaycastHit2D hit = Physics2D.Raycast(startPosition, dashDirection, playerStat.DeshDistance, LayerMask.GetMask("Wall"));
        if (hit.collider != null)
        {
            // �浹�� �߻��� ���, ���� �浹 �������� ��ø� ���߰�, �� �������� 0.2��ŭ ª�� �Ÿ��� ���
            targetPosition = hit.point - dashDirection * 0.2f;
        }

        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            rb.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.position = targetPosition;  // ��� ���� �� ��ġ ����
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
