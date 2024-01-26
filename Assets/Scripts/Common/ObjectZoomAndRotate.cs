using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectZoomAndRotate : MonoBehaviour
{
    private bool isSelected = false;
    private Vector2 touchStartPos;
    private float initialPinchDistance;
    private Vector2 initialRotationVector;
    private bool isZooming = false;
    private RaycastHit hit;

    [Header("Parameters")]
    public float zoomSpeed;
    public float rotationSpeed;

    private void Start()
    {
        zoomSpeed = 0.1f;
        rotationSpeed = 0.5f;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Raycast to check if the touch hits an object
                    if (RaycastObject(touch.position))
                    {
                        isSelected = true;
                        touchStartPos = touch.position;
                        initialPinchDistance = 0f;
                        initialRotationVector = touch.position;
                    }
                    break;

                case TouchPhase.Ended:
                    isSelected = false;
                    break;
            }
        }

        // Perform zoom and rotate if the object is selected
        if (isSelected)
        {
            // Check for pinch gesture (zoom)
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                float pinchDistance = Vector2.Distance(touch1.position, touch2.position);

                if (!isZooming)
                {
                    initialPinchDistance = pinchDistance;
                    isZooming = true;
                }

                float pinchDelta = pinchDistance - initialPinchDistance;
                float scaleFactor = pinchDelta * zoomSpeed;

                transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * (1.0f + scaleFactor), Time.deltaTime * .05f);
            }
            else
            {
                isZooming = false;
            }

            // Check for rotate gesture (drag)
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        Vector2 deltaRotation = touch.position - initialRotationVector;
                        float rotationDelta = deltaRotation.x * rotationSpeed;
                        transform.Rotate(Vector3.up, -rotationDelta, Space.World); // Rotate around the Y-axis
                        initialRotationVector = touch.position;
                        break;
                }
            }
        }
    }

    // Raycast to check if the touch hits an object
    bool RaycastObject(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object is a child of the current GameObject
            return hit.collider.transform.IsChildOf(transform);
        }

        return false;
    }
}
