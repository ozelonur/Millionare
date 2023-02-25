using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using DG.Tweening;
using OrangeBear.EventSystem;
using OrangeBear.Utilities;
using TMPro;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class MoneyUIBear : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private TMP_Text moneyText;

        [SerializeField] private TMP_Text levelBasedMoneyText;

        [SerializeField] private GameObject moneyHolder;

        #endregion

        #region Private Variables

        private int moneyAmount;

        #endregion

        #region MonoBehavior Methods

        private void Start()
        {
            StartCoroutine(CustomCoroutine.WaitOneFrame(() =>
            {
                moneyText.text = "₺" + MoneyManager.Instance.moneyData.Money;
                moneyHolder.SetActive(true);
                moneyAmount = MoneyManager.Instance.moneyData.Money;
            }));
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.UpdateMoneyAmount, UpdateMoneyAmount);
                Register(GameEvents.OnGameComplete, OnGameCompleted);
                Register(GameEvents.OnGameStart, OnGameStart);
            }

            else
            {
                Unregister(CustomEvents.UpdateMoneyAmount, UpdateMoneyAmount);
                Unregister(GameEvents.OnGameComplete, OnGameCompleted);
                Unregister(GameEvents.OnGameStart, OnGameStart);
            }
        }

        private void OnGameStart(object[] arguments)
        {
            moneyHolder.SetActive(false);
        }

        private void OnGameCompleted(object[] arguments)
        {
            moneyHolder.SetActive(true);
        }

        private void UpdateMoneyAmount(object[] arguments)
        {
            int money = (int)arguments[0];

            int difference = money - moneyAmount;
            int levelBasedMoney = 0;

            DOTween.To(() => moneyAmount, x => moneyAmount = x, money, 2f).OnUpdate(() =>
                {
                    moneyText.text = "₺" + moneyAmount;
                })
                .OnComplete(() => { moneyAmount = money; })
                .SetDelay(.3f).SetLink(gameObject);

            DOTween.To(() => levelBasedMoney, x => levelBasedMoney = x, difference, 2f).OnUpdate(() =>
                {
                    levelBasedMoneyText.text = "₺" + levelBasedMoney;
                })
                .OnComplete(() => { levelBasedMoneyText.text = ""; })
                .SetDelay(.3f).SetLink(gameObject);
        }

        #endregion
    }
}