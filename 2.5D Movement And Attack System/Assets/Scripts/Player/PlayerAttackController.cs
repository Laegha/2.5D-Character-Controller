using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    public Weapon heldWeapon;
    [HideInInspector] public int currCombo = 0;

    [SerializeField] PlayerStateMachine playerStateMachine;

    public Transform playerGFX;
    [HideInInspector] public int desiredNormalAttackRotation;
    [HideInInspector] public float rotationTime;

    [HideInInspector] public bool animationFinished;
    [SerializeField] float comboLossCooldown;

    public void NormalAttack(InputAction.CallbackContext context) 
    {
        if(context.canceled)
            playerStateMachine.IsAttacking = true;
    }

    public void NormalAttackPerformed()//this function is called when the AttackState begins. It handles combo count and animation playing
    {
        string weaponType = Enum.GetName(typeof(Weapon.EWeaponType), heldWeapon.weaponType);
        playerStateMachine.PlayerAnimator.Play(weaponType + "_" + currCombo);

        StartCoroutine(AnimationTimer());

        ComboAttack[] comboAttacks = heldWeapon.weaponTypesHolder.weaponTypes[heldWeapon.weaponType].comboAttacks;
        desiredNormalAttackRotation = comboAttacks[currCombo].desiredRotation;
        rotationTime = comboAttacks[currCombo].rotationTime;

        currCombo++;
        if (currCombo > comboAttacks.Length-1)
            currCombo = 0;
    }

    IEnumerator AnimationTimer()//no quiero hacer todo con una coroutine, seria ideal que esto este en el update del attackstate
    {
        yield return new WaitForEndOfFrame();
        float animationTimer = playerStateMachine.PlayerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        yield return new WaitForSeconds(animationTimer);
        animationFinished = true;

    }

    public void StartComboResetCooldown() => StartCoroutine(ComboResetCountdown());

    IEnumerator ComboResetCountdown()
    {
        float timer = comboLossCooldown;
        while (!playerStateMachine.IsAttacking)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                currCombo = 0;
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void ChangeDesiredRotation(int newRotation) => desiredNormalAttackRotation = newRotation;

    public void ChangeRotationTime(float newTime) => rotationTime = newTime;
}
