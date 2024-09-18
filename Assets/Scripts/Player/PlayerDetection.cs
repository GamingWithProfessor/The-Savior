using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Element")]
    private PlayerMovement playerMovement;

    [Header("Settings")]
    [SerializeField] private float detectionRadius;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsGameState())
        DetectStuff();
    }

    private void DetectStuff()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectionRadius);

       foreach (Collider collider in detectedObjects)
       {
        if (collider.CompareTag("WarZoneEnter"))
             EnteredWarZoreCallBack(collider);
       else if (collider.CompareTag("Finish"))
               HitFinishLine();
       }
    }

    private void EnteredWarZoreCallBack(Collider warzoneTriggerCollider)
    {
      Warzone warzone = warzoneTriggerCollider.GetComponentInParent<Warzone>();
      playerMovement.EnteredWarZoreCallBack (warzone);
    }

    private void HitFinishLine()
    {
        playerMovement.HitFinishLine();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, detectionRadius);
    } 
}
   
