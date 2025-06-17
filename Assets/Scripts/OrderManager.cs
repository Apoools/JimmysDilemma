using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    private Queue<CustomerOrderBubble> orderQueue = new Queue<CustomerOrderBubble>();

    [Header("Spawning Settings")]
    public Transform[] plateSlots;         // Drag PlateOrder1–4 here
    public GameObject foodOnPlatePrefab;   // Your prefab (plate + empty food child)
    private int nextPlateIndex = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterOrder(CustomerOrderBubble order)
    {
        if (!orderQueue.Contains(order))
        {
            orderQueue.Enqueue(order);
            Debug.Log("New order registered: " + order.currentOrder.name);
        }
    }

    public void TakeNextOrder()
    {
        if (orderQueue.Count > 0)
        {
            CustomerOrderBubble customer = orderQueue.Dequeue();
            Sprite foodSprite = customer.currentOrder;

            customer.HideOrder();
            StartCoroutine(SpawnFoodAfterDelay(foodSprite, 1.5f)); // 👈 Calls Step 3 here
        }
        else
        {
            Debug.Log("No orders in queue.");
        }
    }

    // ✅ Step 3 code goes here
    private IEnumerator SpawnFoodAfterDelay(Sprite food, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (nextPlateIndex >= plateSlots.Length)
        {
            Debug.LogWarning("All plate slots are full!");
            yield break;
        }

        Transform spawnPoint = plateSlots[nextPlateIndex];
        GameObject plateInstance = Instantiate(foodOnPlatePrefab, spawnPoint.position, Quaternion.identity);

        // Set the food sprite on the child named "Food"
        Transform foodChild = plateInstance.transform.Find("Food");
        if (foodChild != null)
        {
            SpriteRenderer sr = foodChild.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = food;
            }
        }

        nextPlateIndex++;
    }
}
