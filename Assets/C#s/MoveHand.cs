using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHand : MonoBehaviour
{
    public Transform[] waypoints; // 目標地点の配列
    public float speed = 5f; // 移動速度
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
            // 目標地点に向かって移動
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            // 目標地点に到達したら次の目標地点へ
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else if(Who==true)
        {
            // 最終目標地点に到達したらオブジェクトを削除
            Destroy(gameObject);
        }
    }
}
