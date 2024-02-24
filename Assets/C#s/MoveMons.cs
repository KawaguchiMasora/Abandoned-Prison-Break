using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMons : MonoBehaviour
{
    public string targetTag = "Player1"; // ターゲットのタグ
    public GameObject objectToMove; // 指定のオブジェクト
    public Transform targetPosition; // 移動先の位置
    public float moveDuration = 2.0f; // 移動にかかる時間

    private bool isMoving = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && !isMoving)
        {
            // "Player1" のタグを持つオブジェクトが当たったら指定のオブジェクトを指定の位置まで徐々に移動
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
            // "Player1" のタグを持つオブジェクトがトリガーエリアから出たら何か処理を追加する場合はここに記述
            Debug.Log("Player1 exited the trigger area");
        }
    }
}
