using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEffects : MonoBehaviour
{
    bool disabled = false;

    private void Update()
    {
        if (!disabled)
        {
            StartCoroutine(DisableEffect());
            disabled = true;
        }
        
    }

    IEnumerator DisableEffect()
    {
        yield return new WaitForSeconds(0.7f);
        Debug.Log("disabling switch animation");
        gameObject.SetActive(false);
        disabled = false;
    }
}
