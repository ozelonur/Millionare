using System;
using _GAME_.Scripts.Abstracts;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Models;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.CustomInputs
{
    public class AnswerButton : ButtonBehavior
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private TMP_Text answerText;

        #endregion

        #region Private Variables

        private AnswerData answerData;

        #endregion

        #region Public Methods

        public void InitButton(AnswerData answerDataValue)
        {
            ResetButton();
            answerData = answerDataValue;
            answerText.text = answerData.answer;
        }

        public bool IsCorrect()
        {
            return answerData.isCorrect;
        }

        public void AnimateCorrect(Action callback = null)
        {
            buttonImage.DOColor(Color.green, 0.5f).OnComplete(() => { callback?.Invoke(); }).SetLink(gameObject);
        }

        public void AnimateWrong(Action callback = null)
        {
            buttonImage.DOColor(Color.red, 0.5f).OnComplete(() => { callback?.Invoke(); }).SetLink(gameObject);
        }

        protected override void OnClick()
        {
            base.OnClick();
            if (answerData.isCorrect)
            {
                AnimateCorrect(() => { Roar(CustomEvents.CorrectAnswer, true); });
            }
            else
            {
                AnimateWrong(() => { Roar(CustomEvents.CorrectAnswer, false); });
            }
        }

        #endregion

        #region Private Methods

        private void ResetButton()
        {
            if (buttonImage == null)
            {
                buttonImage = GetComponent<Image>();
            }
            transform.DOKill();
            buttonImage.color = Color.white;
        }

        #endregion
    }
}