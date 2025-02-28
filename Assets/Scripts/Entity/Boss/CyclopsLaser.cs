using Assets.Scripts.Entity.Boss;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsLaser : MonoBehaviour
{
    private Vector2 target;
    private Animator animator;
    private Animator childAnimator;
    CyclopsController cyclopsController;
    float rotationZ;
    private float speed = 2f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        childAnimator = GetComponentInChildren<Animator>();
        cyclopsController = GetComponentInParent<CyclopsController>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
        target = cyclopsController.LookDirection;
    }
    private void Update()
    {
        target = cyclopsController.LookDirection;
        rotationZ = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        Quaternion goalRotation = Quaternion.Euler(0f, 0f, rotationZ);
        transform.localRotation = Quaternion.Lerp(transform.rotation, goalRotation,Time.deltaTime * speed);
    }

    public void SetLaser()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("IsLaserStart");
    }

    public void LaserCollision()
    {
        //CyclopsLaserCollision Animator
        childAnimator.SetTrigger("OnLaser");
    }


}
