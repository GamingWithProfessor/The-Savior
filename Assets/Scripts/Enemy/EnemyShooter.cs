using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private Transform bulletsParent;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header(" Settings ")]
    [SerializeField] private float bulletSpeed;
    private bool hasShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryShooting()
    {
        if (hasShot)
            return;

        hasShot = true;

        Invoke("Shoot", .5f);
    }

    private void Shoot()
    {
        Vector3 velocity = bulletSpeed * bulletSpawnPoint.right;

        EnemyBullet bulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity, bulletsParent);
        bulletInstance.Configure(velocity);
    }
}
