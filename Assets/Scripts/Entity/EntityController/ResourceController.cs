using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController baseController;
    private StatHandler statHandler;
    //private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    private Action<float, float> OnChangeHealth;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.Health;

    private void Start()
    {
        CurrentHealth = statHandler.Health;
    }


    public bool ChangeHealth(float change)  // WeaponHandler �Ǵ� ProjectileController���� ����
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (change < 0)
        {
            //animationHandler.Damage();  �ǰ� �ִϸ��̼�

            /*  �ǰ� ����
            if (damageClip != null)
            {
                SoundManager.PlayClip(damageClip);
            }
            */
        }

        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        baseController.Death();
    }

    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }
}
