using System.Collections;
using UnityEngine;

public class FoodSlotHandler : MonoBehaviour
{
    [Header("Food Slot References")]
    public Transform foodSlotLeft;
    public Transform foodSlotRight;

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
                StartCoroutine(HandleFoodToMoney(food));
                leftActive = true;
            }
        }

        if (!rightActive && foodSlotRight.childCount > 0)
        {
            GameObject food = foodSlotRight.GetChild(0).gameObject;
            if (food != null)
            {
                StartCoroutine(HandleFoodToMoney(food));
                rightActive = true;
            }
        }
    }

    IEnumerator HandleFoodToMoney(GameObject foodObj)
    {
        yield return new WaitForSeconds(customerEatDelay);

        // Destroy the food plate
        if (foodObj != null)
        {
            Destroy(foodObj);
        }

        // Award money
        int payout = Random.Range(GameManager.Instance.moneyRange.x, GameManager.Instance.moneyRange.y + 1);
        GameManager.Instance.AddMoney(payout);
        Debug.Log($"[FoodSlot] Customer paid ₱{payout}");

        // Destroy the entire customer GameObject (root of CustomerSitter)
        CustomerSitter sitter = GetComponentInChildren<CustomerSitter>();
        if (sitter != null)
        {
            Destroy(sitter.transform.root.gameObject);
        }
    }
}
