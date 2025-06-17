using UnityEngine;

public class CustomerSitter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public bool isSeated { get; private set; } = false;
    public SpriteRenderer SpriteRenderer => spriteRenderer;

    [Header("Chair-Specific Sprites")]
    public Sprite sitLeftSprite;
    public Sprite sitRightSprite;
    public Sprite sitCenterSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer == null)
            Debug.LogError("[CustomerSitter] SpriteRenderer not found on customer!");
        else
            Debug.Log("[CustomerSitter] SpriteRenderer found: " + spriteRenderer.name);
    }

    public void SitAtChair(ChairPoint chair)
    {
        // Optional: if not already parented correctly, set position
        if (transform.parent != chair.sitPoint)
        {
            transform.SetParent(chair.sitPoint);
            transform.localPosition = Vector3.zero;
        }

        // Set sprite based on chair type
        switch (chair.chairType)
        {
            case ChairType.Left:
                spriteRenderer.sprite = sitLeftSprite;
                break;
            case ChairType.Right:
                spriteRenderer.sprite = sitRightSprite;
                break;
            case ChairType.Center:
                spriteRenderer.sprite = sitCenterSprite;
                break;
        }

        // Flip sprite if needed
        spriteRenderer.flipX = chair.flipSprite;

        isSeated = true;

        Debug.Log($"[SitAtChair] Seated on {chair.chairType}, sprite set to: {spriteRenderer.sprite?.name}");

        // Trigger order bubble if attached
        GetComponent<CustomerOrderBubble>()?.ShowOrderAfterDelay();
    }

    void OnDestroy()
    {
        Debug.Log($"[CustomerSitter] {gameObject.name} has been destroyed.");
    }
}
