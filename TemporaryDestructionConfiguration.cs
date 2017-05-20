using Rocket.API;

namespace arrowkuu.temporarydestruction
{
    public class TemporaryDestructionConfiguration : IRocketPluginConfiguration
    {
        public string TimeFrom;
        public string TimeTo;

        public void LoadDefaults()
        {
            TimeFrom = "10:00:00";
            TimeTo = "23:00:00";
        }
    }
}
