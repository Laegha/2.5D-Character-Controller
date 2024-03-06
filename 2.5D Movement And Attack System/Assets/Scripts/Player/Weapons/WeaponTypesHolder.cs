using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponTypes", menuName = "ScriptableObjects/WeaponTypes", order = 1)]
public class WeaponTypesHolder : ScriptableObject
{
    public SerializedDictionary<Weapon.EWeaponType, WeaponClass> weaponTypes;
}

[System.Serializable]
public class WeaponClass
{
    public ComboAttack[] comboAttacks;
}

[System.Serializable]
public class ComboAttack
{
    public int desiredRotation = -60;
    public float rotationTime = .1f;
    public float damageMultiplier;
}