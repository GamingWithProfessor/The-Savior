using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShooter : MonoBehaviour
{
    [Header ("Elements")] 
    [SerializeField] private GameObject shootingLine;
    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private Transform bulletsParent;

    [SerializeField] private Transform bulletSpawnPosition;

    [Header("Setting")]
    [SerializeField] private float bulletSpeed;
    private bool canShoot;

    void Awake() 
    {
        PlayerMovement.onEnterWarzone += EnteredWarzoneCallback;
        PlayerMovement.onExitWarzone += ExitWarZone;
    }

    private void OnDestroy() 
        
    {
        PlayerMovement.onEnterWarzone -= EnteredWarzoneCallback;
        PlayerMovement.onExitWarzone -= ExitWarZone;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetShootingVisibility(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        ManageShooting();
    }
 
    private void ManageShooting()
    {
      if (Input.GetMouseButtonDown(0))
      Shoot();
    }

    private void Shoot()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity, bulletsParent);

        bulletInstance.Configure(bulletSpawnPosition.right * bulletSpeed);
    }

    private void EnteredWarzoneCallback()
    {
      SetShootingVisibility(true);
      canShoot = true;
    }

    private void ExitWarZone()
    {
        SetShootingVisibility(false);
        canShoot = false;
    }

    private void SetShootingVisibility(bool visibility)
    {
        shootingLine.SetActive(visibility);
    }
}
