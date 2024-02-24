using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movetochair : MonoBehaviour
{
    public float rotationSpeed = 30f; // 回転の速度
    public float angle1 = 0f; // 最初の角度
    public float angle2 = 180f; // 2番目の角度

    private bool isGoingToAngle2 = true;

    void Update()
    {
        // 毎フレームごとにオブジェクトを回転させる
        Rotate();
    }

    void Rotate()
    {
        // 現在のRotationを取得
        Quaternion currentRotation = transform.rotation;

        // 目標のRotationを設定
        Quaternion targetRotation = isGoingToAngle2 ? Quaternion.Euler(angle2, 0, 0) : Quaternion.Euler(angle1, 0, 0);

        // 新しいRotationを計算
        Quaternion newRotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 新しいRotationでオブジェクトを回転させる
        transform.rotation = newRotation;

        // 目標のRotationに到達したら切り替える
        if (Quaternion.Angle(newRotation, targetRotation) < 0.1f)
        {
            isGoingToAngle2 = !isGoingToAngle2;
        }
    }
}
