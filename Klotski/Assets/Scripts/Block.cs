using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 80;

    private Vector3 mPMousePos;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CompareTag("Main Block") && other.gameObject.CompareTag("Exit"))
        {
            Debug.Log("Game Won");
        }
    }

    private void OnMouseDown()
    {
        if (Camera.main != null)
        {
            var mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = 10f;
            mPMousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnMouseDrag()
    {
        if (Camera.main != null)
        {
            var mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = 10f;
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            gameObject.GetComponent<Rigidbody2D>().velocity = (mouseWorldPos - mPMousePos) * moveSpeed;
            mPMousePos = mouseWorldPos;
        }
    }

    private void OnMouseUp()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        var pos = transform.position;
        var minDistance = float.PositiveInfinity;
        Vector2 minPos = pos;
        foreach (var gridPos in PlayArea.instance.GetGridPos())
        {
            var distance = Vector2.Distance(pos, gridPos);
            if (distance < minDistance)
            {
                minDistance = distance;
                minPos = gridPos;
            }
        }

        transform.position = minPos;
    }
}