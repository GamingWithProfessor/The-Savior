using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyTrigger : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private LineRenderer shootingLine;

    [Header(" Settings ")]
    [SerializeField] private LayerMask enemiesMask;
    private List<Enemy> currentEnemies = new List<Enemy>();
    private bool canCheckForShootingEnemies;

    private void Awake()
    {
        PlayerMovement.onEnteredWarzone += EnteredWarzoneCallback;
        PlayerMovement.onExitedWarzone += ExitedWarzoneCallback;
    }

    private void OnDestroy()
    {
        PlayerMovement.onEnteredWarzone -= EnteredWarzoneCallback;   
        PlayerMovement.onExitedWarzone -= ExitedWarzoneCallback;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canCheckForShootingEnemies)
            CheckForShootingEnemies();
    }

    private void EnteredWarzoneCallback()
    {
        canCheckForShootingEnemies = true;
    }

    private void ExitedWarzoneCallback()
    {
        canCheckForShootingEnemies = false;
    }

    private void CheckForShootingEnemies()
    {
        // World Space ray origin
        Vector3 rayOrigin = shootingLine.transform.TransformPoint(shootingLine.GetPosition(0));
        Vector3 worldSpaceSecondPoint = shootingLine.transform.TransformPoint(shootingLine.GetPosition(1));

        Vector3 rayDirection = (worldSpaceSecondPoint - rayOrigin).normalized;
        float maxDistance = Vector3.Distance(rayOrigin, worldSpaceSecondPoint);

        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection, maxDistance, enemiesMask);

        for (int i = 0; i < hits.Length; i++)
        {
            Enemy currentEnemy = hits[i].collider.GetComponent<Enemy>();

            if (!currentEnemies.Contains(currentEnemy))
                currentEnemies.Add(currentEnemy);
        }

        // We have a list of current enemies, enemies we've detected
        // For each current enemy in the list, check if we have a raycast hit for that enemy
        // If that's not the case, it means that the enemy has exited the line of sight of the player

        List<Enemy> enemiesToRemove = new List<Enemy>();

        foreach(Enemy enemy in currentEnemies)
        {
            bool enemyFound = false;

            for (int i = 0; i < hits.Length; i++)
            {
                if(hits[i].collider.GetComponent<Enemy>() == enemy)
                {
                    enemyFound = true;
                    break;
                }
            }

            if(!enemyFound)
            {
                enemy.ShootAtPlayer();
                enemiesToRemove.Add(enemy);
            }
        }

        // Remove processed enemies from the current enemies list
        foreach (Enemy enemy in enemiesToRemove)
            currentEnemies.Remove(enemy);
    }
}
