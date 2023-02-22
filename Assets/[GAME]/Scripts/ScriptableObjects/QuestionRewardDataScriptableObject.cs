using _GAME_.Scripts.Models;
using UnityEngine;

namespace _GAME_.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Question Reward Data", menuName = "Orange Bear / Question Reward Data", order = 1)]
    public class QuestionRewardDataScriptableObject : ScriptableObject
    {
        public QuestionRewardData[] questionRewardDataList;
    }
}