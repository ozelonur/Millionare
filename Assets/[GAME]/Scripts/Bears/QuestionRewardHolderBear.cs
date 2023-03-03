using System;
using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.Managers;
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

        #region Private Variables

        private int moneyAmount;

        #endregion

        #region Public Methods

        public void InitQuestionReward(QuestionRewardData questionRewardData, int indexValue)
        {
            moneyAmount = questionRewardData.amount;
            moneyText.text = "₺" + questionRewardData.amount.MoneyWithComma();
            index.text = (indexValue + 1) + ".";

            if (questionRewardData.isCheckPoint)
            {
                holderImage.color = new Color(1, 90 / 255f, 0, 1);
            }

            else
            {
                holderImage.color = new Color(18 / 255f, 88 / 255f, 182 / 255f, 1);
            }
        }


        public void AnimateEarning(Action callback = null)
        {
            string moneyTextString = moneyText.text;
            string cleanString = moneyTextString.Replace(",", "");

            moneyAmount = cleanString.GetNumberInString();
            holderImage.DOColor(Color.green, 0.5f).OnComplete(() =>
            {
                MoneyManager.Instance.AddMoney(moneyAmount);
                callback?.Invoke();
            }).SetLink(gameObject);
        }

        public void AnimateNext(Action callback = null)
        {
            holderImage.DOColor(Color.cyan, 0.5f).OnComplete(() => { callback?.Invoke(); }).SetLink(gameObject);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameStart, OnGameStart);
                Register(GameEvents.OnGameComplete, OnGameComplete);
            }

            else
            {
                Unregister(GameEvents.OnGameStart, OnGameStart);
                Unregister(GameEvents.OnGameComplete, OnGameComplete);
            }
        }

        private void OnGameComplete(object[] arguments)
        {
            holderImage.color = new Color(18 / 255f, 88 / 255f, 182 / 255f, 1);
        }

        private void OnGameStart(object[] arguments)
        {
            holderImage.color = new Color(18 / 255f, 88 / 255f, 182 / 255f, 1);
        }

        #endregion
    }
}