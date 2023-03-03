using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.ScriptableObjects;
using OrangeBear.EventSystem;
using UnityEngine;
#if UNITY_EDITOR
using _GAME_.Scripts.Models;
using Sirenix.OdinInspector;
using UnityEditor;
#endif

namespace OrangeBear.Bears
{
    public class MoneyController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Transform parent;

        #endregion

        #region Private Variables

        private QuestionRewardDataScriptableObject _questionRewardDataScriptableObject;
        private QuestionRewardHolderBear _holderPrefab;


        private QuestionRewardHolderBear[] _holders;
        private int _index;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _holders = parent.GetComponentsInChildren<QuestionRewardHolderBear>();
        }

        private void Start()
        {
            Roar(CustomEvents.GetQuestionRewardDatas, _questionRewardDataScriptableObject);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.CorrectAnswer, CorrectAnswer);
                Register(GameEvents.InitLevel, InitLevel);
            }

            else
            {
                Unregister(CustomEvents.CorrectAnswer, CorrectAnswer);
                Unregister(GameEvents.InitLevel, InitLevel);
            }
        }

        private void InitLevel(object[] arguments)
        {
            _index = 0;
            
            _questionRewardDataScriptableObject =
                Resources.Load<QuestionRewardDataScriptableObject>(GlobalStrings.QuestionRewardData);
            
            QuestionRewardData[] questionRewardDataList = _questionRewardDataScriptableObject.questionRewardDataList;
            
            for (int i = 0; i < questionRewardDataList.Length; i++)
            {
            }
        }

        private void CorrectAnswer(object[] arguments)
        {
            bool status = (bool)arguments[0];
            if (!status)
            {
                return;
            }

            if (_index < _holders.Length)
            {
                _holders[_index].AnimateEarning();
            }

            if (_index < _holders.Length - 1)
            {
                _holders[_index + 1].AnimateNext();
            }

            _index++;
        }

        #endregion

        #region Private Methods

#if UNITY_EDITOR
        [Button("Init Rewards")]
        private void InitRewards()
        {
            _questionRewardDataScriptableObject =
                Resources.Load<QuestionRewardDataScriptableObject>(GlobalStrings.QuestionRewardData);
            _holderPrefab = Resources.Load<QuestionRewardHolderBear>(GlobalStrings.QuestionRewardHolderPrefab);

            QuestionRewardData[] questionRewardDataList = _questionRewardDataScriptableObject.questionRewardDataList;

            parent = GameObject.Find("MoneyArea").transform;

            for (int i = 0; i < questionRewardDataList.Length; i++)
            {
                QuestionRewardHolderBear questionRewardHolderBear =
                    (QuestionRewardHolderBear)PrefabUtility.InstantiatePrefab(_holderPrefab);

                questionRewardHolderBear.transform.SetParent(parent);

                questionRewardHolderBear.InitQuestionReward(questionRewardDataList[i], i);
            }
        }
#endif

        #endregion
    }
}