using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EndGame : MonoBehaviour
{
    public Button targetButton; // �^�[�Q�b�g�ƂȂ�{�^����Inspector�Ŏw��

    void Update()
    {
        // �^�[�Q�b�g�̃{�^�����A�N�e�B�u����PAD�̎l�p�{�^���������ꂽ��Q�[�����I��
        if (targetButton != null && targetButton.IsActive() && Gamepad.current != null && Gamepad.current.buttonWest.isPressed)
        {
            ExitGame();
        }
    }

    void ExitGame()
    {
        Debug.Log("�Q�[�����I�����܂����B");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
