using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Cinemachine;

public class CameraKakudo : MonoBehaviour
{
    public float rotationSensitivity = 5.0f; // 回転の感度
    public float maxTiltAngle = 10.0f; // 傾ける限界
    public float returnSpeed = 2.0f; // 戻る速度

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool canRotate = true;

    private float accumulatedRotationX = 0.0f;
    private float accumulatedRotationY = 0.0f;

    private void Start()
    {
        // 初期の回転を保存
        initialRotation = transform.rotation;
        targetRotation = initialRotation;
    }

    private void Update()
    {
        // ゲームパッド（デバイス取得）
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        // ゲームパッドの右スティックの入力値を取得
        var rightStick = gamepad.rightStick.ReadValue();

        // 上下の入力を反転させる
        rightStick.y *= -1;

        // オブジェクトが回転可能かどうかを判定
        if (!canRotate)
        {
            return;
        }

        // オブジェクトの回転角度を計算
        float rotationX = rightStick.y * rotationSensitivity;
        float rotationY = rightStick.x * rotationSensitivity;

        // 回転角度を制限
        accumulatedRotationX = Mathf.Clamp(accumulatedRotationX + rotationX, -maxTiltAngle, maxTiltAngle);
        accumulatedRotationY = Mathf.Clamp(accumulatedRotationY + rotationY, -maxTiltAngle, maxTiltAngle);

        // オブジェクトを回転させる
        transform.rotation = Quaternion.Euler(initialRotation.eulerAngles.x + accumulatedRotationX, initialRotation.eulerAngles.y + accumulatedRotationY, 0.0f);

        // 入力が0になったら徐々に初期位置に戻す
        if (rightStick == Vector2.zero)
        {
            targetRotation = initialRotation;
            float step = returnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Mathf.Clamp01(step * 2.0f)); // Mathf.Clamp01で0から1の範囲に収め、速度を2倍にすることでゆるく戻す
            accumulatedRotationX = Mathf.Lerp(accumulatedRotationX, 0.0f, step);
            accumulatedRotationY = Mathf.Lerp(accumulatedRotationY, 0.0f, step);
        }
    }

    // 他のスクリプトから回転を無視するためのメソッド
    public void SetCanRotate(bool value)
    {
        canRotate = value;
    }
}