namespace LightConnect.Tutorial
{
    public class TutorialService
    {
        private TutorialSettings _tutorialSettings;

        public TutorialService(TutorialSettings tutorialSettings)
        {
            _tutorialSettings = tutorialSettings;
        }

        public bool GetMessageForLevel(int levelId, out TutorialMessage message)
        {
            return _tutorialSettings.GetMessageForLevel(levelId, out message);
        }
    }
}