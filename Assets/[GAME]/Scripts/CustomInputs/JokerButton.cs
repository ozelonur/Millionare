using _GAME_.Scripts.Abstracts;
using _GAME_.Scripts.Enums;
using OrangeBear.EventSystem;
using TMPro;
using UnityEngine;

namespace _GAME_.Scripts.CustomInputs
{
    public class JokerButton: ButtonBehavior
    {
        #region Serialized Fields

        [SerializeField] protected JokerType jokerType;
        [SerializeField] protected TMP_Text usedImage;

        #endregion

        #region Protected Variables

        protected bool isUsed;

        #endregion

        #region MonoBehavior Methods

        protected override void Awake()
        {
            base.Awake();
            isUsed = false;
            usedImage.gameObject.SetActive(false);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            base.CheckRoarings(status);
            if (status)
            {
                Register(GameEvents.OnGameStart, OnGameStart);
            }

            else
            {
                Unregister(GameEvents.OnGameStart, OnGameStart);
            }
        }

        private void OnGameStart(object[] arguments)
        {
            isUsed = false;
            button.interactable = true;
            usedImage.gameObject.SetActive(false);
        }

        #endregion

        #region Override Methods

        protected override void OnClick()
        {
            base.OnClick();
            if (isUsed)
            {
                return;
            }
            
            usedImage.gameObject.SetActive(true);
        }

        #endregion
    }
}