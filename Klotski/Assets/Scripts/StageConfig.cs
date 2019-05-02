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
        [SerializeField] public float width;
        [SerializeField] public float height;
        [SerializeField] public int x;
        [SerializeField] public int y;
    }

    [SerializeField] private BlockConfig[] blocks;
    [SerializeField] private int mainBlockType;

    public IEnumerable<BlockConfig> GetBlocks()
    {
        return blocks;
    }

    public int GetMainBlockType()
    {
        return mainBlockType;
    }
}