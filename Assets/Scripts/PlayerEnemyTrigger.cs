using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyTrigger : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private LineRenderer shootingLine;

    [Header(" Settings ")]
    private bool canCheckForShootingEnemies;
    private List<Enemy> currentEnemies = new List<Enemy>();
    [SerializeField] private LayerMask enemiesMask;

    // Start is called before the first frame update

    private void Awake()
    {
        PlayerMovement.onEnterWarzone += EnterWarzoneCallback;
        PlayerMovement.onExitWarzone += ExitWarzoneCallback;
    }

    private void OnDestroy()
    {
        PlayerMovement.onEnterWarzone -= EnterWarzoneCallback;
        PlayerMovement.onExitWarzone -= ExitWarzoneCallback;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canCheckForShootingEnemies)
        {
            CheckForShootingEnemies();
        }
    }

    private void EnterWarzoneCallback()
    {
        canCheckForShootingEnemies = true;
    }

    private void ExitWarzoneCallback()
    {
        canCheckForShootingEnemies = false;
    }

    private void CheckForShootingEnemies()
    {
        // world space ray origin
        Vector3 rayOrigin = shootingLine.transform.TransformVector(shootingLine.GetPosition(0));
        Vector3 worldSpaceSecondPoint = shootingLine.transform.TransformVector(shootingLine.GetPosition(1));

        Vector3 rayDirection = (worldSpaceSecondPoint - rayOrigin).normalized;
        float maxDistance = Vector3.Distance(rayOrigin, worldSpaceSecondPoint);

        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection, maxDistance, enemiesMask);

        for (int i = 0; i < hits.Length; i++)
        {
            Enemy currentEnemy = hits[i].collider.GetComponent<Enemy>();

            if (!currentEnemies.Contains(currentEnemy))
            {
                currentEnemies.Add(currentEnemy);
            }

            Debug.Log(hits[i].collider.name);            
        }

        List<Enemy> enemiesToRemove = new List<Enemy>();

        foreach (Enemy enemy in currentEnemies)
        {
            bool enemyFound = false;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.GetComponent<Enemy>() == enemy)
                {
                    enemyFound = true;
                    break;
                }
            }

            if (!enemyFound)
            {
                enemy.ShootAtPlayer();
                enemiesToRemove.Add(enemy); 
            }
        }

        foreach (Enemy enemy in enemiesToRemove)
        {
            currentEnemies.Remove(enemy);
        }
    }
}
