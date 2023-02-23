using _GAME_.Scripts.GlobalVariables;
using DG.Tweening;
using OrangeBear.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OrangeBear.Bears
{
    public class GraphicBear : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Image fillImage;

        [SerializeField] private TMP_Text percentageText;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.InitQuestion, InitQuestion);
            }

            else
            {
                Unregister(CustomEvents.InitQuestion, InitQuestion);
            }
        }

        private void InitQuestion(object[] arguments)
        {
            fillImage.fillAmount = 0;
            percentageText.text = "0%";
        }

        #endregion

        #region Public Methods

        public void UpdateGraphic(float percentage)
        {
            percentageText.text = "0%";
            fillImage.fillAmount = 0;
            fillImage.DOFillAmount(percentage, 1f).SetDelay(.3f).SetEase(Ease.OutCubic).SetLink(gameObject);

            float percentageValue = 0;
            DOTween.To(() => percentageValue, x => percentageValue = x, percentage, 1f).SetDelay(.3f)
                .SetEase(Ease.OutCubic).SetLink(gameObject).OnUpdate(() =>
                {
                    percentageText.text = (percentageValue * 100).ToString("0") + "%";
                }).SetLink(gameObject);
        }

        #endregion
    }
}