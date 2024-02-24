using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using Prime31.TransitionKit;


public class SelectUI : MonoBehaviour
{
    public ButtonAction[] buttonActions; // �e�{�^���ɑΉ�����A�N�V�����̔z��
    private int selectedIndex = 0;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;

    private bool buttonPressed = false;
    private bool selectionPaused = false; // �V�����{�^�����L�������ꂽ��I�����ꎞ��~

    public GameObject back;
    public GameObject backB;

    public GameObject SelectButton;
    public GameObject SelectButton2;
    public GameObject SelectButton3;

    public GameObject EndGame;
    public GameObject EndGameButton;

    public GameObject Titles;

    private bool endGameActivated = false; // �I�u�W�F�N�g���L�������ꂽ���ǂ����������t���O

    void Start()
    {
        selectedIndex = 0;
        UpdateButtonColors();
    }

    void Update()
    {
        if (Gamepad.current != null && !selectionPaused)
        {
            float verticalInput = Gamepad.current.dpad.y.ReadValue();

            if (verticalInput < 0 && !buttonPressed)
            {
                MoveSelection(1);
                buttonPressed = true;
            }
            else if (verticalInput > 0 && !buttonPressed)
            {
                MoveSelection(-1);
                buttonPressed = true;
            }
            else if (verticalInput == 0)
            {
                buttonPressed = false;
            }
        }

        if (Gamepad.current != null && Gamepad.current.buttonSouth.isPressed)
        {
            ExecuteSelectedButtonAction();
        }

        // PAD�́��{�^���������ꂽ��I�u�W�F�N�g�𖳌��ɂ��A�I�����ĊJ
        if (Gamepad.current != null && Gamepad.current.buttonEast.isPressed)
        {
            if (selectionPaused)
            {
                selectionPaused = false;
                back.SetActive(false);
                backB.SetActive(false);
                SelectButton.SetActive(true);
                SelectButton2.SetActive(true);
                SelectButton3.SetActive(true);
                EndGame.SetActive(false);
                EndGameButton.SetActive(false);
                Titles.SetActive(true);
                endGameActivated = false; // �I�u�W�F�N�g�������ɂȂ�����t���O�����Z�b�g
            }
        }

        // PAD�́~�{�^���������ꂽ��Q�[�����I��
        if (Gamepad.current != null && EndGame.activeSelf && endGameActivated)
        {
            
                StartCoroutine(ExitGameAfterDelay(0.5f)); // 2�b���ExitGame���\�b�h���Ăяo��
            
        }
        IEnumerator ExitGameAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                ExitGame();
            }
        }
    }

    void MoveSelection(int direction)
    {
        selectedIndex += direction;

        // �{�^������ԏ���z�������ԉ��ɁA��ԉ����z�������ԏ��
        if (selectedIndex < 0)
        {
            selectedIndex = buttonActions.Length - 1;
        }
        else if (selectedIndex >= buttonActions.Length)
        {
            selectedIndex = 0;
        }

        UpdateButtonColors();
    }

    void ExecuteSelectedButtonAction()
    {
        if (selectedIndex >= 0 && selectedIndex < buttonActions.Length)
        {
            ButtonAction selectedAction = buttonActions[selectedIndex];
            selectedAction.ExecuteAction();

            // �I�u�W�F�N�g���L���ɂȂ�����V�����{�^�����L�������A�I����V�����{�^���Ɉڍs
            if (selectedAction.objectToEnable != null)
            {
                selectedAction.objectToEnable.SetActive(true);

                // �V�����{�^�����ݒ肳��Ă���ꍇ�A���̃{�^����L�������A�I�������̃{�^���Ɉڍs
                if (selectedAction.newButtonToEnable != null)
                {
                    selectedAction.newButtonToEnable.SetActive(true);
                    selectedIndex = System.Array.IndexOf(buttonActions, selectedAction.newButtonToEnable);

                    // �V�����{�^�����L�������ꂽ��I�����ꎞ��~
                    selectionPaused = true;
                    SelectButton.SetActive(false);
                    SelectButton2.SetActive(false);
                    SelectButton3.SetActive(false);
                }

                // �I�u�W�F�N�g���L�������ꂽ��t���O��ݒ�
                endGameActivated = true;
            }
        }
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void UpdateButtonColors()
    {
        for (int i = 0; i < buttonActions.Length; i++)
        {
            ButtonAction buttonAction = buttonActions[i];
            if (buttonAction != null)
            {
                // �{�^����image�ł͂Ȃ��A�{�^��������TextMeshPro�̐F��ύX����
                Color textColor = (i == selectedIndex) ? selectedColor : normalColor;
                TextMeshProUGUI buttonText = buttonAction.button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.color = textColor;
                }
            }
        }
    }

    [System.Serializable]
    public class ButtonAction
    {
        public Button button; // �Ή�����{�^��
        public string actionName; // �{�^���̃A�N�V�������i��: "Jump", "Attack"�j
        public string sceneToLoad; // �{�^���������ꂽ�Ƃ��Ɉړ�����V�[����
        public GameObject objectToEnable; // �{�^���������ꂽ�Ƃ��ɗL���ɂ���I�u�W�F�N�g
        public GameObject newButtonToEnable; // �I�u�W�F�N�g���L���ɂȂ�����L���ɂ���V�����{�^��

        public void ExecuteAction()
        {
            // �{�^�����Ƃ̏��������s
            Debug.Log("Executing action for button: " + actionName);

            // �V�[���̈ړ�
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                Debug.Log("Player1 entered the trigger zone.");

                // �t�F�[�h�g�����W�V�����̐ݒ�
                var fader = new FadeTransition()
                {
                    fadedDelay = 2.0f,
                    fadeToColor = Color.black

                };
                TransitionKit.instance.transitionWithDelegate(fader);

                Debug.Log("Transition initiated.");
                SceneManager.LoadScene(sceneToLoad);
            }

            // �����Ƀ{�^�����Ƃ̏�����ǉ�
        }
    }
}
