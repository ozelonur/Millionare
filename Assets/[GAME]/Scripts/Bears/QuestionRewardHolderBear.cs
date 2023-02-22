using _GAME_.Scripts.Models;
using OrangeBear.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OrangeBear.Bears
{
    public class QuestionRewardHolderBear : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private TMP_Text moneyText;

        [SerializeField] private TMP_Text index;
        [SerializeField] private Image holderImage;

        #endregion
        #region Public Methods

        public void InitQuestionReward(QuestionRewardData questionRewardData, int indexValue)
        {
            moneyText.text = "₺" + questionRewardData.amount;
            index.text = (indexValue + 1) + ".";

            if (questionRewardData.isCheckPoint)
            {
                holderImage.color = Color.yellow;
            }
        }

        #endregion
    }
}