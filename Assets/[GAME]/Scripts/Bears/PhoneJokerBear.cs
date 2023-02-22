using _GAME_.Scripts.GlobalVariables;
using DG.Tweening;
using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Bears
{
    public class PhoneJokerBear : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private RectTransform questionHolder;

        [SerializeField] private Transform phoneJokerPanel;

        #endregion

        #region Private Variables

        private float _questionHolderHeight = 400f;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.PhoneJokerUsed, PhoneJokerUsed);
            }

            else
            {
                Unregister(CustomEvents.PhoneJokerUsed, PhoneJokerUsed);
            }
        }

        private void PhoneJokerUsed(object[] arguments)
        {
            Vector2 sizeDelta = questionHolder.sizeDelta;

            questionHolder.DOSizeDelta(new Vector2(sizeDelta.x, _questionHolderHeight), 1f).SetEase(Ease.Linear)
                .SetLink(gameObject);

            phoneJokerPanel.DOLocalMoveY(phoneJokerPanel.transform.localPosition.y + 150, 1f).SetEase(Ease.Linear)
                .SetLink(gameObject);
        }

        #endregion
    }
}