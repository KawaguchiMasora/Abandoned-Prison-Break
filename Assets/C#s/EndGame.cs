using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EndGame : MonoBehaviour
{
    public Button targetButton; // ターゲットとなるボタンをInspectorで指定

    void Update()
    {
        // ターゲットのボタンがアクティブかつPADの四角ボタンが押されたらゲームを終了
        if (targetButton != null && targetButton.IsActive() && Gamepad.current != null && Gamepad.current.buttonWest.isPressed)
        {
            ExitGame();
        }
    }

    void ExitGame()
    {
        Debug.Log("ゲームを終了しました。");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
