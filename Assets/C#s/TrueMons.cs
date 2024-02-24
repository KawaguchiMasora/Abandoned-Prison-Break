using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueMons : MonoBehaviour
{
    public string targetTag = "Player1"; // �^�[�Q�b�g�̃^�O
    public GameObject Hand;
    public GameObject Anime;
    public Vector3 targetPosition; // �ύX��̈ʒu

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

        yield return new WaitForSeconds(1f); // 1�b�ҋ@

        if (Anime != null)
        {
            // Anime�̍��W���w��̈ʒu�܂ŕύX
            Anime.transform.position = targetPosition;
        }

        Debug.Log("kowareta");
    }
}