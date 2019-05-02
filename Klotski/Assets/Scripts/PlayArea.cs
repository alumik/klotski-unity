using UnityEngine;

public class PlayArea : MonoBehaviour
{
    [SerializeField] private Vector2 origin;
    [SerializeField] private float cellSize;
    [SerializeField] private GameObject[] blockPrefabs;
    [SerializeField] private StageConfig stageConfig;

    private bool[,] mGrid;
    private Vector2[,] mGridPos;
    private static PlayArea mInstance;

    public static PlayArea instance => mInstance;

    private void Awake()
    {
        mInstance = this;
    }

    private void Start()
    {
        InitGrid();
        InitBlocks();
    }

    private void InitGrid()
    {
        mGridPos = new Vector2[4, 5];
        mGrid = new bool[4, 5];
        for (var x = 0; x < 4; x++)
        {
            for (var y = 0; y < 5; y++)
            {
                mGridPos[x, y] = new Vector2(origin.x + x * cellSize, origin.y + y * cellSize);
                mGrid[x, y] = false;
            }
        }
    }

    private void InitBlocks()
    {
        foreach (var block in stageConfig.GetBlocks())
        {
            var pos = mGridPos[block.x, block.y];
            var blockObject = Instantiate(
                blockPrefabs[block.type],
                new Vector3(pos.x, pos.y, 0),
                Quaternion.identity);
            for (var x = block.x; x < block.width; x++)
            {
                for (var y = block.y; y < block.height; y++)
                {
                    mGrid[x, y] = true;
                }
            }

            if (block.type == stageConfig.GetMainBlockType())
            {
                blockObject.GetComponent<Block>().SetAsMainBlock();
            }
        }
    }
}