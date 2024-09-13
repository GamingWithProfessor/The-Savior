using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State { Alive, Dead }
    private State state;

    [Header(" Elements ")]
    [SerializeField] private CharacterRagdoll characterRagdoll;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Alive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        if (state == State.Dead)
        {
            return;            
        }
        Die();
    }
    private void Die()
    {
        state = State.Dead;

        characterRagdoll.Ragdollify();
    }
}
