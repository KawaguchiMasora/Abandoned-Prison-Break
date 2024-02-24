using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointlLght : MonoBehaviour
{
    public float maxRotationChange = 5f; // �ő��Rotation�ω���
    public float rotationSpeed = 1f; // Rotation�̕ω����x

    private Quaternion targetRotation;

    void Start()
    {
        // �ŏ��̖ڕWRotation�������_���ɐݒ�
        GenerateRandomTargetRotation();
    }

    void Update()
    {
        // ���݂�Rotation����ڕW��Rotation�ɏ��X�ɕω�������
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // �ڕW��Rotation�ɋ߂Â�����V�����ڕW��Rotation��ݒ�
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            GenerateRandomTargetRotation();
        }
    }

    void GenerateRandomTargetRotation()
    {
        // ���݂�Rotation���擾
        Quaternion currentRotation = transform.rotation;

        // �����_���ȕω��𐶐�
        float randomX = Random.Range(-maxRotationChange, maxRotationChange);
        float randomY = Random.Range(-maxRotationChange, maxRotationChange);

        // �V����Rotation���v�Z
        Quaternion randomRotation = Quaternion.Euler(randomX, randomY, 0);

        // �ڕW��Rotation��ݒ�
        targetRotation = currentRotation * randomRotation;
    }
}
