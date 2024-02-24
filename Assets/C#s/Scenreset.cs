using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scenreset : MonoBehaviour
{
    public string targetSceneName = "YourTargetScene"; // ターゲットのシーン名


    private void Update()
    {
        // Pキーが押されたら
        if (Input.GetKeyDown(KeyCode.P))
        {
          
        }
    }

    private void LoadTargetScene()
    {
        // ターゲットのシーンを非同期で読み込む
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneName);

        // シーンが読み込まれるまで待機
        while (!asyncOperation.isDone)
        {
            // 何もしないで待機
        }
    }
}
