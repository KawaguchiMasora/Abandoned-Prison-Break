using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraChange : MonoBehaviour
{
    public string targetTag = "Player1"; // ターゲットのタグ
    public Cinemachine.CinemachineVirtualCamera virtualCamera; // Cinemachine VirtualCamera

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // "Player1" のタグを持つオブジェクトが当たったら指定のシネマシーンのカメラを有効化
            EnableVirtualCamera(true);
            Debug.Log("aaaaaaa");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // "Player1" のタグを持つオブジェクトがトリガーエリアから出たらカメラを無効化
            EnableVirtualCamera(false);
            Debug.Log("bbbbbbb");
        }
    }

    private void EnableVirtualCamera(bool enable)
    {
        // VirtualCameraを有効化または無効化
        virtualCamera.gameObject.SetActive(enable);
    }
}

