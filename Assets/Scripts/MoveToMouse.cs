using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    public bool allowSpriteOverride = true;
    public float movementSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isMoving = false;

    public SpriteRenderer spriteRenderer;
    public Sprite spriteUp, spriteDown, spriteLeft, spriteRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            isMoving = true;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 direction = (targetPosition - rb.position).normalized;
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, movementSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);

            // Set sprite based on direction
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                spriteRenderer.sprite = direction.x > 0 ? spriteRight : spriteLeft;
            }
            else
            {
                spriteRenderer.sprite = direction.y > 0 ? spriteUp : spriteDown;
            }

            if (Vector2.Distance(rb.position, targetPosition) < 0.05f)
            {
                isMoving = false;
            }
        }
    }
}
