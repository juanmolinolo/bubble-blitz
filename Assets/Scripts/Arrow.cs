using Assets.Constants;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D player;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip arrowMovementClip;

    [SerializeField]
    private AudioClip bubblePopClip;

    [SerializeField]
    private Rigidbody2D rb;

    private static bool isChainActive;

    void Start()
    {
        isChainActive = false;
    }

    void Update()
    {
        if (!isChainActive)
        {
            transform.position = player.position;
            rb.linearVelocity = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isChainActive = true;
                PlayArrowMovementClip();
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(0, ArrowConstants.MOVEMENT_SPEED);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isChainActive = false;
        audioSource.Stop();

        if (collision.CompareTag(BubbleConstants.BUBBLE_TAG))
        {
            var collidedBubble = collision.GetComponent<Bubble>();
            collidedBubble.Split();
            PlayBubblePopClip();
        }
    }

    private void PlayArrowMovementClip()
    {
        audioSource.PlayOneShot(arrowMovementClip);
    }

    private void PlayBubblePopClip()
    {
        audioSource.PlayOneShot(bubblePopClip);
    }
}
