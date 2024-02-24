using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueMons : MonoBehaviour
{
    public string targetTag = "Player1"; // ターゲットのタグ
    public GameObject Hand;
    public GameObject Anime;
    public Vector3 targetPosition; // 変更後の位置

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            StartCoroutine(ActivateAndPlayAnimations());
        }
    }

    IEnumerator ActivateAndPlayAnimations()
    {
        Hand.SetActive(true);

        yield return new WaitForSeconds(1f); // 1秒待機

        if (Anime != null)
        {
            // Animeの座標を指定の位置まで変更
            Anime.transform.position = targetPosition;
        }

        Debug.Log("kowareta");
    }
}