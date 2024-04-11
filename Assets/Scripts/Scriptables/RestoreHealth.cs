using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs_Debuffs/RestoreHealth")]
public class RestoreHealth : Buffs
{
    public float amount;
    public override void Apply(GameObject target, float reduction)
    {
        // access gameobject's playerhealth and heal function
        target.GetComponent<PlayerHealth>().Heal(amount - reduction);
        target.GetComponent<PlayerSounds>().PlayAudio("heal");
    }
}
