//using System.Collections.Generic;
//using UnityEngine;
//
//public class DragMove : MonoBehaviour
//{
//    private Vector2 mMouseDownPos;
//    private Vector2 mMouseUpPos;
//    private GameObject mCurrentBlock;
//    private bool mMoving;
//    private static DragMove mInstance;
//    public static DragMove instance => mInstance;
//
//    private Camera mCamera = Camera.main;
//
//    private void Awake()
//    {
//        mInstance = this;
//    }
//
//    void Update()
//    {
//        if (mMoving) return;
//        if (Input.GetMouseButtonDown(0) && mCamera != null)
//        {
//            var ray = mCamera.ScreenPointToRay(Input.mousePosition);
//            var isCollider = Physics.Raycast(ray, out var hitInfo);
//            if (hitInfo.collider == null) return;
//            if (isCollider && hitInfo.collider.gameObject.name.Contains("WoodX"))
//            {
//                judgeXY = 1;
//                touchWood = hitInfoOne.collider.gameObject;
//                dragWod = touchWood.transform.GetComponent<Bar>();
//                woodSave = dragWod.woodFiled;
//                btnDown = Input.mousePosition;
//            }
//
//            if (isColliderOne && hitInfoOne.collider.gameObject.name.Contains("WoodY"))
//            {
//                judgeXY = 2;
//                touchWood = hitInfoOne.collider.gameObject;
//                dragWod = touchWood.transform.GetComponent<Bar>();
//                woodSave = dragWod.woodFiled;
//                btnDown = Input.mousePosition;
//            }
//        }
//    }
//}
