using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerMovement : MonoBehaviour
{ enum State {Idle, Run, Warzone }


    [Header ("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float slowMoScale;

    [Header ("Testing")]
    [SerializeField] private int Framerate;
    // Start is called before the first frame update

    [Header ("Elemets")]
    [SerializeField] private PlayerAnimator playeranimator;
    [SerializeField] private PlayerIK playerIK;
    

    private State state;
    private Warzone currentWarzone;

    [Header ("Spline Settings")]
    private float warZoneTimer;

    [Header ("Actions")]
    public static Action onEnterWarzone;
    public static Action onExitWarzone;



    void Start()
    {
        Application.targetFrameRate = Framerate;
        state = State.Idle;

    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
       StartRunning();

       ManageState();
       
    }

   private void ManageState()
   {
     switch (state)
     {
        case State.Idle:
                break;

        case State.Run:
            Move();
                break;

        case State.Warzone:
            ManageWarZoneState();
                break;
     }
   }

    private void StartRunning()
    {
        state = State.Run;
        playeranimator.PlayRunAnimation();
    }

    private void Move ()
    {
      transform.position += transform.right * moveSpeed * Time.deltaTime;
    }

    public void EnteredWarZoreCallBack(Warzone warzone)
    {
      if (currentWarzone != null)
      return;

      state = State.Warzone;
      
      currentWarzone = warzone;

      currentWarzone.StartAnimationIKTarget();
      warZoneTimer = 0;
      playeranimator.Play(currentWarzone.GetAnimationToPlay(), currentWarzone.GetAnimatorSpeed());

      Time.timeScale = slowMoScale;
      Time.fixedDeltaTime = slowMoScale / 50;
      

      playerIK.ConfigureIk(currentWarzone.GetIKTarget());

      onEnterWarzone?.Invoke();
      
      Debug.Log("Entered Warzone");
    }

    public void ManageWarZoneState()
    {
      if (currentWarzone == null) return;
       warZoneTimer +=Time.deltaTime;
       float splinePercent = warZoneTimer/currentWarzone.GetDuration();
       transform.position = currentWarzone.GetPlayerSpline().EvaluatePosition(splinePercent);

       if (splinePercent >= 1)
       ExitWarZone();
    }

    private void ExitWarZone()
    {
      state = State.Run;
      currentWarzone = null;

      playeranimator.Play("Run", 1);

      Time.timeScale = 1;
      Time.fixedDeltaTime = 1f / 50;

      onExitWarzone?.Invoke();

      playerIK.DisableIk();

    }
}
