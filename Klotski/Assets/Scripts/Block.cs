using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 80;

    private Vector3 mPMousePos;
    private Vector3 mPPos;
    private AudioSource mAudioSource;

    private void Start()
    {
        mPPos = transform.position;
        mAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CompareTag("Main Block") && other.gameObject.CompareTag("Exit"))
        {
            FindObjectOfType<PlayArea>().GameWon();
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

        if (BackgroundMusic.Instance && BackgroundMusic.Instance.GetComponent<AudioSource>().isPlaying)
        {
            mAudioSource.PlayOneShot(mAudioSource.clip);
        }

        transform.position = minPos;
        if (Vector3.Distance(minPos, mPPos) > 0.1f)
        {
            FindObjectOfType<PlayArea>().AddStep();
        }

        mPPos = minPos;
    }
}