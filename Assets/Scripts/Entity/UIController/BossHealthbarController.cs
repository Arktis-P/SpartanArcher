using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthbarController : MonoBehaviour
{
    public RectTransform healthBar;

    public GameObject boss;
    private float health;
    private float maxHealth;

    public void Init(GameObject boss)
    {
        this.boss = boss;
        ChangeHealthBar();
    }

    private void GetBossStat()
    {
        ResourceController resource = boss.GetComponent<ResourceController>();
        health = resource.CurrentHealth;
        maxHealth = resource.MaxHealth;
    }
    public void ChangeHealthBar()
    {
        GetBossStat();
        float healtRatio = health / maxHealth;
        healthBar.localScale = new Vector3(healtRatio, 1.0f, 1.0f);
    }
}
