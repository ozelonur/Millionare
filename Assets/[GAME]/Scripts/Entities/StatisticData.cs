using OrangeBear.Entity;

namespace _GAME_.Scripts.Entities
{
    public class StatisticData : Entity<StatisticData>
    {
        #region Init Methods

        protected override bool Init() => true;

        #endregion

        #region Public Variables

        public int GameCount;
        public int WinCount;
        public int JokerUsed;

        #endregion

        #region Public Methods

        public void IncreaseGameCount()
        {
            GameCount++;
            Save();
        }

        public void IncreaseWinCount()
        {
            WinCount++;
            Save();
        }

        public void IncreaseJokerUsed()
        {
            JokerUsed++;
            Save();
        }

        #endregion
    }
}