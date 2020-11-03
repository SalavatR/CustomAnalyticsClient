using System;
using CustomGameAnalytics.Scripts.Settings;

namespace CustomGameAnalytics.Scripts.Events.Data
{
    [Serializable]
    public class LevelInfoData : EventData
    {
        public int level;
        private readonly string _type;

        public LevelInfoData(LevelInfoType levelInfoType, int level)
        {
            this.level = level;
            switch (levelInfoType)
            {
                case LevelInfoType.Start:
                    _type = EventTypes.LevelStart;
                    break;
                case LevelInfoType.Win:
                    _type = EventTypes.Win;
                    break;
                case LevelInfoType.Lose:
                    _type = EventTypes.Lose;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(levelInfoType), levelInfoType, null);
            }
        }

        internal override string type => _type;
    }

    public enum LevelInfoType : byte
    {
        Start,
        Win,
        Lose
    }
}