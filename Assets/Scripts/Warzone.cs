using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Dreamteck.Splines;

public class Warzone : MonoBehaviour
{
    [Header(" Elements ")]
    
    [SerializeField] private Transform ikTarget;
    [SerializeField] private SplineComputer newPlayerSpline;
    [SerializeField] private SplineFollower ikSplineFollower;


    [Header(" Settings ")]
    [SerializeField] private float duration;
    [SerializeField] private float animatorSpeed;
    [SerializeField] private string animationToPlay;

    // Start is called before the first frame update
    void Start()
    {
        ikSplineFollower.followDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimatingIKTarget()
    {
        
        ikSplineFollower.follow = true;
    }

    public SplineComputer GetPlayerSpline()
    {
        return newPlayerSpline;
    }

    public float GetDuration()
    {
        return duration;
    }

    public float GetAnimatorSpeed()
    {
        return animatorSpeed;
    }

    public string GetAnimationToPlay()
    {
        return animationToPlay;
    }

    public Transform GetIKTarget()
    {
        return ikTarget;
    }
}
