using UnityEngine;

public class EnemyClose : MonoBehaviour
{
    // zis sets up ze objects we need to look at
    public Transform player;      // will need to attach these inside of unity
    public Transform enemyMJ;

    // settings 
    public float proximityRange = 5f;   // shake proximity
    public float shakeIntensity = 0.05f; // shake factor

    // just to know the orignal location of cam
    private Vector3 originalPosition;

    void Start()
    {
        // save the camera's starting spot so we don't permanently break it...idk what else to do here
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // just a safety check to see if I attached the items in unity
        if (player == null || enemyMJ == null)
        {
            return;
        }


        float distance = Vector3.Distance(player.position, enemyMJ.position); // the distance calc



        if (distance < proximityRange)
        {
            // pick a random direction and multiply it by how hard we want to shake
            Vector3 randomMovement = Random.insideUnitSphere * shakeIntensity;

            // apply that random movement to the camera's original resting spot
            transform.localPosition = originalPosition + randomMovement;

            // this makes super super scary shake shake shake - taylor swift
        }
        else
        {
            // if the enemy is far away, force the camera back to its normal resting spot
            transform.localPosition = originalPosition;
        }
    }
}