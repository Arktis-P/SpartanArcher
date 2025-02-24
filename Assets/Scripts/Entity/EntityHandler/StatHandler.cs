using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private int health = 10;
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, health);
    }

    [Range(1f, 20f)][SerializeField] private float moveSpeed = 3;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = Mathf.Clamp(value, 0, 20);
    }

    [Range(1f, 100f)][SerializeField] private float attackFreq = 2f;

    public float AttackFreq
    {
        get => attackFreq;
        set => attackFreq = Mathf.Clamp(value, 0, 100);
    }

    [Range(1f, 100f)][SerializeField] private float shootingRange = 5f;

    public float ShootingRange
    {
        get => shootingRange;
        set => shootingRange = Mathf.Clamp(value, 0, 100);
    }

}
