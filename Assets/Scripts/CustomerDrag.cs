using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDrag : MonoBehaviour
{

    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            transform.position = mouseWorldPos + offset;
        }
    }

    void OnMouseDown()
    {
        if (GetComponent<CustomerSitter>()?.isSeated == true)
            return;

        if (CompareTag("Chair"))
        {
            Debug.Log("Chairs cannot be dragged.");
            return;
        }

        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset.z = 0f;
    }

    void OnMouseUp()
    {
        isDragging = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Chair"))
            {
                ChairPoint chair = hit.GetComponent<ChairPoint>();
                if (chair != null)
                {
                    CustomerSitter sitter = GetComponent<CustomerSitter>();
                    if (sitter != null)
                    {
                        sitter.SitAtChair(chair);
                        return;
                    }
                }

                // fallback position if SitPoint not configured
                transform.position = hit.transform.position + new Vector3(0f, 0.1f, 0f);
                return;
            }
        }

        // No valid chair found — reset to original
        transform.position = originalPosition;
    }


    // Optional: Gizmo to visualize the overlap area
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}
