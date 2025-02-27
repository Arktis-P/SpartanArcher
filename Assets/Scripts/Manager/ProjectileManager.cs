using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class ProjectileManager : SingleTon<ProjectileManager>
{
    [SerializeField]private GameObject[] projectilePrefabs; //프로젝타일 프리팹 정렬 리스트?

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostion, Vector2 direction)
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex]; //rangeWeaponHandler.BulletIndex
        GameObject obj = Instantiate(origin, startPostion, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler);
    }
    public void fragmentBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostion, Vector2 direction,bool isfragment)
    {
        GameObject origin = projectilePrefabs[3];
        GameObject obj = Instantiate(origin, startPostion, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.FragmentProjectile = true;
        projectileController.Init(direction, rangeWeaponHandler);
    }
}
