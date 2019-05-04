using UnityEngine;

namespace Common
{
    public static class Store
    {
        public static StageConfig NextStageConfig;
        public static int LastSceneIndex;
        public static int Time;
        public static int Steps;
        public static float LastScrollPosition;
        public static Color CurrentColor = new Color32(0xF4, 0x43, 0x36, 0xFF);
        public static int CurrentColorIndex = 0;
        public static readonly Database Db = new Database();

        public const int SceneMainMenu = 0;
        public const int SceneLevelSelector = 1;
        public const int SceneGameInfo = 4;

        public static readonly byte[,] Colors =
        {
            {0xf4, 0x43, 0x36},
            {0x9c, 0x27, 0xb0},
            {0x3f, 0x51, 0xb5},
            {0x03, 0x9b, 0xe5},
            {0x00, 0x96, 0x88},
            {0x68, 0x9f, 0x38},
            {0xef, 0x6c, 0x00},
            {0x79, 0x55, 0x48},
            {0x60, 0x7d, 0x8b}
        };
    }
}