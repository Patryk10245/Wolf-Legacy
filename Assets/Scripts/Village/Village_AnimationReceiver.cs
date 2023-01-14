using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village_AnimationReceiver : MonoBehaviour
{
    public Village_UI_Control villageUI;
    public AudioListener audioListener;

    public void AnimEvent_BuyPaladin()
    {
        villageUI.FixPaladinBuilding();
    }
    public void AnimEvent_BuyBarbarian()
    {
        villageUI.FixBarbarianBuilding();
    }
    public void AnimEvent_BuyArcher()
    {
        villageUI.FixArcherBuilding();
    }
    public void AnimEvent_BuyMage()
    {
        villageUI.FixMageBuilding();
    }
}
