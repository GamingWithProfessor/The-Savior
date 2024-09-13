using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header ("Elements")]
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRunAnimation()
    {
       Play ("Run");
      //animator.Play ("Run");
    }    

    public void Play(string animationName)
    {
      animator.Play(animationName);
    }

    public void Play(string animationName, float animatorSpeed)
    {
      animator.speed = animatorSpeed;
      Play(animationName);
    }
  
}
