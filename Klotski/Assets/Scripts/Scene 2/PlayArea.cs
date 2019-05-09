using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Scene_2
{
    public class PlayArea : MonoBehaviour
    {
        [SerializeField] private Vector2 origin;
        [SerializeField] private float cellSize;
        [SerializeField] private GameObject[] blockPrefabs;
        [SerializeField] private Text stepCounter;
        [SerializeField] private Text timer;
        [SerializeField] private StageAnimator animator;
        [SerializeField] private Button undoButton;
        [SerializeField] private Button redoButton;

        private GameObject[,] mGrid;
        private StageConfig mStageConfig;
        private Vector2[,] mGridPos;
        private int mSteps;
        private float mTime;
        private bool mStarted;
        private Stack<PlayerMove> mPlayerMovesL = new Stack<PlayerMove>();
        private Stack<PlayerMove> mPlayerMovesR = new Stack<PlayerMove>();
        private static PlayArea mInstance;
        public static PlayArea Instance => mInstance;
        
        private struct PlayerMove
        {
            public GameObject Block;
            public Vector3 OldPosition;
            public Vector3 NewPosition;
        }

        private void Awake()
        {
            mInstance = this;
        }

        private void Start()
        {
            mStageConfig = Store.NextStageConfig;
            InitGrid();
            InitBlocks();
        }

        private void Update()
        {
            AddTime();
        }

        public void GameWon()
        {
            Store.Time = (int) mTime;
            Store.Steps = mSteps;
            animator.GameWon();
        }

        public void AddStep()
        {
            mSteps++;
            stepCounter.text = mSteps + " 步";
        }

        public void StartTime()
        {
            mStarted = true;
        }
        
        public void ResetGame()
        {
            RemoveBlocks();
            ResetStacks();
            ResetSteps();
            ResetTime();
            InitGrid();
            InitBlocks();
        }

        public Vector2[,] GetGridPos()
        {
            return mGridPos;
        }

        public void AddMove(GameObject block, Vector3 oldPosition, Vector3 newPosition)
        {
            var move = new PlayerMove {Block = block, OldPosition = oldPosition, NewPosition = newPosition};
            mPlayerMovesR.Clear();
            redoButton.interactable = false;
            mPlayerMovesL.Push(move);
            undoButton.interactable = true;
        }

        public void UndoMove()
        {
            if (mPlayerMovesL.Count > 0)
            {
                var move = mPlayerMovesL.Pop();
                mPlayerMovesR.Push(move);
                redoButton.interactable = true;
                move.Block.GetComponent<Block>().MoveTo(move.OldPosition);
                SubtractStep();

                if (mPlayerMovesL.Count == 0)
                {
                    undoButton.interactable = false;
                }
            }
        }

        public void RedoMove()
        {
            if (mPlayerMovesR.Count > 0)
            {
                var move = mPlayerMovesR.Pop();
                mPlayerMovesL.Push(move);
                undoButton.interactable = true;
                move.Block.GetComponent<Block>().MoveTo(move.NewPosition);
                AddStep();
                
                if (mPlayerMovesR.Count == 0)
                {
                    redoButton.interactable = false;
                }
            }
        }

        private void SubtractStep()
        {
            mSteps--;
            stepCounter.text = mSteps + " 步";
        }

        private void ResetStacks()
        {
            mPlayerMovesL.Clear();
            mPlayerMovesR.Clear();
            undoButton.interactable = false;
            redoButton.interactable = false;
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
}