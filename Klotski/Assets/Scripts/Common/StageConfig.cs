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

        [SerializeField] private int stageId;
        [SerializeField] private string stageName;
        [SerializeField] private BlockConfig[] blocks;

        public IEnumerable<BlockConfig> GetBlocks()
        {
            return blocks;
        }

        public string GetStageName()
        {
            return stageName;
        }

        public int GetStageId()
        {
            return stageId;
        }
    }
}