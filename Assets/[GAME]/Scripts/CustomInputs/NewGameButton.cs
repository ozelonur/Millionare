using _GAME_.Scripts.Abstracts;
using _GAME_.Scripts.GlobalVariables;

namespace _GAME_.Scripts.CustomInputs
{
    public class NewGameButton : ButtonBehavior
    {
        #region Override Methods

        protected override void OnClick()
        {
            base.OnClick();
            Roar(CustomEvents.NewGameButtonClicked);
        }

        #endregion
    }
}