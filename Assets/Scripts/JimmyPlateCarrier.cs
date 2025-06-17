using System.Collections.Generic;
using UnityEngine;

public class JimmyPlateCarrier : MonoBehaviour
{
    [Header("Interaction")]
    public float interactRange = 1.0f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Plate Holding")]
    public Transform leftHandSlot;
    public Transform rightHandSlot;
    public int maxPlates = 2;

    [Header("Jimmy Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite holdRightSprite;
    public Sprite holdBothSprite;

    private List<GameObject> heldPlates = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (!TryServePlate())
            {
                TryPickupPlate();
            }
        }
    }

    bool TryPickupPlate()
    {
        if (heldPlates.Count >= maxPlates) return false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Plate"))
            {
                GameObject plate = hit.gameObject;

                // Disable plate collider so it can't be picked again
                plate.GetComponent<Collider2D>().enabled = false;

                // Parent to hand slot
                if (heldPlates.Count == 0)
                    plate.transform.SetParent(leftHandSlot);
                else if (heldPlates.Count == 1)
                    plate.transform.SetParent(rightHandSlot);

                plate.transform.localPosition = Vector3.zero;
                heldPlates.Add(plate);

                // Disable movement sprite override
                GetComponent<MoveToMouse>().allowSpriteOverride = false;

                UpdateSprite();
                return true;
            }
        }

        return false;
    }

    bool TryServePlate()
    {
        if (heldPlates.Count == 0) return false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("CustomerTable"))
            {
                Debug.Log("[Serve] Found CustomerTable");

                // Check both FoodSlotLeft and FoodSlotRight
                Transform leftSlot = hit.transform.Find("FoodSlotLeft");
                Transform rightSlot = hit.transform.Find("FoodSlotRight");

                if (leftSlot != null && leftSlot.childCount == 0)
                {
                    PlacePlateInSlot(leftSlot);
                    return true;
                }
                else if (rightSlot != null && rightSlot.childCount == 0)
                {
                    PlacePlateInSlot(rightSlot);
                    return true;
                }
                else
                {
                    Debug.Log("[Serve] No available slots (both filled?)");
                }
            }
        }

        return false;
    }

    void PlacePlateInSlot(Transform slot)
    {
        GameObject plate = heldPlates[0];
        heldPlates.RemoveAt(0);

        plate.transform.SetParent(slot);
        plate.transform.localPosition = Vector3.zero;
        plate.GetComponent<Collider2D>().enabled = true;

        UpdateSprite();

        // Re-enable movement sprite override if no more plates held
        if (heldPlates.Count == 0)
            GetComponent<MoveToMouse>().allowSpriteOverride = true;
    }

    void UpdateSprite()
    {
        Debug.Log($"[Jimmy] UpdateSprite() called | Plates: {heldPlates.Count}");

        if (heldPlates.Count == 0)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if (heldPlates.Count == 1)
        {
            spriteRenderer.sprite = holdRightSprite;
        }
        else if (heldPlates.Count == 2)
        {
            spriteRenderer.sprite = holdBothSprite;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
