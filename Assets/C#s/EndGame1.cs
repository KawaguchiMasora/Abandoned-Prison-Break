using UnityEngine;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class EndGame1 : MonoBehaviour
{
    public string targetTag = "Player1"; // ターゲットのタグ
    public string targetSceneName; // 遷移先シーンの名前

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Player1 entered the trigger zone.");

            // フェードトランジションの設定
            var fader = new FadeTransition()
            {
                fadedDelay = 1.0f,
                fadeToColor = Color.black

            };
            TransitionKit.instance.transitionWithDelegate(fader);
            SceneManager.LoadScene(targetSceneName);

            Debug.Log("Transition initiated.");
        }
    }
}





