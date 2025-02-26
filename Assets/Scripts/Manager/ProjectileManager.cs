using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class ProjectileManager : SingleTon<ProjectileManager>
{
    [SerializeField]private GameObject[] projectilePrefabs; //프로젝타일 프리팹 정렬 리스트?

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostion, Vector2 direction)
    {
        GameObject origin = projectilePrefabs[1]; //rangeWeaponHandler.BulletIndex
        GameObject obj = Instantiate(origin, startPostion, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler,this);
    }
}
