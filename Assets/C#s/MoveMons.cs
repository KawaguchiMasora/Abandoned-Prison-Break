using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMons : MonoBehaviour
{
    public string targetTag = "Player1"; // �^�[�Q�b�g�̃^�O
    public GameObject objectToMove; // �w��̃I�u�W�F�N�g
    public Transform targetPosition; // �ړ���̈ʒu
    public float moveDuration = 2.0f; // �ړ��ɂ����鎞��

    private bool isMoving = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && !isMoving)
        {
            // "Player1" �̃^�O�����I�u�W�F�N�g������������w��̃I�u�W�F�N�g���w��̈ʒu�܂ŏ��X�Ɉړ�
            StartCoroutine(MoveObjectToTargetPosition());
            Debug.Log("Player1 entered the trigger area");
        }
    }

    private IEnumerator MoveObjectToTargetPosition()
    {
        isMoving = true;

        float elapsedTime = 0f;
        Vector3 startingPosition = objectToMove.transform.position;

        while (elapsedTime < moveDuration)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPosition, targetPosition.position, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the target position
        objectToMove.transform.position = targetPosition.position;

        isMoving = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // "Player1" �̃^�O�����I�u�W�F�N�g���g���K�[�G���A����o���牽��������ǉ�����ꍇ�͂����ɋL�q
            Debug.Log("Player1 exited the trigger area");
        }
    }
}
