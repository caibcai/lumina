using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateButton : MonoBehaviour
{
    public MoveControl movecontrol;
    public float rotationSpeed = 180f; // Degrees per second

    private void Start()
    {
        movecontrol = FindObjectOfType<MoveControl>();
    }

    public void LeftRotate()
    {
        if (!movecontrol.rotateState) // Check if a rotation is in progress
        {
            StartCoroutine(RotateObject(movecontrol.selectedTile.onTop.transform, Vector3.up, 90));
            movecontrol.movedSteps++;
        }
    }

    public void RightRotate()
    {
        if (!movecontrol.rotateState) // Check if a rotation is in progress
        {
            StartCoroutine(RotateObject(movecontrol.selectedTile.onTop.transform, Vector3.up, -90));
            movecontrol.movedSteps++;
        }
    }

    private IEnumerator RotateObject(Transform target, Vector3 axis, float angle)
    {
        Quaternion startRotation = target.rotation;
        Quaternion endRotation = target.rotation * Quaternion.Euler(axis * angle);
        float elapsedTime = 0;

        movecontrol.rotateState = true;

        while (elapsedTime < Mathf.Abs(angle) / rotationSpeed)
        {
            target.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime * rotationSpeed / Mathf.Abs(angle));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.rotation = endRotation;

        // Wait for 1 second after rotation
        yield return new WaitForSeconds(1);

        movecontrol.rotateState = false;
    }
}
