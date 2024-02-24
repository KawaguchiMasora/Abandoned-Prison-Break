using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhond : MonoBehaviour
{
    public float rotationSpeed = 15f; // ��]�̑���
    public float rotationRange = 15f; // ��]�͈̔�

    private Quaternion initialRotation;

    void Start()
    {
        // �����̉�]��ۑ�
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Y����]��ω�������
        float newYRotation = initialRotation.eulerAngles.y + Mathf.Sin(Time.time * rotationSpeed) * rotationRange;

        // Z����]��ω�������
        float newZRotation = initialRotation.eulerAngles.z + Mathf.Sin(Time.time * rotationSpeed) * rotationRange;

        // �V������]�l���Z�b�g
        transform.rotation = Quaternion.Euler(initialRotation.eulerAngles.x, newYRotation, newZRotation);
    }
}
