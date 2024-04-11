using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dash : Ability
{
    [SerializeField]
    private float dashVelocity;

    public override void Activate(GameObject parent)
    {
        Debug.Log("Dashing");
    }

}
