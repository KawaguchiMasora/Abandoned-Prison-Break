using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDistance : MonoBehaviour
{
    public GameObject targetObject; // 指定のオブジェクト
    public float offsetDistance = 5f; // 保持したい距離
    public float smoothTime = 0.3f; // 慣性の時間

    public bool followX = true; // X軸をフォローするかどうかのフラグ
    public bool followY = true; // Y軸をフォローするかどうかのフラグ
    public bool followZ = true; // Z軸をフォローするかどうかのフラグ

    private Vector3 velocity; // SmoothDamp で使用する速度ベクトル

    void Update()
    {
        // ターゲットの位置
        Vector3 targetPosition = targetObject.transform.position;

        // アタッチされたオブジェクトの位置
        Vector3 currentPosition = transform.position;

        // アタッチされたオブジェクトをターゲットに向けて滑らかに移動
        float currentX = followX ? Mathf.SmoothDamp(currentPosition.x, targetPosition.x + offsetDistance, ref velocity.x, smoothTime) : currentPosition.x;
        float currentY = followY ? Mathf.SmoothDamp(currentPosition.y, targetPosition.y + offsetDistance, ref velocity.y, smoothTime) : currentPosition.y;
        float currentZ = followZ ? Mathf.SmoothDamp(currentPosition.z, targetPosition.z + offsetDistance, ref velocity.z, smoothTime) : currentPosition.z;

        transform.position = new Vector3(currentX, currentY, currentZ);
    }
}
