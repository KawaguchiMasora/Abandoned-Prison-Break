using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

[RequireComponent(typeof(CharacterController))]

public class PADtest : MonoBehaviour
{
    [Header("移動の速さ"), SerializeField]
    private float _speed = 3;

    [Header("しゃがんでいる時の速さ"), SerializeField]
    private float _SitSpeed = 6;

    [Header("ジャンプする瞬間の速さ"), SerializeField]
    private float _jumpSpeed = 7;

    [Header("重力加速度"), SerializeField]
    private float _gravity = 15;

    [Header("落下時の速さ制限（Infinityで無制限）"), SerializeField]
    private float _fallSpeed = 10;

    [Header("落下の初速"), SerializeField]
    private float _initFallSpeed = 2;

    [Header("アニメーター"), SerializeField]
    Animator animator;


    private Transform _transform;
    private CharacterController _characterController;
    private Vector2 _inputMove;
    private float _verticalVelocity;
    private float _turnVelocity;
    private bool _isGroundedPrev;
    private bool _isSiting;

    [SerializeField]
    private Transform targetObject;

    [SerializeField] private float wallCheckOffset = 0.5f;
    [SerializeField] private float upperWallCheckOffset = 1.0f;
    [SerializeField] private float wallCheckRayDistance = 2.0f; 
    bool isGrab = false;
    bool isClimb = false;
    private Vector3 climbOldPos; 
    private Vector3 climbPos;     

    bool isForwardWall;
    bool isUpperWall;

    private float currentValue = 0.0f;
    private float targetValue = 10.0f;
    private float duration = 9.0f;

    private bool _isDashing;
    private bool _isDes;
    private bool lodseen = false;

    public string targetTag = "Enemy"; 

    public string targetSceneName; 

    [Header("初期の高さ"), SerializeField]
    private float initialHeight = 1.78f;

    [Header("しゃがみ時の高さ"), SerializeField]
    private float crouchingHeight = 1.0f;



    private IEnumerator TransitionWithDelay(float delay)
    {

        
        yield return new WaitForSeconds(delay);
        
        var fader = new FadeTransition()
        {
            fadedDelay = 2.0f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);

        Debug.Log("Transition initiated.");


        // シーン遷移
        SceneManager.LoadScene(targetSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(targetTag))
        {
            animator.SetBool("Des", _isDes);

            // コルーチンを開始
            StartCoroutine(TransitionWithDelay(2.0f));
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       
        _inputMove = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || !_characterController.isGrounded || _isDashing) return;

        _verticalVelocity = _jumpSpeed;
        if (isForwardWall && !isUpperWall)
        {
            isGrab = true;
            Debug.Log("つかみます");
            animator.SetBool("IsGrab", _characterController.isGrounded);
        }
    }

    public void OnDash(InputAction.CallbackContext context)//ダッシュではなくしゃがみ
    {
        if (context.performed)
        {
            // 四角ボタンが押されたとき
            _isDashing = !_isDashing; // trueならfalseに、falseならtrueに切り替える
            animator.SetBool("IsDashing", _isDashing);
            if (_isDashing ==true)
            {
                _characterController.height = crouchingHeight;
                Debug.Log("ooooo");
            }
            if(_isDashing == false)
            {
                _characterController.height = initialHeight;
                Debug.Log("iiiii");
            }
        }
    }


    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyAnimatorParameter();
        Ray();
        _isDes = true;

        var isGrounded = _characterController.isGrounded;
       
        if (isGrounded && !_isGroundedPrev)
        {
            _verticalVelocity = -_initFallSpeed;
        }
        else if (!isGrounded)
        {
            _verticalVelocity -= _gravity * Time.deltaTime;

            if (_verticalVelocity < -_fallSpeed)
                _verticalVelocity = -_fallSpeed;
        }

        _isGroundedPrev = isGrounded;

    
        // 移動速度の設定
        var moveSpeed = _isSiting ? _SitSpeed : (_isDashing ? _SitSpeed : _speed);

        var moveVelocity = new Vector3(
            _inputMove.x * moveSpeed,
            _verticalVelocity,
            _inputMove.y * moveSpeed
        );

