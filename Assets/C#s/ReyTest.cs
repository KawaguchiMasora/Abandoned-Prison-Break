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
        // �v���C���[�̐��ʕ������擾
        Vector3 playerForward = transform.forward;

        // ���C���΂����_�̓v���C���[�̈ʒu
        Vector3 rayOrigin = transform.position;

        Vector3 rayclimb = targetObject.position;

        // ���C�L���X�g�̃q�b�g�����i�[����ϐ�
        RaycastHit hit;
        RaycastHit hit2;



        Ray wallCheckRay = new Ray(transform.position + Vector3.up * wallCheckOffset, transform.forward);
        Ray upperCheckRay = new Ray(transform.position + Vector3.up * upperWallCheckOffset, transform.forward);

        //  �ǔ�����i�[
        bool isForwardWall = Physics.Raycast(wallCheckRay, wallCheckDistance);
        bool isUpperWall = Physics.Raycast(upperCheckRay, wallCheckDistance);




        // ���C�L���X�g���΂��A���������I�u�W�F�N�g�Ƌ������擾
         if (Physics.Raycast(rayOrigin, playerForward, out hit))
         {
             // ���C������
             Debug.DrawLine(rayOrigin, hit.point, Color.red);
         }
         else
         {
             // ���C�����ɂ�������Ȃ������ꍇ���A���C������
             Debug.DrawRay(rayOrigin, playerForward * 10f, Color.red);
         }

         if(Physics.Raycast(rayclimb,playerForward,out hit2))
         {
             // ���C������
             Debug.DrawLine(rayclimb, hit2.point, Color.blue);
         }
         else
         {
             Debug.DrawRay(rayclimb, playerForward * 10f, Color.blue);
         }
    }
}