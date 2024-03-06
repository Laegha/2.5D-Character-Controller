using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] PlayerAttackController playerAttackController;

    private void OnTriggerEnter(Collider other)
    {
        HitableObjectHP otherHP = other.GetComponent<HitableObjectHP>();
        if (otherHP != null)
        {
            Weapon heldWeapon = playerAttackController.heldWeapon;
            otherHP.TakeDamage(heldWeapon.weaponDamage * heldWeapon.weaponTypesHolder.weaponTypes[heldWeapon.weaponType].comboAttacks[playerAttackController.currCombo -1].damageMultiplier);
        }
    }
}
