using OrangeBear.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.Abstracts
{
    [RequireComponent(typeof(Button))]
    public class ButtonBehavior : Bear
    {
        #region Protected Variables

        protected Button button;
        protected Image buttonImage;

        #endregion

        #region MonoBehavior Methods

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            buttonImage = GetComponent<Image>();
            button.onClick.AddListener(OnClick);
        }

        #endregion

        #region Protected Methods

        protected virtual void OnClick()
        {
        }

        #endregion
    }
}