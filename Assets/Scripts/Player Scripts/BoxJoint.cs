using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxJoint : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerBody;

    [SerializeField] private Vector3 scaleDown = new Vector3(1.2f, 0.8f, 1.2f);
    [SerializeField] private Vector3 scaleUp = new Vector3(0.8f, 1.2f, 0.8f);

    [SerializeField] private float scaleKoefficient;
    [SerializeField] private float rotationKoefficient;

    private void Update()
    {
        Vector3 relativePosition = playerTransform.InverseTransformPoint(transform.position);
        float interpolant = relativePosition.y * scaleKoefficient;
        Vector3 scale = Lerp3(scaleDown, Vector3.one, scaleUp, interpolant);
        playerBody.localScale = scale;
        playerBody.localEulerAngles = new Vector3(relativePosition.z, 0, -relativePosition.x) * rotationKoefficient;
    }

    Vector3 Lerp3(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        if (t < 0)
            return Vector3.LerpUnclamped(a, b, t + 1f);
        else
            return Vector3.LerpUnclamped(b, c, t);
    }
}
