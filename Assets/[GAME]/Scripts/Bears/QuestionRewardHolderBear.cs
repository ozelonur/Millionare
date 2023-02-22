using System;
using _GAME_.Scripts.Models;
using DG.Tweening;
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

        public void AnimateEarning(Action callback = null)
        {
            holderImage.DOColor(Color.green, 0.5f).OnComplete(() => { callback?.Invoke(); }).SetLink(gameObject);
        }

        public void AnimateNext(Action callback = null)
        {
            holderImage.DOColor(Color.blue, 0.5f).OnComplete(() => { callback?.Invoke(); }).SetLink(gameObject);
        }

        #endregion
    }
}