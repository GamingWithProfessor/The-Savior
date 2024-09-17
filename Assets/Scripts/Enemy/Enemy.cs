using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State { Alive, Dead }
    private State state;

    [Header(" Elements ")]
    [SerializeField] private EnemyShooter enemyshooter;
    [SerializeField] private CharacterRagdoll characterRagdoll;
    [SerializeField] private CharacterIK characterIK;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Alive;

        playerMovement = FindObjectOfType<PlayerMovement>();
        characterIK.ConfigureIk(playerMovement.GetEnemyTarget());
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

    public void ShootAtPlayer()
    {
       if (state == State.Dead)   
           return;

        enemyshooter.TryShooting();
    }


}
