using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movetochair : MonoBehaviour
{
    public float rotationSpeed = 30f; // ��]�̑��x
    public float angle1 = 0f; // �ŏ��̊p�x
    public float angle2 = 180f; // 2�Ԗڂ̊p�x

    private bool isGoingToAngle2 = true;

    void Update()
    {
        // ���t���[�����ƂɃI�u�W�F�N�g����]������
        Rotate();
    }

    void Rotate()
    {
        // ���݂�Rotation���擾
        Quaternion currentRotation = transform.rotation;

        // �ڕW��Rotation��ݒ�
        Quaternion targetRotation = isGoingToAngle2 ? Quaternion.Euler(angle2, 0, 0) : Quaternion.Euler(angle1, 0, 0);

        // �V����Rotation���v�Z
        Quaternion newRotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

        // �V����Rotation�ŃI�u�W�F�N�g����]������
        transform.rotation = newRotation;

        // �ڕW��Rotation�ɓ��B������؂�ւ���
        if (Quaternion.Angle(newRotation, targetRotation) < 0.1f)
        {
            isGoingToAngle2 = !isGoingToAngle2;
        }
    }
}
