using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.CustomInputs;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _GAME_.Scripts.Models;
using _GAME_.Scripts.ScriptableObjects;
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

        [Header("Question Money Text")] [SerializeField]
        private TMP_Text questionMoneyText;
        
        [Header("Configuration Data")][SerializeField]
        private QuestionRewardDataScriptableObject questionRewardDatas;

        #endregion

        #region Private Variables

        private Coroutine _rewardPanelTimer;
        private bool _newGameButtonClicked;


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
                Register(CustomEvents.HalfJokerUsed, HalfJokerUsed);
                Register(CustomEvents.NewGameButtonClicked, NewGameButtonClicked);
            }

            else
            {
                Unregister(CustomEvents.InitQuestion, InitQuestion);
                Unregister(GameEvents.OnGameStart, OnGameStart);
                Unregister(CustomEvents.CorrectAnswer, CorrectAnswer);
                Unregister(CustomEvents.HalfJokerUsed, HalfJokerUsed);
                Unregister(CustomEvents.NewGameButtonClicked, NewGameButtonClicked);
            }
        }

        protected override void InitLevel(object[] arguments)
        {
            base.InitLevel(arguments);
            if (!_newGameButtonClicked) return;

            StartGame();
            _newGameButtonClicked = false;
        }

        private void NewGameButtonClicked(object[] arguments)
        {
            _newGameButtonClicked = true;
        }

        private void HalfJokerUsed(object[] arguments)
        {
            List<AnswerButton> wrongAnswers = answerButtons.Where(x => !x.IsCorrect()).ToList();

            for (int i = 0; i < 2; i++)
            {
                int index = Random.Range(0, wrongAnswers.Count - 1);
                wrongAnswers[index].gameObject.SetActive(false);
                wrongAnswers.RemoveAt(index);
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
                        answer.AnimateCorrect(() =>
                        {
                            Roar(GameEvents.OnGameComplete, false);
                            Roar(CustomEvents.UpdateMoneyAmount, MoneyManager.Instance.moneyData.Money);
                        });
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

            questionMoneyText.text =
                "₺" + questionRewardDatas.questionRewardDataList[questionNumber].amount.MoneyWithComma();
            questionNumber += 1;
            questionNumberText.text = questionNumber + "/12";
            ActivateAllButtons();
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

        private void ActivateAllButtons()
        {
            foreach (AnswerButton answerButton in answerButtons)
            {
                answerButton.gameObject.SetActive(true);
            }
        }

        #endregion
    }
}