using _GAME_.Scripts.Models;
using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class MoneyController : Bear
    {
        #region Serialized Fields

        [Header("Question Reward Data List")] [SerializeField]
        private QuestionRewardData[] questionRewardDataList;

        #endregion
    }
}