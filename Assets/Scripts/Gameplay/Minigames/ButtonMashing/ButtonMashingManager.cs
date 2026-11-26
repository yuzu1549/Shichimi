using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace GamePlay.Minigames.ButtonMashing
{
    public class ButtonMashingManager : MonoBehaviour
    {
        [SerializeField] private ButtonMashingUI buttonMashingUI;
        [SerializeField] private int countdownTime = 3;
        [SerializeField] private float gameTime = 10f;
        private int pressCount = 0;

        public int CountdownTime => countdownTime;
        public int PressCount => pressCount;
        public float GameTime => gameTime;

        /// <summary>
        /// ゲーム開始ボタンが押されたときの処理
        /// </summary>
        public void OnStartButtonClicked()
        {
            StartGame().Forget();
        }

        /// <summary>
        /// ゲーム開始後の処理
        /// </summary>
        /// <returns></returns>
        async public UniTask StartGame()
        {
            pressCount = 0;
            await buttonMashingUI.StartGame();

            float remainingTime = gameTime;
            buttonMashingUI.UpdateInGameCountdown(remainingTime);

            while (remainingTime > 0f)
            {
                await UniTask.NextFrame();

                remainingTime = Mathf.Max(0f, remainingTime - Time.deltaTime);
                buttonMashingUI.UpdateInGameCountdown(remainingTime);
            }

            EndGame();
        }

        /// <summary>
        /// ゲーム終了後の処理
        /// </summary>
        /// <returns></returns>
        public void EndGame()
        {
            buttonMashingUI.EndGame().Forget();
        }

        /// <summary>
        /// ボタンが押されたときの処理
        /// </summary>
        public void OnButtonPress()
        {
            pressCount++;
            buttonMashingUI.UpdateCount();
        }
    }
}
