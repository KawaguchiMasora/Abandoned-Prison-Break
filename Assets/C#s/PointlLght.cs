using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointlLght : MonoBehaviour
{
    public float maxRotationChange = 5f; // 最大のRotation変化量
    public float rotationSpeed = 1f; // Rotationの変化速度

    private Quaternion targetRotation;

    void Start()
    {
        // 最初の目標Rotationをランダムに設定
        GenerateRandomTargetRotation();
    }

    void Update()
    {
        // 現在のRotationから目標のRotationに徐々に変化させる
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 目標のRotationに近づいたら新しい目標のRotationを設定
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            GenerateRandomTargetRotation();
        }
    }

    void GenerateRandomTargetRotation()
    {
        // 現在のRotationを取得
        Quaternion currentRotation = transform.rotation;

        // ランダムな変化を生成
        float randomX = Random.Range(-maxRotationChange, maxRotationChange);
        float randomY = Random.Range(-maxRotationChange, maxRotationChange);

        // 新しいRotationを計算
        Quaternion randomRotation = Quaternion.Euler(randomX, randomY, 0);

        // 目標のRotationを設定
        targetRotation = currentRotation * randomRotation;
    }
}
