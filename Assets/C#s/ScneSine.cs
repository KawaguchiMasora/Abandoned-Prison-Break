using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScneSine : MonoBehaviour
{
    public float inputLifetime = 10.0f; // ���͏��̎����i�b�j

    private float sceneLoadTime;

    void Start()
    {
        // �V�[�������[�h���ꂽ�������L�^
        sceneLoadTime = Time.time;
    }

    void Update()
    {
        // �V�[�������[�h����Ă����莞�Ԃ��o�߂�����
        if (Time.time - sceneLoadTime >= inputLifetime)
        {
            // �����ɓ��͏��̔j��������ǉ�
            // ��: Input.ResetInputAxes();
        }

        // �ʏ�̓��͏����������ɒǉ�
    }
}
