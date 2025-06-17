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
            StartCoroutine(SpawnFoodAfterDelay(foodSprite, 1.5f));
        }
        else
        {
            Debug.Log("No orders in queue.");
        }
    }

    private IEnumerator SpawnFoodAfterDelay(Sprite food, float delay)
    {
        yield return new WaitForSeconds(delay);

        Transform spawnPoint = plateSlots[nextPlateIndex];

        // Destroy any existing plate at the slot (optional but recommended)
        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
        }

        GameObject plateInstance = Instantiate(foodOnPlatePrefab, spawnPoint.position, Quaternion.identity, spawnPoint);

        Transform foodChild = plateInstance.transform.Find("Food");
        if (foodChild != null)
        {
            SpriteRenderer sr = foodChild.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = food;
            }
        }

        // Wrap the index for reuse
        nextPlateIndex = (nextPlateIndex + 1) % plateSlots.Length;
    }
}
