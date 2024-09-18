using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShooter : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private GameObject shootingLine;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private Transform bulletsParent;

    [Header(" Settings ")]
    [SerializeField] private float bulletSpeed;
    private bool canShoot;

    [Header ( "Action" )]
    public static Action onShot;

    // Called before the start method, only once
    private void Awake()
    {
        PlayerMovement.onEnteredWarzone += EnteredWarzoneCallback;        
        PlayerMovement.onExitedWarzone += ExitedWarzoneCallback;
        PlayerMovement.onDied += DiedCallback;
    }

    private void OnDestroy()
    {
        PlayerMovement.onEnteredWarzone -= EnteredWarzoneCallback;
        PlayerMovement.onExitedWarzone -= ExitedWarzoneCallback;        
        PlayerMovement.onDied -= DiedCallback;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetShootingLineVisibility(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        ManageShooting();
    }

    private void ManageShooting()
    {
        if (Input.GetMouseButtonDown(0) && UIBulletsContainer.instance.CanShoot())
            Shoot();
    }

    private void Shoot()
    {
        Vector3 direction = bulletSpawnPosition.right;
        direction.z = 0;

        Bullet bulletInstance = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity, bulletsParent);
        bulletInstance.Configure(direction * bulletSpeed);

        onShot?.Invoke();
    }

    private void EnteredWarzoneCallback()
    {
        SetShootingLineVisibility(true);
        canShoot = true;
    }

    private void ExitedWarzoneCallback()
    {
        SetShootingLineVisibility(false);
        canShoot = false;
    }

    private void SetShootingLineVisibility(bool visibility)
    {
        shootingLine.SetActive(visibility);
    }

    private void DiedCallback()
    {
        SetShootingLineVisibility(false);
        canShoot = false;
    }
}
