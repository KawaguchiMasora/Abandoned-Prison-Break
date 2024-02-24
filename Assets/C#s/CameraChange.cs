using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraChange : MonoBehaviour
{
    public string targetTag = "Player1"; // �^�[�Q�b�g�̃^�O
    public Cinemachine.CinemachineVirtualCamera virtualCamera; // Cinemachine VirtualCamera

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // "Player1" �̃^�O�����I�u�W�F�N�g������������w��̃V�l�}�V�[���̃J������L����
            EnableVirtualCamera(true);
            Debug.Log("aaaaaaa");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // "Player1" �̃^�O�����I�u�W�F�N�g���g���K�[�G���A����o����J�����𖳌���
            EnableVirtualCamera(false);
            Debug.Log("bbbbbbb");
        }
    }

    private void EnableVirtualCamera(bool enable)
    {
        // VirtualCamera��L�����܂��͖�����
        virtualCamera.gameObject.SetActive(enable);
    }
}

