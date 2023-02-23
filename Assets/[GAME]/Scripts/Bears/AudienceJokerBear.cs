using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Models;
using DG.Tweening;
using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class AudienceJokerBear : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private GraphicBear[] graphics;

        [SerializeField] private RectTransform questionHolder;

        [SerializeField] private Transform phoneJokerPanel;

        #endregion

        #region Private Variables

        private float _questionHolderHeight = 400f;
        private int _answerIndex;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.AudienceJokerUsed, AudienceJokerUsed);
                Register(CustomEvents.InitQuestion, InitQuestion);
            }

            else
            {
                Unregister(CustomEvents.AudienceJokerUsed, AudienceJokerUsed);
                Unregister(CustomEvents.InitQuestion, InitQuestion);
            }
        }

        private void InitQuestion(object[] arguments)
        {
            ClosePhoneJokerPanel();
            QuestionData questionData = (QuestionData)arguments[0];

            List<AnswerData> answers = questionData.answers.ToList();

            int index = answers.IndexOf(answers.FirstOrDefault(answer => answer.isCorrect));

            _answerIndex = index;
        }

        private void AudienceJokerUsed(object[] arguments)
        {
            Vector2 sizeDelta = questionHolder.sizeDelta;

            questionHolder.DOSizeDelta(new Vector2(sizeDelta.x, _questionHolderHeight), 1f).SetEase(Ease.Linear)
                .SetLink(gameObject);

            phoneJokerPanel.DOLocalMoveY(phoneJokerPanel.transform.localPosition.y + 150, 1f)
                .OnComplete(SetGraphics).SetEase(Ease.Linear)
                .SetLink(gameObject);
        }

        #endregion

        #region Private Methods

        private void ClosePhoneJokerPanel()
        {
            Vector2 sizeDelta = questionHolder.sizeDelta;

            questionHolder.sizeDelta = new Vector2(sizeDelta.x, 600);

            Transform transform1 = phoneJokerPanel.transform;
            Vector3 localPosition = transform1.localPosition;
            localPosition = new Vector3(localPosition.x,
                -200, localPosition.z);
            transform1.localPosition = localPosition;
        }

        private void SetGraphics()
        {
            int[] results = new int[4];

            int totalPercentage = 100;

            results[_answerIndex] = Random.Range(50, totalPercentage);

            totalPercentage -= results[_answerIndex];

            for (int i = 0; i < 4; i++)
            {
                if (i == _answerIndex) continue;

                results[i] = Random.Range(0, totalPercentage + 1);
                totalPercentage -= results[i];
            }

            int sum = results.Sum();

            if (sum < 100)
            {
                int difference = 100 - sum;
                if (results.Contains(0))
                {
                    results[results.ToList().IndexOf(0)] += difference;
                }

                else
                {
                    results[_answerIndex] += difference;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                float percentage = results[i] / 100f;
                graphics[i].UpdateGraphic(percentage);
            }
        }

        #endregion
    }
}