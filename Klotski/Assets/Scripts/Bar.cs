using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public int movFowardX;   //X轴方向移动
    public int movFowardY;   //Y轴方向上移动

    bool moveState; //是否正在移动
    float speed = 2000; //移动速度
    public bool isMoveNow;
    public bool isRedWood;
    Vector2 startPos;
    Vector2 targetPos;
    float distance;

    public List<int[]>woodFiled = new List<int[]>(); //用来存放占据在格子二维数组的索引

    void Update()
    {
        if(moveState)
        {
            distance = Vector2.Distance(transform.localPosition, targetPos);
            if(distance < 0.1f)
            {
                if(isRedWood)
                {
                    if(woodFiled[woodFiled.Count - 1][0] == 5)
                    {
                        moveState = false;
                        movFowardX = 0;
                        movFowardY = 0;
                        DragMove.Instance.isMove = false;
                        return;
                    }
                }
                moveState = false;
                movFowardX = 0;
                movFowardY = 0;
                DragMove.Instance.isMove = false;
                return;
            }
            else
            {
                //transform.Translate(new Vector3(movFowardX * speed * Time.deltaTime, movFowardY * speed * Time.deltaTime), Space.Self);
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPos, speed * Time.deltaTime);
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
