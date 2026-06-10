using UnityEngine;

public class ContactDelete : MonoBehaviour
{
    // A simple list of the items to exterminate >:D
    public GameObject[] itemsToDelete;

    // Triggers automatically when this key bumps into something
    void OnCollisionEnter(Collision collision)
    {
        // Look through your list of items to delete
        foreach (GameObject targetItem in itemsToDelete)
        {
            // If the thing we bumped into is one of the items on our list
            if (collision.gameObject == targetItem)
            {
                // Make it vanish instantly!
                Destroy(targetItem);
            }
        }
    }
}