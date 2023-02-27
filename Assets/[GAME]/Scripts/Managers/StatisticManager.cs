using _GAME_.Scripts.Entities;
using OrangeBear.Core;
using OrangeBear.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.Managers
{
    public class StatisticManager : Manager<StatisticManager>
    {
        #region Properties

        public StatisticData statisticData { get; private set; }

        #endregion

        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Button statisticButton;

        [SerializeField] private Button exitButton;
        [SerializeField] private GameObject statisticPanel;
        
        [SerializeField] private TMP_Text gameCountText;
        [SerializeField] private TMP_Text winCountText;
        [SerializeField] private TMP_Text jokerUsedText;

        #endregion

        #region MonoBehavior Methods

        private void Awake()
        {
            statisticButton.onClick.AddListener(OnClick);
            exitButton.onClick.AddListener(OnClick);
            statisticPanel.SetActive(false);
        }

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

        #region Private Methods

        private void OnClick()
        {
            gameCountText.text = statisticData.GameCount.ToString();
            winCountText.text = statisticData.WinCount.ToString();
            jokerUsedText.text = statisticData.JokerUsed.ToString();
            statisticPanel.SetActive(!statisticPanel.activeSelf);            
        }

        #endregion
    }
}