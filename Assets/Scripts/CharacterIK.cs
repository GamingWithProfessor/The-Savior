using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterIK : MonoBehaviour
{
    [SerializeField] public RigBuilder rigBuilder;

    [Header ("Constrains")]
    [SerializeField] private TwoBoneIKConstraint[] twoBoneIKConstraint;
    [SerializeField] private MultiAimConstraint[] multiAimConstraint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfigureIk(Transform ikTarget)
    {
        rigBuilder.enabled = true;

        foreach (TwoBoneIKConstraint twoBoneIKConstraint in twoBoneIKConstraint)
            twoBoneIKConstraint.data.target = ikTarget;

        foreach (MultiAimConstraint multiAimConstraint in multiAimConstraint)
        {
            WeightedTransformArray weightedTransforms = new WeightedTransformArray();
            weightedTransforms.Add(new WeightedTransform(ikTarget, 1));

            multiAimConstraint.data.sourceObjects = weightedTransforms;
        }

        rigBuilder.Build();
        
    }

     public void DisableIk()
    {
        rigBuilder.enabled = false;
    }

    internal void ConfigureIk(object ikTarget)
    {
        throw new NotImplementedException();
    }
}
