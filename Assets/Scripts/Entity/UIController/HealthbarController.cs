using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour
{
    private float healthBarMax = 0.75f;
    public RectTransform healthBar; 

    public GameObject player;
    private float health;
    private float maxHealth;

    public void Init()
    {
        ChangeHealthBar();
    }
    
    private void Update()  // temporary update(), make it change when there's change in player's health
    {
        ChangeHealthBar();
    }

    private void GetPlayerStat()  // get player's health and max health value
    {
        PlayerStat playerStat = player.GetComponent<PlayerStat>();
        health = playerStat.Health;
        maxHealth = playerStat.MaxHealth;
    }
    public void ChangeHealthBar()  // change health bar if there's any change in player's health
    {
        GetPlayerStat();
        float healthRatio = health / maxHealth;
        healthBar.localScale = new Vector3(healthRatio, 1.0f, 1.0f);
    }
    
    // calculate health ratio compared to max health
    // update local scale of healthbar
}
