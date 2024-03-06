using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGFXMovingRotationManager : MonoBehaviour
{
    [SerializeField] Transform rotatedObject;
    bool isFlipped = false;

    [SerializeField] float minRotation;
    [SerializeField] float mediumRotation;
    [SerializeField] float maxRotation;

    [SerializeField] float rotationDuration;

    Rotator currRotator;

    public void RotateCharacter(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            currRotator = new Rotator(rotatedObject.localRotation.eulerAngles.y, isFlipped ? 180f : 0f, rotationDuration, rotatedObject);
            return;
        }

        if (context.performed)
        {
            Vector2 movement = context.ReadValue<Vector2>();

            if (movement.x < 0 && !isFlipped)
                isFlipped = true;
            if (movement.x > 0 && isFlipped)
                isFlipped = false;

            float initialValue = isFlipped ? 180f : 0f;
            float dirMultiplier = isFlipped ? 1f : -1f;
            float valueVariation = movement.x == 0f ? maxRotation : mediumRotation;

            currRotator = new Rotator(rotatedObject.localRotation.eulerAngles.y, initialValue + valueVariation * movement.y * dirMultiplier + (movement.y == 0f ? minRotation * dirMultiplier : 0f), rotationDuration, rotatedObject);
            
        }

    }

    private void Update()
    {
        if (currRotator != null)
        {
            if (currRotator.hasFinished)
            {
                currRotator = null;
                return;
            }

            currRotator.RotateTo();
        }
    }
}

