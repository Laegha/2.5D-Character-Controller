using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapons", order = 1)]
public class Weapon : ScriptableObject
{
    public enum EWeaponType
    {
        Sword,
        Axe,
        Bow
    }
    public WeaponTypesHolder weaponTypesHolder;
    public EWeaponType weaponType;
    public Sprite weaponSprite;
    public string weaponName;
    public string weaponDescription;
    public float weaponDamage;
}
