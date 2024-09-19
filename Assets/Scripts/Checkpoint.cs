using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SpriteRenderer gradient;

    [Header("Actions")]
    public static Action <Checkpoint> onInteracted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        gradient.color = Color.green;

        onInteracted?.Invoke(this);
    }


    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
