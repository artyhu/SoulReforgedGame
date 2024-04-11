using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float cd;
    public float activeTime;

    public virtual void Activate(GameObject parent) { }
}
