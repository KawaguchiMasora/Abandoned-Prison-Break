using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

[RequireComponent(typeof(CharacterController))]

public class PADtest : MonoBehaviour
{
    [Header("�ړ��̑���"), SerializeField]
    private float _speed = 3;

    [Header("���Ⴊ��ł��鎞�̑���"), SerializeField]
    private float _SitSpeed = 6;

    [Header("�W�����v����u�Ԃ̑���"), SerializeField]
    private float _jumpSpeed = 7;

    [Header("�d�͉����x"), SerializeField]
    private float _gravity = 15;

    [Header("�������̑��������iInfinity�Ŗ������j"), SerializeField]
    private float _fallSpeed = 10;

    [Header("�����̏���"), SerializeField]
    private float _initFallSpeed = 2;

    [Header("�A�j���[�^�["), SerializeField]
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

    [Header("�����̍���"), SerializeField]
    private float initialHeight = 1.78f;

    [Header("���Ⴊ�ݎ��̍���"), SerializeField]
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


        // �V�[���J��
        SceneManager.LoadScene(targetSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(targetTag))
        {
            animator.SetBool("Des", _isDes);

            // �R���[�`�����J�n
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
            Debug.Log("���݂܂�");
            animator.SetBool("IsGrab", _characterController.isGrounded);
        }
    }

    public void OnDash(InputAction.CallbackContext context)//�_�b�V���ł͂Ȃ����Ⴊ��
    {
        if (context.performed)
        {
            // �l�p�{�^���������ꂽ�Ƃ�
            _isDashing = !_isDashing; // true�Ȃ�false�ɁAfalse�Ȃ�true�ɐ؂�ւ���
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

    
        // �ړ����x�̐ݒ�
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

        if (isGrab && _characterController.isGrounded)  // isGrounded ���`�F�b�N
        {
            animator.SetBool("IsGrab", false);  // �O���u���L���ł��n�ʂɂ���ꍇ�� IsGrab �� false �ɐݒ�
            isGrab = false;  // �O���u������
            Debug.Log("waa");
        }


        //  �R�ɕ߂܂��Ă���Ƃ��́A�d�͂�0�ɂ���
        //isGrab��treu�ň�ڂ̃��C���ǂɓ������Ă��Ȃ������珈��������
        if (isGrab && !isForwardWall)
        {
            _verticalVelocity = 0.0f;


            
            //  �O���͂��ꂽ��悶�o��
            if (_inputMove.magnitude > 0.1f)
             {
                Debug.Log("test");
                //  �J�n�ʒu��ێ�
                climbOldPos = transform.position;
                Debug.Log(climbOldPos);
                //  �I���ʒu���Z�o
                climbPos = transform.position + transform.forward + Vector3.up;
                Debug.Log(climbPos);
                //  �݂͂�����
                isGrab = false;
                //  �悶�o������s
                isClimb = true;
            }
        }
        if (isClimb)
        {
            //���͂𖳎������ǉ�
           
            //  �悶�o�胂�[�V�����̐i�s�x���擾
            float f = animator.GetFloat("ClimbProgress");
            currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime / duration);

            //float f = f1 + Time.deltaTime;

            //  ���E�͌㔼�ɂ����đ����ړ�����
            float x = Mathf.Lerp(climbOldPos.x, climbPos.x, Ease(f));
            float z = Mathf.Lerp(climbOldPos.z, climbPos.z, Ease(f));
            //  �㉺�͓��������ňړ�
            float y = Mathf.Lerp(climbOldPos.y, climbPos.y+0.4f, f);

            //  ���W���X�V
            transform.position = new Vector3(x, y, z);
            _verticalVelocity = 0.0f;
            //  �i�s�x��8���𒴂�����悶�o��̏I��
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

        //  �C�[�W���O�֐�
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
            // �v���C���[�̐��ʕ������擾
            //Vector3 playerForward = transform.forward;

            // ���C���΂����_�̓v���C���[�̈ʒu
           // Vector3 rayOrigin = transform.position;

            // ���C�L���X�g�̃q�b�g�����i�[����ϐ�
            RaycastHit hit;
            RaycastHit hit2;

            Ray wallCheckRay = new Ray(transform.position + Vector3.up * wallCheckOffset, transform.forward);
            Ray upperCheckRay = new Ray(transform.position + Vector3.up * upperWallCheckOffset, transform.forward); // �C��

            //  �ǔ�����i�[
            isForwardWall = Physics.Raycast(wallCheckRay, wallCheckRayDistance);
            isUpperWall = Physics.Raycast(upperCheckRay, wallCheckRayDistance);

            // ���C������
            Debug.DrawRay(wallCheckRay.origin, wallCheckRay.direction * wallCheckRayDistance, Color.red);
            Debug.DrawRay(upperCheckRay.origin, upperCheckRay.direction * wallCheckRayDistance, Color.green);

        }
    }
}