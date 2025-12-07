using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public MeleeWeaponDamageCollider meleeWeaponDamageCollider;

    private void Awake()
    {
        meleeWeaponDamageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
    }

    public void SetWeaponDamage(CharacterManager characterWieldingWeapon ,WeaponItem weapon)
    {
        meleeWeaponDamageCollider.characterCausingDamage = characterWieldingWeapon;
        Debug.Log($"weaponManager , is Owner : {meleeWeaponDamageCollider.characterCausingDamage.IsOwner}");
        meleeWeaponDamageCollider.physicalDamage = weapon.physicalDamage;
        meleeWeaponDamageCollider.elementalDamage = weapon.elementalDamage;

        meleeWeaponDamageCollider.light_Attack_01_Modifier = weapon.light_Attack_01_Modifier;
    }
}
