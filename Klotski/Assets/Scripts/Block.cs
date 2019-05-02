using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float speed = 2000;
    
    private int mMoveX;
    private int mMoveY;
    private bool mMoving;
    private bool mIsMainBlock;
    private Vector2 mTargetPos;
    private Vector2[]

    public void SetAsMainBlock()
    {
        mIsMainBlock = true;
    }

    private void Update()
    {
        if(mMoving)
        {
            var distance = Vector2.Distance(transform.position, mTargetPos);
            if(distance < 0.1f)
            {
                mMoving = false;
                mMoveX = 0;
                mMoveY = 0;
                DragMove.Instance.Stop();
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, mTargetPos, speed * Time.deltaTime);
            }
        }
    }
    
    public void ChangeMove(int moveStep, bool movState)
    {
        //startPos = transform.localPosition;
        targetPos = new Vector2(transform.localPosition.x + movFowardX * moveStep * 160, transform.localPosition.y + movFowardY * moveStep * 160);
        moveState = movState;
    }
    private void OnDestroy()
    {
        woodFiled = null;
    }
}
