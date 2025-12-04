using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chacter Actions/Weapon Actions/Test Actions")]
public class WeaponItemAction : ScriptableObject
{
    public int actionID;

    public virtual void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        // 무기 액션에 어떤 공통점이 존재하는가?
        // 1. 현재 어떤 무기가 사용되는지 항상 트랙해야함.
        if (playerPerformingAction.IsOwner)
        {
            playerPerformingAction.playerNetworkManager.currentWeaponBeingUsed.Value = weaponPerformingAction.itemID;
        }

        Debug.Log("The Action has Fired");
    }
}
