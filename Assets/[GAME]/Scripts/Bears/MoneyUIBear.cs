using _GAME_.Scripts.Managers;
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

        [SerializeField] private GameObject moneyHolder;

        #endregion

        #region MonoBehavior Methods

        private void Start()
        {
            StartCoroutine(CustomCoroutine.WaitOneFrame(() =>
            {
                moneyText.text = "₺" + MoneyManager.Instance.moneyData.Money;
                moneyHolder.SetActive(true);
            }));
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
            }

            else
            {
            }
        }

        #endregion
    }
}