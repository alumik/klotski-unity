using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMove : MonoBehaviour
{
    Vector2 btnDown;//按下鼠标的屏幕位置
    Vector2 btnUp;//抬起鼠标屏幕位置
    int judgeXY; //判断滑条的横纵向
    public GameObject touchWood;
    Bar dragWod; //滑条自身脚本
    public bool isMove;
    List<int[]> woodSave = new List<int[]>();
    int[,] gridArray;
    private static DragMove _Instance;
    public static DragMove Instance
    {
        get
        {
            return _Instance;
        }
    }
    private void Awake()
    {
        _Instance = this;
    }
    void Update()
    {
        if(isMove)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            Ray rayOne = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfoOne;
            bool isColliderOne = Physics.Raycast(rayOne, out hitInfoOne);
            if(hitInfoOne.collider == null)
            {
                return;
            }
            if(isColliderOne && hitInfoOne.collider.gameObject.name.Contains("WoodX"))
            {
                judgeXY = 1;
                touchWood = hitInfoOne.collider.gameObject;
                dragWod = touchWood.transform.GetComponent<Bar>();
                woodSave = dragWod.woodFiled;
                btnDown = Input.mousePosition;
            }
            if(isColliderOne && hitInfoOne.collider.gameObject.name.Contains("WoodY"))
            {
                judgeXY = 2;
                touchWood = hitInfoOne.collider.gameObject;
                dragWod = touchWood.transform.GetComponent<Bar>();
                woodSave = dragWod.woodFiled;
                btnDown = Input.mousePosition;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(judgeXY == 0)
            {
                return;
            }
            btnUp = Input.mousePosition;
            gridArray = PlayArea.Instance.gridFill;
            if(judgeXY == 1)
            {
                if(btnUp.x - btnDown.x > 2)
                {
                    //X轴方向移动，取出滑条该方向上顶点  woodSave[woodSave.Count-1]
                    int[] ar = woodSave[woodSave.Count - 1];
                    if(ar[0] == 5)
                    {
                        return;
                    }
                    else
                    {
                        //下一个格子不为空则返回
                        if(gridArray[ar[0] + 1, ar[1]] == 1)
                        {
                            return;
                        }
                        //循环遍历，为空继续，计算步数
                        for(int i = 1; i < 6 - ar[0]; i++)
                        {
                            if(gridArray[ar[0] + i, ar[1]] == 1)
                            {
                                dragWod.movFowardX = 1;
                                //根据移动步数将对应位置置空，然后将移动后的位子标记占据
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1]] = 0;
                                }
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0] + (i - 1), ab[1]] = 1;
                                    dragWod.woodFiled[j] = new int[] { ab[0] + (i - 1), ab[1] };
                                }
                                dragWod.ChangeMove((i - 1), true);
                                isMove = true;
                                judgeXY = 0;
                                woodSave = null;
                                gridArray = null;
                                return;
                            }
                            //若滑条移动方向全部为空
                            if(ar[0] + i == 5 && gridArray[5, ar[1]] == 0)
                            {
                                dragWod.movFowardX = 1;
                                //根据移动步数将对应位置置空，然后将移动后的位子标记占据
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1]] = 0;
                                }
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0] + i, ab[1]] = 1;
                                    //将滑条自身的点位标记做出更改
                                    dragWod.woodFiled[j] = new int[] { ab[0] + i, ab[1] };
                                }
                                dragWod.ChangeMove(i, true);
                                isMove = true;
                                judgeXY = 0;
                                woodSave = null;
                                gridArray = null;
                                return;
                            }
                        }
                    }
                }
                else if(btnUp.x - btnDown.x < -2)
                {
                    int[] ar = woodSave[0];
                    if(ar[0] == 0)
                    {
                        return;
                    }
                    else
                    {
                        //循环遍历，为空继续
                        if(gridArray[ar[0] - 1, ar[1]] == 1)
                        {
                            return;
                        }
                        for(int i = 1; i < ar[0] + 1; i++)
                        {
                            if(gridArray[ar[0] - i, ar[1]] == 1)
                            {
                                dragWod.movFowardX = -1;
                                //根据移动步数将对应位置置空，然后将移动后的位子标记占据
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1]] = 0;
                                }
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0] - (i - 1), ab[1]] = 1;
                                    dragWod.woodFiled[j] = new int[] { ab[0] - (i - 1), ab[1] };
                                }
                                dragWod.ChangeMove((i - 1), true);
                                judgeXY = 0;
                                isMove = true;
                                woodSave = null;
                                gridArray = null;
                                return;
                            }
                            if(ar[0] - i == 0 && gridArray[0, ar[1]] == 0)
                            {
                                dragWod.movFowardX = -1;
                                //根据移动步数将对应位置置空，然后将移动后的位子标记占据
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1]] = 0;
                                }
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0] - i, ab[1]] = 1;
                                    dragWod.woodFiled[j] = new int[] { ab[0] - i, ab[1] };
                                }
                                dragWod.ChangeMove(i, true);
                                judgeXY = 0;
                                isMove = true;
                                woodSave = null;
                                gridArray = null;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            if(judgeXY == 2)
            {
                if(btnUp.y - btnDown.y > 2)
                {
                    int[] ar = woodSave[woodSave.Count - 1];
                    if(ar[1] == 5)
                    {
                        return;
                    }
                    else
                    {
                        //循环遍历，为空继续
                        if(gridArray[ar[0], ar[1] + 1] == 1)
                        {
                            return;
                        }
                        for(int i = 1; i < 6 - ar[1]; i++)
                        {
                            if(gridArray[ar[0], ar[1] + i] == 1)
                            {
                                dragWod.movFowardY = 1;
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1]] = 0;
                                }
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1] + (i - 1)] = 1;
                                    dragWod.woodFiled[j] = new int[] { ab[0], ab[1] + (i - 1) };
                                }
                                dragWod.ChangeMove((i - 1), true);
                                isMove = true;
                                judgeXY = 0;
                                woodSave = null;
                                gridArray = null;
                                return;
                            }
                            //若滑条移动方向全部为空
                            if(ar[1] + i == 5 && gridArray[ar[0], 5] == 0)
                            {
                                dragWod.movFowardY = 1;
                                //根据移动步数将对应位置置空，然后将移动后的位子标记占据
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1]] = 0;
                                }
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1] + i] = 1;
                                    //将滑条自身的点位标记做出更改
                                    dragWod.woodFiled[j] = new int[] { ab[0], ab[1] + i };
                                }
                                dragWod.ChangeMove(i, true);
                                isMove = true;
                                judgeXY = 0;
                                woodSave = null;
                                gridArray = null;
                                return;
                            }
                        }
                    }
                }
                else if(btnUp.y - btnDown.y < -2)
                {
                    int[] ar = woodSave[0];
                    if(ar[1] == 0)
                    {
                        return;
                    }
                    else
                    {
                        if(gridArray[ar[0], ar[1] - 1] == 1)
                        {
                            return;
                        }
                        for(int i = 1; i < ar[1] + 1; i++)
                        {
                            if(gridArray[ar[0], ar[1] - i] == 1)
                            {
                                dragWod.movFowardY = -1;
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1]] = 0;
                                }
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1] - (i - 1)] = 1;
                                    dragWod.woodFiled[j] = new int[] { ab[0], ab[1] - (i - 1) };
                                }
                                dragWod.ChangeMove((i - 1), true);
                                isMove = true;
                                judgeXY = 0;
                                woodSave = null;
                                gridArray = null;
                                return;
                            }
                            //若滑条移动方向全部为空
                            if(ar[1] - i == 0 && gridArray[ar[0], 0] == 0)
                            {
                                dragWod.movFowardY = -1;
                                //根据移动步数将对应位置置空，然后将移动后的位子标记占据
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1]] = 0;
                                }
                                for(int j = 0; j < woodSave.Count; j++)
                                {
                                    int[] ab = woodSave[j];
                                    gridArray[ab[0], ab[1] - i] = 1;
                                    //将滑条自身的点位标记做出更改
                                    dragWod.woodFiled[j] = new int[] { ab[0], ab[1] - i };
                                }
                                dragWod.ChangeMove(i, true);
                                isMove = true;
                                judgeXY = 0;
                                woodSave = null;
                                gridArray = null;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }

    private void OnDestroy()
    {
        btnDown = Vector2.zero;
        btnUp = Vector2.zero;
        dragWod = null;
        touchWood = null;
    }


}
