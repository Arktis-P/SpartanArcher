using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler
{

    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1;
    public float BulletSize { get { return bulletSize; } set { bulletSize = value; } }

    [SerializeField] private float duration;
    public float Duration { get { return duration; } set { duration = value; } }

    [SerializeField] private float spread;
    public float Spread { get { return spread; } set { spread = value; } }

    [SerializeField] private int numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } set { numberofProjectilesPerShot = value; } }

    [SerializeField] private float multipleProjectilesAngel;
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngel; } set { multipleProjectilesAngel = value; } }

    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get { return projectileColor; } }

    [SerializeField] private bool bounce = false;
    public bool Bounce { get { return bounce; } set { bounce = value; } }

    [SerializeField] private int bounceNum = 1;
    public int BounceNum { get { return bounceNum; } set { bounceNum = value; } }


    private ProjectileManager projectileManager;
    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }

    public override void Attack()
    {
        base.Attack();

        float projectilesAngleSpace = multipleProjectilesAngel;
        int numberOfProjectilesPerShot = numberofProjectilesPerShot;

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * multipleProjectilesAngel;


        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(Controller.attackDirection, angle);
        }
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle));
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}


