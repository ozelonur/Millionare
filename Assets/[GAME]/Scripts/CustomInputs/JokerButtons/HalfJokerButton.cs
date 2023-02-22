using _GAME_.Scripts.GlobalVariables;

namespace _GAME_.Scripts.CustomInputs.JokerButtons
{
    public class HalfJokerButton : JokerButton
    {
        #region Override Methods

        protected override void OnClick()
        {
            base.OnClick();
            if (isUsed)
            {
                return;
            }

            isUsed = true;
            button.interactable = false;

            Roar(CustomEvents.HalfJokerUsed);
        }

        #endregion
    }
}