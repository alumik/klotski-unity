namespace Common
{
    public static class Store
    {
        public static StageConfig NextStageConfig;
        public static int LastSceneIndex;
        public static string Time;
        public static int Steps;
        public static float LastScrollPosition;
        public const int SceneMainMenu = 0;
        public const int SceneLevelSelector = 1;
        public const int SceneGameInfo = 4;
    }
}