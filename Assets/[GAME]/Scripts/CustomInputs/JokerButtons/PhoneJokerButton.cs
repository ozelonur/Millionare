using _GAME_.Scripts.GlobalVariables;

namespace _GAME_.Scripts.CustomInputs.JokerButtons
{
    public class PhoneJokerButton : JokerButton
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
            
            Roar(CustomEvents.PhoneJokerUsed);
        }

        #endregion
    }
}