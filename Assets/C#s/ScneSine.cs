using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScneSine : MonoBehaviour
{
    public float inputLifetime = 10.0f; // 入力情報の寿命（秒）

    private float sceneLoadTime;

    void Start()
    {
        // シーンがロードされた時刻を記録
        sceneLoadTime = Time.time;
    }

    void Update()
    {
        // シーンがロードされてから一定時間が経過したら
        if (Time.time - sceneLoadTime >= inputLifetime)
        {
            // ここに入力情報の破棄処理を追加
            // 例: Input.ResetInputAxes();
        }

        // 通常の入力処理をここに追加
    }
}
