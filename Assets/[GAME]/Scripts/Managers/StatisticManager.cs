using _GAME_.Scripts.Entities;
using OrangeBear.Core;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class StatisticManager : Manager<StatisticManager>
    {
        #region Properties

        public StatisticData statisticData { get; private set; }

        #endregion

        #region MonoBehavior Methods

        private void Start()
        {
            statisticData = StatisticData.Get();

            if (statisticData == null)
            {
                statisticData = new StatisticData();

                bool isSuccessful = statisticData.Register();

                if (!isSuccessful)
                {
                    Debug.LogError("Statistic data cannot register!");
                }
            }
            
            statisticData.Load();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameComplete, OnGameComplete);
            }

            else
            {
                Unregister(GameEvents.OnGameComplete, OnGameComplete);
            }
        }

        private void OnGameComplete(object[] arguments)
        {
            bool status = (bool)arguments[0];

            if (status)
            {
                IncreaseWinCount();
            }
            IncreaseGameCount();
        }

        #endregion

        #region Public Methods

        public void IncreaseGameCount()
        {
            statisticData.IncreaseGameCount();
        }

        public void IncreaseWinCount()
        {
            statisticData.IncreaseWinCount();
        }

        public void IncreaseJokerCount()
        {
            statisticData.IncreaseJokerUsed();
        }

        #endregion
    }
}