using Scene_0;
using UnityEngine;

namespace Scene_2
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 100;

        private Vector3 mPMousePos;
        private Vector3 mPPos;
        private AudioSource mAudioSource;
        private Rigidbody2D rb;

        private void Start()
        {
            mPPos = transform.position;
            mAudioSource = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (CompareTag("Main Block") && other.gameObject.CompareTag("Exit"))
            {
                PlayArea.Instance.GameWon();
            }
        }

        private void OnMouseDown()
        {
            if (Camera.main != null)
            {
                var mouseScreenPos = Input.mousePosition;
                mouseScreenPos.z = 10f;
                mPMousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }

        private void OnMouseDrag()
        {
            if (Camera.main != null)
            {
                var mouseScreenPos = Input.mousePosition;
                mouseScreenPos.z = 10f;
                var mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
                rb.velocity = (mouseWorldPos - mPMousePos) * moveSpeed;
                mPMousePos = mouseWorldPos;
            }
        }

        private void OnMouseUp()
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            var pos = transform.position;
            var minDistance = float.PositiveInfinity;
            Vector2 minPos = pos;
            foreach (var gridPos in PlayArea.Instance.GetGridPos())
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
                PlayArea.Instance.AddStep();
            }

            mPPos = minPos;
        }
    }
}