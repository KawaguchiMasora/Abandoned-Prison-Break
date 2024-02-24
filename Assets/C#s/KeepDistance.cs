using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDistance : MonoBehaviour
{
    public GameObject targetObject; // �w��̃I�u�W�F�N�g
    public float offsetDistance = 5f; // �ێ�����������
    public float smoothTime = 0.3f; // �����̎���

    public bool followX = true; // X�����t�H���[���邩�ǂ����̃t���O
    public bool followY = true; // Y�����t�H���[���邩�ǂ����̃t���O
    public bool followZ = true; // Z�����t�H���[���邩�ǂ����̃t���O

    private Vector3 velocity; // SmoothDamp �Ŏg�p���鑬�x�x�N�g��

    void Update()
    {
        // �^�[�Q�b�g�̈ʒu
        Vector3 targetPosition = targetObject.transform.position;

        // �A�^�b�`���ꂽ�I�u�W�F�N�g�̈ʒu
        Vector3 currentPosition = transform.position;

        // �A�^�b�`���ꂽ�I�u�W�F�N�g���^�[�Q�b�g�Ɍ����Ċ��炩�Ɉړ�
        float currentX = followX ? Mathf.SmoothDamp(currentPosition.x, targetPosition.x + offsetDistance, ref velocity.x, smoothTime) : currentPosition.x;
        float currentY = followY ? Mathf.SmoothDamp(currentPosition.y, targetPosition.y + offsetDistance, ref velocity.y, smoothTime) : currentPosition.y;
        float currentZ = followZ ? Mathf.SmoothDamp(currentPosition.z, targetPosition.z + offsetDistance, ref velocity.z, smoothTime) : currentPosition.z;

        transform.position = new Vector3(currentX, currentY, currentZ);
    }
}
