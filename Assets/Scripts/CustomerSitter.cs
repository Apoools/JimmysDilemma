using UnityEngine;

public class CustomerSitter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool isSeated { get; private set; } = false;

    // Sprites are manually assigned (even if they come from different sheets)
    public Sprite sitLeftSprite;
    public Sprite sitRightSprite;
    public Sprite sitCenterSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        Debug.Log("[CustomerSitter] SpriteRenderer found: " + (spriteRenderer != null));
    }


    public void SitAtChair(ChairPoint chair)
    {
        transform.position = chair.sitPoint.position;

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

        spriteRenderer.flipX = chair.flipSprite;
        isSeated = true;

        Debug.Log($"[SitAtChair] Seated on {chair.chairType}, sprite should be: {spriteRenderer.sprite?.name}");

        // ✅ Add this line to trigger the speech bubble
        GetComponent<CustomerOrderBubble>()?.ShowOrderAfterDelay();
    }

}
