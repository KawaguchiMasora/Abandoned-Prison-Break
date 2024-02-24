using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using Prime31.TransitionKit;


public class SelectUI : MonoBehaviour
{
    public ButtonAction[] buttonActions; // 各ボタンに対応するアクションの配列
    private int selectedIndex = 0;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;

    private bool buttonPressed = false;
    private bool selectionPaused = false; // 新しいボタンが有効化されたら選択を一時停止

    public GameObject back;
    public GameObject backB;

    public GameObject SelectButton;
    public GameObject SelectButton2;
    public GameObject SelectButton3;

    public GameObject EndGame;
    public GameObject EndGameButton;

    public GameObject Titles;

    private bool endGameActivated = false; // オブジェクトが有効化されたかどうかを示すフラグ

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

        // PADの●ボタンが押されたらオブジェクトを無効にし、選択を再開
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
                endGameActivated = false; // オブジェクトが無効になったらフラグをリセット
            }
        }

        // PADの×ボタンが押されたらゲームを終了
        if (Gamepad.current != null && EndGame.activeSelf && endGameActivated)
        {
            
                StartCoroutine(ExitGameAfterDelay(0.5f)); // 2秒後にExitGameメソッドを呼び出す
            
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

        // ボタンが一番上を越えたら一番下に、一番下を越えたら一番上に
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

            // オブジェクトが有効になったら新しいボタンも有効化し、選択を新しいボタンに移行
            if (selectedAction.objectToEnable != null)
            {
                selectedAction.objectToEnable.SetActive(true);

                // 新しいボタンが設定されている場合、そのボタンを有効化し、選択をそのボタンに移行
                if (selectedAction.newButtonToEnable != null)
                {
                    selectedAction.newButtonToEnable.SetActive(true);
                    selectedIndex = System.Array.IndexOf(buttonActions, selectedAction.newButtonToEnable);

                    // 新しいボタンが有効化されたら選択を一時停止
                    selectionPaused = true;
                    SelectButton.SetActive(false);
                    SelectButton2.SetActive(false);
                    SelectButton3.SetActive(false);
                }

                // オブジェクトが有効化されたらフラグを設定
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
                // ボタンのimageではなく、ボタンが持つTextMeshProの色を変更する
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
        public Button button; // 対応するボタン
        public string actionName; // ボタンのアクション名（例: "Jump", "Attack"）
        public string sceneToLoad; // ボタンが押されたときに移動するシーン名
        public GameObject objectToEnable; // ボタンが押されたときに有効にするオブジェクト
        public GameObject newButtonToEnable; // オブジェクトが有効になったら有効にする新しいボタン

        public void ExecuteAction()
        {
            // ボタンごとの処理を実行
            Debug.Log("Executing action for button: " + actionName);

            // シーンの移動
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                Debug.Log("Player1 entered the trigger zone.");

                // フェードトランジションの設定
                var fader = new FadeTransition()
                {
                    fadedDelay = 2.0f,
                    fadeToColor = Color.black

                };
                TransitionKit.instance.transitionWithDelegate(fader);

                Debug.Log("Transition initiated.");
                SceneManager.LoadScene(sceneToLoad);
            }

            // ここにボタンごとの処理を追加
        }
    }
}
