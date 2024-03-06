using UnityEngine;

public class Rotator
{
    float originalRotation;
    float desiredRotation;
    Transform transform;
    float rotationDuration;
    float rotationSpeed;
    float rotationDir = 1;

    float rotated = 0;
    float rotationNeeded;
    public bool hasFinished;

    public Rotator(float originalRotation, float desiredRotation, float rotationDuration, Transform transform)
    {
        this.originalRotation = NormalizeAngle(originalRotation);
        this.desiredRotation = NormalizeAngle(desiredRotation);
        this.rotationDuration = rotationDuration;
        this.transform = transform;
        GetRotationDirection();
    }

    void GetRotationDirection()
    {
        rotationNeeded = desiredRotation - originalRotation;

        if (rotationNeeded < 0)
        {
            rotationDir = -1;
            rotationNeeded *= -1;
        }
        rotationSpeed = rotationNeeded / rotationDuration;
    }

    public void RotateTo()
    {
        if (rotated >= rotationNeeded)
        {
            hasFinished = true;
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, desiredRotation, transform.localRotation.z);
            return;
        }

        float deltaRotation = rotationSpeed * Time.deltaTime;
        rotated += deltaRotation;

        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles + new Vector3(0, deltaRotation * rotationDir, 0));
    }

    float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 270)
            return angle - 360;
        else if (angle < -270)
            return angle + 360;
        else
            return angle;
    }
}
