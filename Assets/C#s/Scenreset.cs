using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scenreset : MonoBehaviour
{
    public string targetSceneName = "YourTargetScene"; // �^�[�Q�b�g�̃V�[����


    private void Update()
    {
        // P�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.P))
        {
          
        }
    }

    private void LoadTargetScene()
    {
        // �^�[�Q�b�g�̃V�[����񓯊��œǂݍ���
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneName);

        // �V�[�����ǂݍ��܂��܂őҋ@
        while (!asyncOperation.isDone)
        {
            // �������Ȃ��őҋ@
        }
    }
}
