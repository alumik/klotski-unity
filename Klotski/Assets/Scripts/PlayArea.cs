using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayArea : MonoBehaviour
{
    [SerializeField] private Vector2 origin;
    [SerializeField] private float cellSize;
    [SerializeField] private GameObject[] blockPrefabs;
    [SerializeField] private Text stepCounter;
    [SerializeField] private Text timer;

    private GameObject[,] mGrid;
    private StageConfig mStageConfig;
    private Vector2[,] mGridPos;
    private int mSteps;
    private float mTime;
    private bool mStarted;

    public static PlayArea instance { get; private set; }

    private void Awake()
    {
        instance = this;
        StartCoroutine(AudioLowPassIncrease());
    }

    private static IEnumerator AudioLowPassIncrease()
    {
        var filter = BackgroundMusic.Instance.GetComponent<AudioLowPassFilter>();
        filter.enabled = true;
        for (float f = 22000; f >= 1000; f -= 500)
        {
            filter.cutoffFrequency = f;
            yield return new WaitForSeconds((float) 0.01);
        }
    }

    private void Start()
    {
        mStageConfig = Store.NextStageConfig;
        InitGrid();
        InitBlocks();
        GameObject.Find("Stage Name").GetComponent<Text>().text = mStageConfig.GetStageName();
    }

    private void Update()
    {
        AddTime();
    }

    public void GameWon()
    {
        Store.Time = timer.text;
        Store.Steps = mSteps;
        FindObjectOfType<StageAnimator>().GameWon();
    }

    public void AddStep()
    {
        if (!mStarted) mStarted = true;
        mSteps++;
        stepCounter.text = mSteps + " 步";
    }

    public void Reset()
    {
        RemoveBlocks();
        ResetSteps();
        ResetTime();
        InitGrid();
        InitBlocks();
    }

    public Vector2[,] GetGridPos()
    {
        return mGridPos;
    }

    private void AddTime()
    {
        if (mStarted)
        {
            mTime += Time.deltaTime;
            PrintTime();
        }
    }

    private void PrintTime()
    {
        var timeSpan = TimeSpan.FromSeconds(mTime);
        timer.text = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }

    private void ResetTime()
    {
        mStarted = false;
        mTime = 0;
        PrintTime();
    }

    private void ResetSteps()
    {
        mSteps = 0;
        stepCounter.text = mSteps + " 步";
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