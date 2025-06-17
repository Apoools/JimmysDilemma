using System.Collections;
using UnityEngine;

public class CustomerOrderBubble : MonoBehaviour
{
    public GameObject speechBubble;            // parent bubble GameObject
    public SpriteRenderer orderImage;          // child sprite inside bubble
    public Sprite[] possibleOrders;            // list of food order sprites
    public float delayBeforeOrder = 2f;        // delay before showing bubble

    [HideInInspector] public Sprite currentOrder;

    private bool hasOrdered = false;

    void Start()
    {
        // Hide the bubble on startup
        if (speechBubble != null)
            speechBubble.SetActive(false);
        else
            Debug.LogWarning("[CustomerOrderBubble] speechBubble not assigned.");
    }

    public void ShowOrderAfterDelay()
    {
        if (!hasOrdered)
            StartCoroutine(DelayedOrder());
    }

    private IEnumerator DelayedOrder()
    {
        hasOrdered = true;
        yield return new WaitForSeconds(delayBeforeOrder);

        // Randomly pick a sprite
        if (possibleOrders.Length == 0)
        {
            Debug.LogError("[CustomerOrderBubble] No possibleOrders assigned.");
            yield break;
        }

        currentOrder = possibleOrders[Random.Range(0, possibleOrders.Length)];
        Debug.Log($"[OrderBubble] Order: {currentOrder.name}, Enabling speech bubble.");

        // Assign sprite safely
        if (orderImage != null)
        {
            orderImage.sprite = currentOrder;
        }
        else
        {
            Debug.LogError("[CustomerOrderBubble] 'orderImage' is not assigned!");
            yield break;
        }

        // Enable the bubble if it exists
        if (speechBubble != null)
        {
            speechBubble.SetActive(true);
        }
        else
        {
            Debug.LogError("[CustomerOrderBubble] 'speechBubble' is not assigned!");
            yield break;
        }

        // ✅ Register to the order queue only after bubble is shown successfully
        if (OrderManager.Instance != null)
        {
            OrderManager.Instance.RegisterOrder(this);
        }
        else
        {
            Debug.LogError("[CustomerOrderBubble] OrderManager instance not found!");
        }
    }

    public void HideOrder()
    {
        if (speechBubble != null)
            speechBubble.SetActive(false);
    }
}
