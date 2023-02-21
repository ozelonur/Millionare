using _GAME_.Scripts.Abstracts;
using _GAME_.Scripts.Models;
using DG.Tweening;
using TMPro;
using UnityEngine;

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
            answerData = answerDataValue;
            answerText.text = answerData.answer;
        }

        protected override void OnClick()
        {
            base.OnClick();
            if (answerData.isCorrect)
            {
                buttonImage.DOColor(Color.green, 0.5f).SetLink(gameObject);
            }
            else
            {
                buttonImage.DOColor(Color.red, 0.5f).SetLink(gameObject);
            }
        }

        #endregion
    }
}