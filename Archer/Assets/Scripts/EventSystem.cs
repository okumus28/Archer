using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public bool canUpgrade = false;
    public Animator openUpgradeButtonAnimator;

    private void Update()
    {
        if (openUpgradeButtonAnimator.gameObject.activeSelf == true)
        {
            if (canUpgrade)
            {
                openUpgradeButtonAnimator.SetBool("Upgrade" , true);
            }
            else
            {
                openUpgradeButtonAnimator.SetBool("Upgrade" , false);
            }
        }
    }

}
