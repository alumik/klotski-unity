using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayArea : MonoBehaviour
{
    int bgLength = 960;    //华容道的边长
    public Vector2 [,] gridManager;  //所有格子的中心点位
    public int[,] gridFill;          //所有格子是否被填充（0是空，1是填充）
    Vector2 onePos;   //初始左下角第一个点的位置
    float stepLength;     //华容道每格长度
    List<string[]> setGrid = new List<string[]>();  //读取存放所有滑条的类型，名称，显示位置
    public GameObject gridPanel;  //滑条的父节点
    // Use this for initialization
    private static PlayArea _Instance;
    public static PlayArea Instance
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
    void Start()
    {
        AddPointPos();
        SetGrid();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void AddPointPos()
    {
        stepLength = bgLength / 6;
        gridManager = new Vector2 [6, 6];
        onePos = new Vector2(-stepLength * 2.5f, -stepLength * 2.5f);
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                gridManager[i, j] = new Vector2(onePos.x + i * stepLength, onePos.y + j * stepLength);
            }
        }
        gridFill = new int[6, 6];
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                gridFill[i, j] = 0;
            }
        }
    }
    //读取所有滑条的信息，并克隆添加到华容道上
    void SetGrid()
    {
        /* 代表信息：1（3代表3格滑条，2代表2格滑条，1代表唯一红色滑条）2（0代表横放，1代表竖放）
         * 3和4代表（滑条的轴心点在华容道内）5（滑条的名字） */
        string[] aa = new string[] { "3", "0", "3", "6", "WoodX1"};
        string[] bb = new string[] { "3", "1", "6", "4", "WoodY2" };
        string[] cc = new string[] { "2", "1", "3", "4", "WoodY3" };
        string[] ee = new string[] { "2", "1", "4", "4", "WoodY4" };
        string[] ff = new string[] { "3", "1", "1", "2", "WoodY5" };
        string[] gg = new string[] { "2", "1", "2", "2", "WoodY6" };
        string[] hh = new string[] { "2", "0", "3", "3", "WoodX7" };
        string[] ii = new string[] { "1", "0", "1", "4", "WoodXRed8"};
        setGrid.Add(aa);
        setGrid.Add(bb);
        setGrid.Add(cc);
        setGrid.Add(ee);
        setGrid.Add(ff);
        setGrid.Add(gg);
        setGrid.Add(hh);
        setGrid.Add(ii);
        //循环读取信息
        for(int i = 0; i < setGrid.Count; i++)
        {
            string[] mm = new string[4];
            mm = setGrid[i];   //读取每个滑条的信息
            GameObject girdCreat = null;
            Bar inputWood = null;
            int[] yy = new int[4] {int.Parse(mm[0]), int.Parse(mm[1]), int.Parse(mm[2]), int.Parse(mm[3]) };
            //判断滑条类型，然后查看显示位置并添加，并标记对应占据格子为已填充
            if(yy[0] == 2 && yy[1] == 0)
            {
                Vector2 zz = gridManager[yy[2] - 1, yy[3] - 1]; //读取坐标
                //克隆并命名
                girdCreat = Instantiate(Resources.Load("Prefabs/Block21")) as GameObject;
                girdCreat.name = mm[4];
                //设置已占据的格子
                gridFill[yy[2] - 1, yy[3] - 1] = 1;
                gridFill[yy[2] - 0, yy[3] - 1] = 1;
                //设置父节点
                girdCreat.transform.parent = gridPanel.transform;
                inputWood = girdCreat.GetComponent<Bar>();
                //写入添加的格子二维数组索引
                inputWood.woodFiled .Add(new int[2] { yy[2] - 1, yy[3] - 1 });
                inputWood.woodFiled .Add(new int[2] { yy[2] - 0, yy[3] - 1 });
                girdCreat.transform.localPosition = new Vector2(zz.x + stepLength / 2, zz.y);
                girdCreat.transform.localScale = Vector2.one;
            }
            if(yy[0] == 2 && yy[1] == 1)
            {
                Vector2 zz = gridManager[yy[2] - 1, yy[3] - 1];
                girdCreat = Instantiate(Resources.Load("Prefabs/Block12")) as GameObject;
                girdCreat.name = mm[4];
                gridFill[yy[2] - 1, yy[3] - 1] = 1;
                gridFill[yy[2] - 1, yy[3] - 0] = 1;
                girdCreat.transform.parent = gridPanel.transform;
                inputWood = girdCreat.GetComponent<Bar>();
                inputWood.woodFiled.Add(new int[2] { yy[2] - 1, yy[3] - 1 });
                inputWood.woodFiled.Add(new int[2] { yy[2] - 1, yy[3] - 0});
                girdCreat.transform.localPosition = new Vector2(zz.x, zz.y + stepLength / 2);
                girdCreat.transform.localScale = Vector2.one;
            }
            if(yy[0] == 3 && yy[1] == 0)
            {
                Vector2 zz = gridManager[yy[2] - 1, yy[3] - 1];
                girdCreat = Instantiate(Resources.Load("Prefabs/Block11")) as GameObject;
                girdCreat.name = mm[4];
                gridFill[yy[2] - 2, yy[3] - 1] = 1;
                gridFill[yy[2] - 1, yy[3] - 1 ] = 1;
                gridFill[yy[2] - 0, yy[3] - 1] = 1;
                girdCreat.transform.parent = gridPanel.transform;
                girdCreat.transform.localPosition = new Vector2(zz.x, zz.y);
                girdCreat.transform.localScale = Vector2.one;
                inputWood = girdCreat.GetComponent<Bar>();
                inputWood.woodFiled.Add(new int[2] { yy[2] - 2, yy[3] - 1 });
                inputWood.woodFiled.Add(new int[2] { yy[2] - 1, yy[3] - 1 });
                inputWood.woodFiled.Add(new int[2] { yy[2] - 0, yy[3] - 1 });
            }
            if(yy[0] == 1 && yy[1] == 0)
            {
                Vector2 zz = gridManager[yy[2] - 1, yy[3] - 1];
                girdCreat = Instantiate(Resources.Load("Prefabs/Block22")) as GameObject;
                girdCreat.name = mm[4];
                gridFill[yy[2] - 1, yy[3] - 1] = 1;
                gridFill[yy[2] - 0, yy[3] - 1] = 1;
                girdCreat.transform.parent = gridPanel.transform;
                inputWood = girdCreat.GetComponent<Bar>();
                inputWood.woodFiled.Add(new int[2] { yy[2] - 1, yy[3] - 1 });
                inputWood.woodFiled.Add(new int[2] { yy[2] - 0, yy[3] - 1 });
                girdCreat.transform.localPosition = new Vector2(zz.x + stepLength / 2, zz.y);
                girdCreat.transform.localScale = Vector2.one;
                inputWood.isRedWood = true;
            }
            if(girdCreat == null)
            {
                return;
            }
        }
    }
    private void OnDestroy()
    {
        gridManager = null;
        gridFill = null;
        onePos = Vector2.zero;
        setGrid = null;
    }
}
