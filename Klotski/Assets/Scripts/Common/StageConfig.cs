using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName = "Stage Config")]
    public class StageConfig : ScriptableObject
    {
        [Serializable]
        public struct BlockConfig
        {
            [SerializeField] public int type;
            [SerializeField] public int x;
            [SerializeField] public int y;
        }

        [SerializeField] private int difficulty;
        [SerializeField] private int stageId;
        [SerializeField] private string stageName;
        [SerializeField] private int minSteps;
        [SerializeField] private StageConfig nextStage;
        [SerializeField] private BlockConfig[] blocks;

        public int GetDifficulty()
        {
            return difficulty;
        }
        
        public int GetStageId()
        {
            return stageId;
        }

        public string GetStageName()
        {
            return stageName;
        }

        public int GetMinSteps()
        {
            return minSteps;
        }

        public StageConfig GetNextStage()
        {
            return nextStage;
        }
        
        public IEnumerable<BlockConfig> GetBlocks()
        {
            return blocks;
        }
    }
}