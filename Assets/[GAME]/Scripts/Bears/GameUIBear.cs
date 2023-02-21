using _GAME_.Scripts.CustomInputs;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Models;
using OrangeBear.Core;
using TMPro;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class GameUIBear : UIBear
    {
        #region Seralized Fields

        [Header("Answer Buttons")][SerializeField]
        private AnswerButton[] answerButtons;
        
        [Header("Question Text")][SerializeField]
        private TMP_Text questionText;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            base.CheckRoarings(status);
            if (status)
            {
                Register(CustomEvents.InitQuestion, InitQuestion);
            }

            else
            {
                Unregister(CustomEvents.InitQuestion, InitQuestion);
            }
        }

        private void InitQuestion(object[] arguments)
        {
            QuestionData questionData = (QuestionData) arguments[0];
            
            questionText.text = questionData.question;
            
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].InitButton(questionData.answers[i]);
            }
        }

        #endregion
    }
}

