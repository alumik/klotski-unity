using UnityEngine;
using UnityEngine.UI;

public class PlayArea : MonoBehaviour
{
    [SerializeField] private Vector2 origin;
    [SerializeField] private float cellSize;
    [SerializeField] private GameObject[] blockPrefabs;

    private GameObject[,] mGrid;
    private StageConfig mStageConfig;
    private Vector2[,] mGridPos;

    public static PlayArea instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mStageConfig = Store.NextStageConfig;
        Store.NextStageConfig = null;
        InitGrid();
        InitBlocks();
        GameObject.Find("Stage Name").GetComponent<Text>().text = mStageConfig.GetStageName();
    }

    public void Reset()
    {
        RemoveBlocks();
        InitGrid();
        InitBlocks();
    }

    public Vector2[,] GetGridPos()
    {
        return mGridPos;
    }

    private void RemoveBlocks()
    {
        foreach (var block in mGrid)
        {
            Destroy(block);
        }
    }

    private void InitGrid()
    {
        mGridPos = new Vector2[4, 5];
        mGrid = new GameObject[4, 5];
        for (var x = 0; x < 4; x++)
        {
            for (var y = 0; y < 5; y++)
            {
                mGridPos[x, y] = new Vector2(origin.x + x * cellSize, origin.y + y * cellSize);
            }
        }
    }

    private void InitBlocks()
    {
        foreach (var block in mStageConfig.GetBlocks())
        {
            var pos = mGridPos[block.x, block.y];
            var blockObject = Instantiate(
                blockPrefabs[block.type],
                new Vector3(pos.x, pos.y, 0),
                Quaternion.identity,
                gameObject.transform);
            var thisScale = blockObject.transform.localScale;
            var parentScale = blockObject.transform.parent.localScale;
            blockObject.transform.localScale = new Vector3(
                thisScale.x / parentScale.x,
                thisScale.y / parentScale.y,
                thisScale.z / parentScale.z);
            mGrid[block.x, block.y] = blockObject;
        }
    }
}