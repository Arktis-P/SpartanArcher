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

    private void Awake()
    {
        baseController = GetComponent<BaseController>();
        statHandler = GetComponent<StatHandler>();
        //animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        CurrentHealth = statHandler.Health;
    }


    public bool ChangeHealth(float change)  // WeaponHandler 또는 ProjectileController에서 적용
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
            //animationHandler.Damage();  피격 애니메이션

            /*  피격 사운드
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
