using UnityEngine;

namespace Common
{
    public class SafeArea : MonoBehaviour
    {
        private RectTransform mPanel;
        private Rect mLastSafeArea = new Rect(0, 0, 0, 0);

        private void Awake()
        {
            mPanel = GetComponent<RectTransform>();
            Refresh();
        }

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            var safeArea = GetSafeArea();
            if (safeArea != mLastSafeArea)
            {
                ApplySafeArea(safeArea);
            }
        }

        private static Rect GetSafeArea()
        {
            return Screen.safeArea;
        }

        private void ApplySafeArea(Rect r)
        {
            mLastSafeArea = r;
            var anchorMin = r.position;
            var anchorMax = r.position + r.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            mPanel.anchorMin = anchorMin;
            mPanel.anchorMax = anchorMax;
        }
    }
}