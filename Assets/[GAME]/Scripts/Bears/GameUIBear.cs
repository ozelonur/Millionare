using System.Collections;
using System.Linq;
using _GAME_.Scripts.CustomInputs;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Models;
using DG.Tweening;
using OrangeBear.Core;
using OrangeBear.EventSystem;
using TMPro;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class GameUIBear : UIBear
    {
        #region Seralized Fields

        [Header("In Game Panels")] [SerializeField]
        private CustomPanelData[] inGamePanels;

        [Header("Answer Buttons")] [SerializeField]
        private AnswerButton[] answerButtons;

        [Header("Question Text")] [SerializeField]
        private TMP_Text questionText;

        [Header("Tap for Next Button")] [SerializeField]
        private TapForNextButton tapForNextButton;

        [Header("Question Number Text")] [SerializeField]
        private TMP_Text questionNumberText;

        #endregion

        #region Private Variables

        private Coroutine _rewardPanelTimer;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            base.CheckRoarings(status);
            if (status)
            {
                Register(CustomEvents.InitQuestion, InitQuestion);
                Register(GameEvents.OnGameStart, OnGameStart);
                Register(CustomEvents.CorrectAnswer, CorrectAnswer);
            }

            else
            {
                Unregister(CustomEvents.InitQuestion, InitQuestion);
                Unregister(GameEvents.OnGameStart, OnGameStart);
                Unregister(CustomEvents.CorrectAnswer, CorrectAnswer);
            }
        }

        private void CorrectAnswer(object[] arguments)
        {
            bool status = (bool)arguments[0];
            if (status)
            {
                ActivatePanel(InGamePanels.Reward);
                tapForNextButton.transform.DOScale(Vector3.one * 1.25f, .5f).SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Yoyo).SetLink(tapForNextButton.gameObject);

                if (_rewardPanelTimer != null)
                {
                    StopCoroutine(_rewardPanelTimer);
                }

                _rewardPanelTimer = StartCoroutine(StartRewardPanelTimer());
            }

            else
            {
                foreach (AnswerButton answer in answerButtons)
                {
                    if (answer.IsCorrect())
                    {
                        answer.AnimateCorrect(() => { Roar(GameEvents.OnGameComplete, false); });
                    }
                }
            }
        }

        private void OnGameStart(object[] arguments)
        {
            ActivatePanel(InGamePanels.Question);
        }

        private void InitQuestion(object[] arguments)
        {
            if (_rewardPanelTimer != null)
            {
                StopCoroutine(_rewardPanelTimer);
            }
            
            tapForNextButton.transform.localScale = Vector3.one;
            
            QuestionData questionData = (QuestionData)arguments[0];

            questionText.text = questionData.question;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].InitButton(questionData.answers[i]);
            }

            int questionNumber = (int)arguments[1];
            questionNumber += 1;
            questionNumberText.text = questionNumber + "/12";
            ActivatePanel(InGamePanels.Question);
        }

        #endregion

        #region Private Methods

        private void ActivatePanel(InGamePanels panelType)
        {
            inGamePanels.Where(panel => panel.panelType != panelType).ToList()
                .ForEach(panel => panel.panel.SetActive(false));
            inGamePanels.FirstOrDefault(panel => panel.panelType == panelType)?.panel.SetActive(true);
        }

        private IEnumerator StartRewardPanelTimer()
        {
            float time = 3f;
            CustomPanelData panelData = inGamePanels.FirstOrDefault(panel => panel.panelType == InGamePanels.Reward);

            yield return new WaitForSeconds(time);

            if (panelData == null || !panelData.panel.activeInHierarchy) yield break;

            tapForNextButton.transform.DOKill(true);
            Roar(CustomEvents.NextQuestion);
        }

        #endregion
    }
}