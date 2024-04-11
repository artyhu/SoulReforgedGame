using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs_Debuffs/RestoreSoul")]
public class RestoreSoul : Buffs
{
    public int amount;
    public override void Apply(GameObject target, float reduction=0)
    {
        // access gameobject's playerhealth and heal function
        target.GetComponent<PlayerHealth>().soulRestore(amount);

    }

    private IEnumerator RevertSpriteChange(GameObject target)
    {
        yield return new WaitForSeconds(1f);
    }
}
