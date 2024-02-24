using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReyTest : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private float wallCheckOffset = 0.5f;
    [SerializeField]
    private float upperWallCheckOffset = 1.0f;
    [SerializeField]
    private float wallCheckDistance = 2.0f;

    bool isGrab = false;
    private void Update()
    {  
        // プレイヤーの正面方向を取得
        Vector3 playerForward = transform.forward;

        // レイを飛ばす原点はプレイヤーの位置
        Vector3 rayOrigin = transform.position;

        Vector3 rayclimb = targetObject.position;

        // レイキャストのヒット情報を格納する変数
        RaycastHit hit;
        RaycastHit hit2;



        Ray wallCheckRay = new Ray(transform.position + Vector3.up * wallCheckOffset, transform.forward);
        Ray upperCheckRay = new Ray(transform.position + Vector3.up * upperWallCheckOffset, transform.forward);

        //  壁判定を格納
        bool isForwardWall = Physics.Raycast(wallCheckRay, wallCheckDistance);
        bool isUpperWall = Physics.Raycast(upperCheckRay, wallCheckDistance);




        // レイキャストを飛ばし、当たったオブジェクトと距離を取得
         if (Physics.Raycast(rayOrigin, playerForward, out hit))
         {
             // レイを可視化
             Debug.DrawLine(rayOrigin, hit.point, Color.red);
         }
         else
         {
             // レイが何にも当たらなかった場合も、レイを可視化
             Debug.DrawRay(rayOrigin, playerForward * 10f, Color.red);
         }

         if(Physics.Raycast(rayclimb,playerForward,out hit2))
         {
             // レイを可視化
             Debug.DrawLine(rayclimb, hit2.point, Color.blue);
         }
         else
         {
             Debug.DrawRay(rayclimb, playerForward * 10f, Color.blue);
         }
    }
}