using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : CharacterCombatManager
{
    PlayerManager player;

    public WeaponItem currentWeaponBeingUsed;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    public void PerformWeaponBaseAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
    {
        if (player.IsOwner)
        {
            // 액션 수행하기.
            weaponAction.AttemptToPerformAction(player, weaponPerformingAction);

            // 수행한 액션을 서버에 알리고, 그 후 서버가 다른 클라이언트에게 수행한 액션을 보여줌
            player.playerNetworkManager.NotifyTheServerOfWeaponActionServerRpc();
        }
    }
}
