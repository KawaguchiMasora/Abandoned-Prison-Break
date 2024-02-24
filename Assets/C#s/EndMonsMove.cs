using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EndMonsMove : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    public string targetTag = "Player1"; // ターゲットのタグ
    bool A = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target &&A==false)
        {
            agent.destination = target.transform.position;
        }
    }

  
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(targetTag))
        {

            A = true;
        }
    }
}
