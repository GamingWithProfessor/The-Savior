using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBulletsContainer : MonoBehaviour
{

public static UIBulletsContainer instance;

[Header( "Elements" )]
[SerializeField] private Transform bulletsParent;

[Header("Settings")]
[SerializeField] private Color activeColor;
[SerializeField] private Color inactiveColor;
private int bulletsShot;

    private void Awake()
    {
         if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        PlayerShooter.onShot += onShotCallBack;

        PlayerMovement.onEnteredWarzone += EnteredWarZoneCallBack;
        PlayerMovement.onExitedWarzone += ExitedWarZoneCallBack;
    }
    
    private void OnDestroy()
    {
        PlayerShooter.onShot -= onShotCallBack;

        PlayerMovement.onEnteredWarzone -= EnteredWarZoneCallBack;
        PlayerMovement.onExitedWarzone -= ExitedWarZoneCallBack;
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletsParent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onShotCallBack()
    {
       bulletsShot++;
       
       bulletsShot = Mathf.Min(bulletsShot, bulletsParent.childCount);
       
       bulletsParent.GetChild(bulletsShot - 1).GetComponent<Image>().color = inactiveColor;
    }

    private void EnteredWarZoneCallBack()
    {
      bulletsParent.gameObject.SetActive(true);
    }

    private void ExitedWarZoneCallBack()
    {
      bulletsParent.gameObject.SetActive(false);
    }

    public bool CanShoot()
    {
        return bulletsShot < bulletsParent.childCount;
    }
}