        var moveDelta = moveVelocity * Time.deltaTime;

        _characterController.Move(moveDelta);

        if (_inputMove != Vector2.zero)
        {
            var targetAngleY = -Mathf.Atan2(_inputMove.y, _inputMove.x) * Mathf.Rad2Deg + 90;
            var angleY = Mathf.SmoothDampAngle(
                _transform.eulerAngles.y,
                targetAngleY,
                ref _turnVelocity,
                0.1f
            );
            _transform.rotation = Quaternion.Euler(0, angleY, 0);
        }

        if (isGrab && _characterController.isGrounded)  // isGrounded をチェック
        {
            animator.SetBool("IsGrab", false);  // グラブが有効でかつ地面にいる場合は IsGrab を false に設定
            isGrab = false;  // グラブを解除
            Debug.Log("waa");
        }


        //  崖に捕まっているときは、重力を0にする
        //isGrabがtreuで一つ目のレイが壁に当たっていなかったら処理が走る
        if (isGrab && !isForwardWall)
        {
            _verticalVelocity = 0.0f;


            
            //  前入力されたらよじ登る
            if (_inputMove.magnitude > 0.1f)
             {
                Debug.Log("test");
                //  開始位置を保持
                climbOldPos = transform.position;
                Debug.Log(climbOldPos);
                //  終了位置を算出
                climbPos = transform.position + transform.forward + Vector3.up;
                Debug.Log(climbPos);
                //  掴みを解除
                isGrab = false;
                //  よじ登りを実行
                isClimb = true;
            }
        }
        if (isClimb)
        {
            //入力を無視するを追加
           
            //  よじ登りモーションの進行度を取得
            float f = animator.GetFloat("ClimbProgress");
            currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime / duration);

            //float f = f1 + Time.deltaTime;

            //  左右は後半にかけて早く移動する
            float x = Mathf.Lerp(climbOldPos.x, climbPos.x, Ease(f));
            float z = Mathf.Lerp(climbOldPos.z, climbPos.z, Ease(f));
            //  上下は等速直線で移動
            float y = Mathf.Lerp(climbOldPos.y, climbPos.y+0.4f, f);

            //  座標を更新
            transform.position = new Vector3(x, y, z);
            _verticalVelocity = 0.0f;
            //  進行度が8割を超えたらよじ登りの終了
            if (f >= 0.8f)
            {
                
                animator.SetBool("IsGrab", false);
              
            }
            if (f >= 1)
            {
                currentValue = 0;
                isClimb = false;
            }

        }

        //  イージング関数
        float Ease(float x)
        {
            return x * x * x;
        }



        void ApplyAnimatorParameter()
        {
            var speed = _inputMove.magnitude;

            var up = currentValue;

            animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
            animator.SetFloat("ClimbProgress", up, 0.099f, Time.deltaTime);

            animator.SetFloat("FallSpeed", -_characterController.velocity.y); // Use negative velocity.y for falling speed
            animator.SetBool("IsGround", _characterController.isGrounded);
         
        }
        

        void Ray()
        {
            // プレイヤーの正面方向を取得
            //Vector3 playerForward = transform.forward;

            // レイを飛ばす原点はプレイヤーの位置
           // Vector3 rayOrigin = transform.position;

            // レイキャストのヒット情報を格納する変数
            RaycastHit hit;
            RaycastHit hit2;

            Ray wallCheckRay = new Ray(transform.position + Vector3.up * wallCheckOffset, transform.forward);
            Ray upperCheckRay = new Ray(transform.position + Vector3.up * upperWallCheckOffset, transform.forward); // 修正

            //  壁判定を格納
            isForwardWall = Physics.Raycast(wallCheckRay, wallCheckRayDistance);
            isUpperWall = Physics.Raycast(upperCheckRay, wallCheckRayDistance);

            // レイを可視化
            Debug.DrawRay(wallCheckRay.origin, wallCheckRay.direction * wallCheckRayDistance, Color.red);
            Debug.DrawRay(upperCheckRay.origin, upperCheckRay.direction * wallCheckRayDistance, Color.green);

        }
    }
}