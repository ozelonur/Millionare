using System.Collections.Generic;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Models;
using _GAME_.Scripts.ScriptableObjects;
using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class QuestionControllerBear : Bear
    {
        #region Serialized Fields

        [Header("Question Pool")] [SerializeField]
        private List<QuestionDataScriptableObject> questions;

        #endregion

        #region Private Variables

        private int _questionIndex;
        private QuestionData _currentQuestion;

        #endregion

        #region MonoBehavior Methods

        private void Awake()
        {
            _currentQuestion = GetQuestion();
        }

        private void Start()
        {
            Roar(CustomEvents.InitQuestion, _currentQuestion);
        }

        #endregion

        #region Private Methods

        private QuestionData GetQuestion()
        {
            QuestionData[] questionDatas = questions[_questionIndex].questions;
            
            return questionDatas[Random.Range(0, questionDatas.Length)];
        }

        #endregion
    }
}