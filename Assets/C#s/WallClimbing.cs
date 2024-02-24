using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class WallClimbing : MonoBehaviour
{
    private CharacterController characterController;
    private Animator anim;

    [Header("�ǔ���")]
    [SerializeField] private float wallCheckOffset = 0.5f;
    [SerializeField] private float upperWallCheckOffset = 1.5f;
    [SerializeField] private float wallCheckDistance = 0.7f;

    [Header("�R�߂܂�")]
    [SerializeField] private bool isGrab;
    [SerializeField] private bool isClimb;
    private Vector3 climbOldPos;
    private Vector3 climbPos;

    [Header("���̑�")]
    [SerializeField] private float gravity = 15.0f;
    [SerializeField] private float jumpSpeed = 7.0f;

    private Vector3 acceleration;
    private Vector3 inputVec;

    private bool isGrounded;
    private bool isForwardWall;
    private bool isUpperWall;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;
        inputVec = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // �ǔ���Ɏg�p����Ray
        Ray wallCheckRay = new Ray(transform.position + Vector3.up * wallCheckOffset, transform.forward);
        Ray upperCheckRay = new Ray(transform.position + Vector3.up * upperWallCheckOffset, transform.forward);

        // �ǔ�����i�[
        isForwardWall = Physics.Raycast(wallCheckRay, wallCheckDistance);
        isUpperWall = Physics.Raycast(upperCheckRay, wallCheckDistance);

        // ���ʂɕǂ�����A�����ʒu�ɕǂ��Ȃ��A�ڒn���Ă��鎞�ɃW�����v����ƊR�͂܂�Ɉڍs
        if (isForwardWall && !isUpperWall && Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrab = true;
        }

        // �R�ɕ߂܂��Ă���Ƃ��́A�d�͂�0�ɂ���
        if (isGrab && !isForwardWall)
        {
            acceleration.y = 0.0f;

            // �O���͂��ꂽ��悶�o��
            if (inputVec != Vector3.zero)
            {
                // �J�n�ʒu��ێ�
                climbOldPos = transform.position;
                // �I���ʒu���Z�o
                climbPos = transform.position + transform.forward * 4 + Vector3.up * 5.5f;
                // �݂͂�����
                isGrab = false;
                // �悶�o������s
                isClimb = true;
            }
        }

        // �悶�o����
        if (isClimb)
        {
            // �悶�o�胂�[�V�����̐i�s�x���擾
            float climbProgress = anim.GetFloat("ClimbProgress");

            // ���E�͌㔼�ɂ����đ����ړ�����
            float x = Mathf.Lerp(climbOldPos.x, climbPos.x, Ease(climbProgress));
            float z = Mathf.Lerp(climbOldPos.z, climbPos.z, Ease(climbProgress));
            // �㉺�͓��������ňړ�
            float y = Mathf.Lerp(climbOldPos.y, climbPos.y, climbProgress);

            // ���W���X�V
            transform.position = new Vector3(x, y, z);
            acceleration.y = 0.0f;

            // �i�s�x��8���𒴂�����悶�o��̏I��
            if (climbProgress >= 0.8f)
                isClimb = false;
        }
    }

    private void FixedUpdate()
    {
        // �d�͂�K�p
        acceleration.y -= gravity * Time.fixedDeltaTime;

        // �L�����N�^�[�R���g���[���[�Ɉړ��x�N�g����K�p
        characterController.Move(acceleration * Time.fixedDeltaTime);
    }

    private float Ease(float x)
    {
        return x * x * x;
    }
}
