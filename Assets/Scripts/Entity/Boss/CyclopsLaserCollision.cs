using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsLaserCollision : CyclopsLaser
{
    private Animator _animator;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
    }
    public void OnLaser()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger("OnLaser");
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if Layer == Player 데미지
        //플레이어라면 데미지를 줘야함.
        if (collision.gameObject.layer != 8)
            return;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(collision.gameObject.Equals(player))
        {
            ResourceController resourceController = player.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-2);
            }
        }
    }
}
