using System;
using _GAME_.Scripts.Abstracts;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
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

        [SerializeField] private Image buttonInsideImage;
        [SerializeField] private Color neutralColor;

        #endregion

        #region Private Variables

        private AnswerData _answerData;

        #endregion

        #region Public Methods

        public void InitButton(AnswerData answerDataValue)
        {
            ResetButton();
            _answerData = answerDataValue;
            answerText.text = _answerData.answer;

            buttonInsideImage.color = neutralColor;
        }

        public bool IsCorrect()
        {
            return _answerData.isCorrect;
        }

        public void AnimateCorrect(Action callback = null)
        {
            buttonInsideImage.DOColor(Color.green, 0.5f).OnComplete(() => { callback?.Invoke(); }).SetLink(gameObject);
        }

        public void AnimateWrong(Action callback = null)
        {
            buttonInsideImage.DOColor(Color.red, 0.5f).OnComplete(() => { callback?.Invoke(); }).SetLink(gameObject);
        }

        protected override void OnClick()
        {
            base.OnClick();
            if (_answerData.isCorrect)
            {
                SoundManager.Instance.PlayCorrectAnswerSound();
                AnimateCorrect(() => { Roar(CustomEvents.CorrectAnswer, true); });
            }
            else
            {
                SoundManager.Instance.PlayWrongAnswerSound();
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
            buttonInsideImage.DOKill(true);
            buttonInsideImage.color = Color.blue;
        }

        #endregion
    }
}