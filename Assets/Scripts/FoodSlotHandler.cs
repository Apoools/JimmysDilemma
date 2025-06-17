using System.Collections;
using UnityEngine;

public class FoodSlotHandler : MonoBehaviour
{
    [Header("Food Slot References")]
    public Transform foodSlotLeft;
   // public Transform foodSlotRight;

    [Header("Sit Points (customer anchors)")]
    public Transform sitPointLeft;
    public Transform sitPointRight;

    public float customerEatDelay = 5f;

    private bool leftActive = false;
    private bool rightActive = false;

    void Update()
    {
        if (!leftActive && foodSlotLeft.childCount > 0)
        {
            GameObject food = foodSlotLeft.GetChild(0).gameObject;
            if (food != null)
            {
                StartCoroutine(HandleFoodToMoney(food, sitPointLeft, true));
                leftActive = true;
            }
        }
        /*
        if (!rightActive && foodSlotRight.childCount > 0)
        {
            GameObject food = foodSlotRight.GetChild(0).gameObject;
            if (food != null)
            {
                StartCoroutine(HandleFoodToMoney(food, sitPointRight, false));
                rightActive = true;
            }
        }*/
    }

    IEnumerator HandleFoodToMoney(GameObject foodObj, Transform sitPoint, bool isLeft)
    {
        yield return new WaitForSeconds(customerEatDelay);

        // Destroy the food
        if (foodObj != null)
        {
            Destroy(foodObj);
        }

        // Award money
        int payout = Random.Range(GameManager.Instance.moneyRange.x, GameManager.Instance.moneyRange.y + 1);
        GameManager.Instance.AddMoney(payout);
        Debug.Log($"[FoodSlot] Customer paid ₱{payout}");

        // Try to find and destroy the customer
        if (sitPoint != null)
        {
            Debug.Log($"[FoodSlot] Checking sitPoint: {sitPoint.name} with {sitPoint.childCount} children");

            // Try via CustomerSitter script
            CustomerSitter customer = sitPoint.GetComponentInChildren<CustomerSitter>(true);
            if (customer != null)
            {
                Debug.Log($"[FoodSlot] Destroying customer via CustomerSitter: {customer.name}");
                Destroy(customer.gameObject);
            }
            else if (sitPoint.childCount > 0)
            {
                // Fallback: destroy first child
                Transform child = sitPoint.GetChild(0);
                Debug.Log($"[FoodSlot] Destroying customer via fallback: {child.name}");
                Destroy(child.gameObject);
            }
            else
            {
                Debug.LogWarning($"[FoodSlot] No customer found under {sitPoint.name}");
            }
        }

        // Reset state
        if (isLeft) leftActive = false;
        else rightActive = false;
    }
}
