using UnityEngine;

public class JimmyInteraction : MonoBehaviour
{
    public float interactRange = 0.6f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);
            foreach (Collider2D hit in hits)
            {
                if (hit.GetComponent<CustomerOrderBubble>() != null)
                {
                    OrderManager.Instance.TakeNextOrder();
                    break;
                }
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
