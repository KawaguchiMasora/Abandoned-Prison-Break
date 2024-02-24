using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHand : MonoBehaviour
{
    public Transform[] waypoints; // �ڕW�n�_�̔z��
    public float speed = 5f; // �ړ����x
    public bool Who;

    private int currentWaypointIndex = 0;

    void Update()
    {
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            // �ڕW�n�_�Ɍ������Ĉړ�
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            // �ڕW�n�_�ɓ��B�����玟�̖ڕW�n�_��
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else if(Who==true)
        {
            // �ŏI�ڕW�n�_�ɓ��B������I�u�W�F�N�g���폜
            Destroy(gameObject);
        }
    }
}
