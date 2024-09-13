using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRagdoll : MonoBehaviour
{
    [Header(" Elements ")]    
    [SerializeField] private Animator animator;
    [SerializeField] private Collider mainCollider;
    [SerializeField] private Rigidbody[] rigidbodies;

    [Header(" Settings ")]
    [SerializeField] private float ragdollForce;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Rigidbody rig in rigidbodies)
        {
            rig.isKinematic = true ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ragdollify()
    {
        foreach (Rigidbody rig in rigidbodies)
        {
            rig.isKinematic = false;

            rig.AddForce((Vector3.up + Random.insideUnitSphere) * ragdollForce);
        }

        animator.enabled = false;
        mainCollider.enabled = false;
    }
}
