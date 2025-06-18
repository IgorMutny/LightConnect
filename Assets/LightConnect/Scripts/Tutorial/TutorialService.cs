namespace LightConnect.Tutorial
{
    public class TutorialService
    {
        private TutorialSettings _tutorialSettings;

        public TutorialService(TutorialSettings tutorialSettings)
        {
            if (Instance == null)
                Instance = this;
            else
                throw new System.Exception("Tutorial service has been already created");

            _tutorialSettings = tutorialSettings;
        }

        public static TutorialService Instance { get; private set; }

        public bool GetMessageForLevel(int levelId, out TutorialMessage message)
        {
            return _tutorialSettings.GetMessageForLevel(levelId, out message);
        }
    }
}