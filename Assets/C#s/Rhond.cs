using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhond : MonoBehaviour
{
    public float rotationSpeed = 15f; // 回転の速さ
    public float rotationRange = 15f; // 回転の範囲

    private Quaternion initialRotation;

    void Start()
    {
        // 初期の回転を保存
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Y軸回転を変化させる
        float newYRotation = initialRotation.eulerAngles.y + Mathf.Sin(Time.time * rotationSpeed) * rotationRange;

        // Z軸回転を変化させる
        float newZRotation = initialRotation.eulerAngles.z + Mathf.Sin(Time.time * rotationSpeed) * rotationRange;

        // 新しい回転値をセット
        transform.rotation = Quaternion.Euler(initialRotation.eulerAngles.x, newYRotation, newZRotation);
    }
}
