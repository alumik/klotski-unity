using System;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private BlockConfig[] blocks;

    public IEnumerable<BlockConfig> GetBlocks()
    {
        return blocks;
    }
}