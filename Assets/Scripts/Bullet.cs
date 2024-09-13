using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header(" Settings")]
    [SerializeField] private LayerMask enemiesMask;
    [SerializeField] private float detectionRadius;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        Destroy (gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForEnemies();
    }

    private void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }

    public void Configure(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void CheckForEnemies()
    {
        Collider[] detectedEnemies = Physics.OverlapSphere(transform.position, detectionRadius, enemiesMask);
        foreach (Collider enemyCollider in detectedEnemies)
        {
            enemyCollider.GetComponent<Enemy>().TakeDamage();
        }
    }
}
