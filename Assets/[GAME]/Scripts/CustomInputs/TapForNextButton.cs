using _GAME_.Scripts.Abstracts;
using _GAME_.Scripts.GlobalVariables;
using DG.Tweening;

namespace _GAME_.Scripts.CustomInputs
{
    public class TapForNextButton : ButtonBehavior
    {
        protected override void OnClick()
        {
            base.OnClick();
            transform.DOKill(true);
            print("Next Question");
            Roar(CustomEvents.NextQuestion);
        }
    }
}