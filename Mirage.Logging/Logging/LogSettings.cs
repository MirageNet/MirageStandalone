using System.Collections.Generic;
namespace Mirage.Logging
{
    public class LogSettings
    {
        [System.Serializable]
        public struct Level
        {
            public string Name;
            public LogType level;
        };

        public List<Level> Levels = new List<Level>();

        // Start is called before the first frame update
        void Awake()
        {
            SetLogLevels();
        }

        public void SetLogLevels()
        {
            foreach (Level setting in Levels)
            {
                ILogger logger = LogFactory.GetLogger(setting.Name);
                logger.filterLogType = setting.level;
            }
        }
    }
}
