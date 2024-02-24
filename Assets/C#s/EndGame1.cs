using UnityEngine;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class EndGame1 : MonoBehaviour
{
    public string targetTag = "Player1"; // �^�[�Q�b�g�̃^�O
    public string targetSceneName; // �J�ڐ�V�[���̖��O

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Player1 entered the trigger zone.");

            // �t�F�[�h�g�����W�V�����̐ݒ�
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





