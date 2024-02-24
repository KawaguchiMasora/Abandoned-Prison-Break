using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class WallClimbing : MonoBehaviour
{
    private CharacterController characterController;
    private Animator anim;

    [Header("壁判定")]
    [SerializeField] private float wallCheckOffset = 0.5f;
    [SerializeField] private float upperWallCheckOffset = 1.5f;
    [SerializeField] private float wallCheckDistance = 0.7f;

    [Header("崖捕まり")]
    [SerializeField] private bool isGrab;
    [SerializeField] private bool isClimb;
    private Vector3 climbOldPos;
    private Vector3 climbPos;

    [Header("その他")]
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

        // 壁判定に使用するRay
        Ray wallCheckRay = new Ray(transform.position + Vector3.up * wallCheckOffset, transform.forward);
        Ray upperCheckRay = new Ray(transform.position + Vector3.up * upperWallCheckOffset, transform.forward);

        // 壁判定を格納
        isForwardWall = Physics.Raycast(wallCheckRay, wallCheckDistance);
        isUpperWall = Physics.Raycast(upperCheckRay, wallCheckDistance);

        // 正面に壁があり、高い位置に壁がなく、接地している時にジャンプすると崖掴まりに移行
        if (isForwardWall && !isUpperWall && Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrab = true;
        }

        // 崖に捕まっているときは、重力を0にする
        if (isGrab && !isForwardWall)
        {
            acceleration.y = 0.0f;

            // 前入力されたらよじ登る
            if (inputVec != Vector3.zero)
            {
                // 開始位置を保持
                climbOldPos = transform.position;
                // 終了位置を算出
                climbPos = transform.position + transform.forward * 4 + Vector3.up * 5.5f;
                // 掴みを解除
                isGrab = false;
                // よじ登りを実行
                isClimb = true;
            }
        }

        // よじ登り状態
        if (isClimb)
        {
            // よじ登りモーションの進行度を取得
            float climbProgress = anim.GetFloat("ClimbProgress");

            // 左右は後半にかけて早く移動する
            float x = Mathf.Lerp(climbOldPos.x, climbPos.x, Ease(climbProgress));
            float z = Mathf.Lerp(climbOldPos.z, climbPos.z, Ease(climbProgress));
            // 上下は等速直線で移動
            float y = Mathf.Lerp(climbOldPos.y, climbPos.y, climbProgress);

            // 座標を更新
            transform.position = new Vector3(x, y, z);
            acceleration.y = 0.0f;

            // 進行度が8割を超えたらよじ登りの終了
            if (climbProgress >= 0.8f)
                isClimb = false;
        }
    }

    private void FixedUpdate()
    {
        // 重力を適用
        acceleration.y -= gravity * Time.fixedDeltaTime;

        // キャラクターコントローラーに移動ベクトルを適用
        characterController.Move(acceleration * Time.fixedDeltaTime);
    }

    private float Ease(float x)
    {
        return x * x * x;
    }
}
