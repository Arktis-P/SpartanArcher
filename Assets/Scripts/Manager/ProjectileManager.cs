using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class ProjectileManager : SingleTon<ProjectileManager>
{
    //[SerializeField] private GameObject[] projectilePrefabs; //������Ÿ�� ������ ���� ����Ʈ?

    //public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostion, Vector2 direction)
    //{
    //    GameObject origin = projectilePrefabs[1]; //rangeWeaponHandler.BulletIndex
    //    GameObject obj = Instantiate(origin, startPostion, Quaternion.identity);

    //    ProjectileController projectileController = obj.GetComponent<ProjectileController>();
    //    projectileController.Init(direction, rangeWeaponHandler);
    //}
}
