using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.E; // Press E to interact
    public float pickupRange = 3f; // How far away you can grab items
    public Transform holdPosition; // An empty object in front of camera to hold item

    private GameObject heldItem; // Tracks what we are currently holding

    void Update()
    {
        // If we press E
        if (Input.GetKeyDown(pickupKey))
        {
            // If we are already holding something, drop it
            if (heldItem != null)
            {
                DropItem();
            }
            // If our hands are empty, try to pick something up
            else
            {
                TryPickUpItem();
            }
        }
    }

    void TryPickUpItem()
    {
        // Shoot a laser forward from the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            // Check if the item has the tag "Pickupable"
            if (hit.collider.CompareTag("Pickupable"))
            {
                heldItem = hit.collider.gameObject;

                // 1. Turn off physics gravity so it doesn't fall out of your hands
                if (heldItem.GetComponent<Rigidbody>())
                {
                    heldItem.GetComponent<Rigidbody>().isKinematic = true;
                }

                // 2. Glue the object to our hold position reference
                heldItem.transform.position = holdPosition.position;
                heldItem.transform.parent = holdPosition;
            }
        }
    }

    void DropItem()
    {
        // 1. Unglue the item from our hand
        heldItem.transform.parent = null;

        // 2. Turn physics gravity back on so it falls naturally
        if (heldItem.GetComponent<Rigidbody>())
        {
            heldItem.GetComponent<Rigidbody>().isKinematic = false;
        }

        // 3. Clear our hands
        heldItem = null;
    }
}