using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Drag : MonoBehaviour
{
    private bool isDragging = false;

    private void Update()
    {
        if (isDragging) {
            //Dragging the 
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }
    private void OnMouseDown()
    {
        Debug.Log("Start Dragging");
        isDragging = true;

    }

    private void OnMouseUp()
    {
        Debug.Log("Stopped Dragging");
        isDragging = false;
    }
}
