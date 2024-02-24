using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTure : MonoBehaviour
{
    public string targetTag = "Player1"; // ターゲットのタグ
    public GameObject[] Hand; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            for(int i= 0; i<= Hand.Length; i++)
            {
                Hand[i].SetActive(true);
            }
        }
    }
}
