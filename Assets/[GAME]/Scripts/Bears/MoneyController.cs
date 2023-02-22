using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Models;
using _GAME_.Scripts.ScriptableObjects;
using OrangeBear.EventSystem;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class MoneyController : Bear
    {
        #region Private Variables

        private QuestionRewardDataScriptableObject _questionRewardDataScriptableObject;
        private QuestionRewardHolderBear _holderPrefab;

        private Transform _parent;

        #endregion

        #region Private Methods

        [Button("Init Rewards")]
        private void InitRewards()
        {
            _questionRewardDataScriptableObject =
                Resources.Load<QuestionRewardDataScriptableObject>(GlobalStrings.QuestionRewardData);
            _holderPrefab = Resources.Load<QuestionRewardHolderBear>(GlobalStrings.QuestionRewardHolderPrefab);
            
            QuestionRewardData[] questionRewardDataList = _questionRewardDataScriptableObject.questionRewardDataList;

            _parent = GameObject.Find("MoneyArea").transform;

            for (int i = 0; i < questionRewardDataList.Length; i++)
            {
                QuestionRewardHolderBear questionRewardHolderBear =
                    (QuestionRewardHolderBear)PrefabUtility.InstantiatePrefab(_holderPrefab);

                if (questionRewardHolderBear == null)
                {
                    print("Null");
                }

                questionRewardHolderBear.transform.SetParent(_parent);
                
                questionRewardHolderBear.InitQuestionReward(questionRewardDataList[i], i);
            }
        }

        #endregion
    }
}