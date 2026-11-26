using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;
using System;
using UnityEngine.UI;

namespace GamePlay.Minigames.ButtonMashing
{
    public class ButtonMashingUI : MonoBehaviour
    {
        [Header("ButtonMashingManager")]
        [SerializeField] private ButtonMashingManager buttonMashingManager;

        [Header("UIオブジェクト")]
        [SerializeField] private GameObject buttonMashingUI;
        [SerializeField] private GameObject buttonMashingResultUI;

        [Header("ボタン")]
        [SerializeField] private Button mashingButton;

        [Header("各種テキスト")]
        [SerializeField] private TMP_Text startCountdownText;
        [SerializeField] private TMP_Text inGameCountdownText;
        [SerializeField] private TMP_Text pressCountText;
        [SerializeField] private TMP_Text finishText;
        [SerializeField] private TMP_Text resultText;

        void Awake()
        {
            buttonMashingUI.SetActive(false);
            buttonMashingResultUI.SetActive(false);
            inGameCountdownText.gameObject.SetActive(false);
            mashingButton.interactable = false;
        }

        /// <summary>
        /// ゲーム開始後のUIの流れを制御
        /// </summary>
        /// <returns></returns>
        async public UniTask StartGame()
        {
            int countdownTime = buttonMashingManager.CountdownTime;
            float gameTime = buttonMashingManager.GameTime;
            buttonMashingUI.SetActive(true);

            // 開始前カウントダウンの表示
            startCountdownText.color = Color.red;
            for (int t = countdownTime; t > 0; t -= 1)
            {
                startCountdownText.text = $"{t}";
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
            }

            // ゲームスタートの表示
            startCountdownText.text = "GO!";
            startCountdownText.color = Color.green;
            inGameCountdownText.gameObject.SetActive(true);
            inGameCountdownText.color = Color.red;

            mashingButton.interactable = true; // ボタンを押せるようにする
        }

        /// <summary>
        /// ゲーム中のカウントダウンの表示
        /// </summary>
        public void UpdateInGameCountdown(float time)
        {
            inGameCountdownText.text = $"{time:F1}";
        }

        /// <summary>
        /// ボタンが押されたときの処理
        /// </summary>
        public void UpdateCount()
        {
            pressCountText.SetText($"Press Count: {buttonMashingManager.PressCount}");
        }

        /// <summary>
        /// ゲーム終了後のUIの流れを制御
        /// </summary>
        /// <returns></returns>
        async public UniTask EndGame()
        {
            startCountdownText.color = Color.red;
            startCountdownText.text = "Finish!!";
            mashingButton.interactable = false; // ボタンを押せないようにする

            await UniTask.Delay(TimeSpan.FromSeconds(2f));

            buttonMashingUI.SetActive(false);
            buttonMashingResultUI.SetActive(true);

            resultText.text = $"Press Count: {buttonMashingManager.PressCount}";
            
        }
    }
}
