using System;
using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.Managers;
using DG.Tweening;
using OrangeBear.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _GAME_.Scripts.Models;

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

        #region Private Variables

        private int moneyAmount;

        #endregion

        #region Public Methods

        public void InitQuestionReward(QuestionRewardData questionRewardData, int indexValue)
        {
            moneyAmount = questionRewardData.amount;
            moneyText.text = "₺" + questionRewardData.amount;
            index.text = (indexValue + 1) + ".";

            if (questionRewardData.isCheckPoint)
            {
                holderImage.color = Color.yellow;
            }
        }


        public void AnimateEarning(Action callback = null)
        {
            moneyAmount = moneyText.text.GetNumberInString();
            holderImage.DOColor(Color.green, 0.5f).OnComplete(() =>
            {
                MoneyManager.Instance.AddMoney(moneyAmount);
                callback?.Invoke();
            }).SetLink(gameObject);
        }

        public void AnimateNext(Action callback = null)
        {
            holderImage.DOColor(Color.blue, 0.5f).OnComplete(() => { callback?.Invoke(); }).SetLink(gameObject);
        }

        #endregion
    }
}