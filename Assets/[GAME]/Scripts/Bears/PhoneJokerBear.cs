using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Models;
using DG.Tweening;
using OrangeBear.EventSystem;
using TMPro;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class PhoneJokerBear : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private RectTransform questionHolder;

        [SerializeField] private Transform phoneJokerPanel;
        [SerializeField] private TMP_Text answerText;

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
                Register(CustomEvents.PhoneJokerUsed, PhoneJokerUsed);
                Register(CustomEvents.InitQuestion, InitQuestion);
            }

            else
            {
                Unregister(CustomEvents.PhoneJokerUsed, PhoneJokerUsed);
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

        private void PhoneJokerUsed(object[] arguments)
        {
            Vector2 sizeDelta = questionHolder.sizeDelta;

            questionHolder.DOSizeDelta(new Vector2(sizeDelta.x, _questionHolderHeight), 1f).SetEase(Ease.Linear)
                .SetLink(gameObject);

            phoneJokerPanel.DOLocalMoveY(phoneJokerPanel.transform.localPosition.y + 150, 1f).SetEase(Ease.Linear)
                .SetLink(gameObject);

            string answer = _answerIndex switch
            {
                0 => "A",
                1 => "B",
                2 => "C",
                3 => "D",
                _ => ""
            };

            answerText.text = answer;
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
                -225, localPosition.z);
            transform1.localPosition = localPosition;
        }

        #endregion
    }
}