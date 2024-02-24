using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Cinemachine;

public class CameraKakudo : MonoBehaviour
{
    public float rotationSensitivity = 5.0f; // ��]�̊��x
    public float maxTiltAngle = 10.0f; // �X������E
    public float returnSpeed = 2.0f; // �߂鑬�x

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool canRotate = true;

    private float accumulatedRotationX = 0.0f;
    private float accumulatedRotationY = 0.0f;

    private void Start()
    {
        // �����̉�]��ۑ�
        initialRotation = transform.rotation;
        targetRotation = initialRotation;
    }

    private void Update()
    {
        // �Q�[���p�b�h�i�f�o�C�X�擾�j
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        // �Q�[���p�b�h�̉E�X�e�B�b�N�̓��͒l���擾
        var rightStick = gamepad.rightStick.ReadValue();

        // �㉺�̓��͂𔽓]������
        rightStick.y *= -1;

        // �I�u�W�F�N�g����]�\���ǂ����𔻒�
        if (!canRotate)
        {
            return;
        }

        // �I�u�W�F�N�g�̉�]�p�x���v�Z
        float rotationX = rightStick.y * rotationSensitivity;
        float rotationY = rightStick.x * rotationSensitivity;

        // ��]�p�x�𐧌�
        accumulatedRotationX = Mathf.Clamp(accumulatedRotationX + rotationX, -maxTiltAngle, maxTiltAngle);
        accumulatedRotationY = Mathf.Clamp(accumulatedRotationY + rotationY, -maxTiltAngle, maxTiltAngle);

        // �I�u�W�F�N�g����]������
        transform.rotation = Quaternion.Euler(initialRotation.eulerAngles.x + accumulatedRotationX, initialRotation.eulerAngles.y + accumulatedRotationY, 0.0f);

        // ���͂�0�ɂȂ����珙�X�ɏ����ʒu�ɖ߂�
        if (rightStick == Vector2.zero)
        {
            targetRotation = initialRotation;
            float step = returnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Mathf.Clamp01(step * 2.0f)); // Mathf.Clamp01��0����1�͈̔͂Ɏ��߁A���x��2�{�ɂ��邱�Ƃł�邭�߂�
            accumulatedRotationX = Mathf.Lerp(accumulatedRotationX, 0.0f, step);
            accumulatedRotationY = Mathf.Lerp(accumulatedRotationY, 0.0f, step);
        }
    }

    // ���̃X�N���v�g�����]�𖳎����邽�߂̃��\�b�h
    public void SetCanRotate(bool value)
    {
        canRotate = value;
    }
}