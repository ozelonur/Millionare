using _GAME_.Scripts.Entities;
using OrangeBear.Core;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class MoneyManager : Manager<MoneyManager>
    {
        #region Properties

        public MoneyData moneyData { get; private set; }

        #endregion

        #region MonoBehavior Methods

        private void Start()
        {
            moneyData = MoneyData.Get();

            if (moneyData == null)
            {
                moneyData = new MoneyData();

                bool isSuccessful = moneyData.Register();

                if (!isSuccessful)
                {
                    Debug.LogError("MoneyData could not be registered!");
                }
            }
            
            moneyData.Load();
        }

        #endregion

        #region Public Methods

        public void AddMoney(int amount)
        {
            moneyData.AddMoney(amount);
        }
        
        public void SubtractMoney(int amount)
        {
            moneyData.SubtractMoney(amount);
        }

        #endregion
    }
}