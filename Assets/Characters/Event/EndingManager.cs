using UnityEngine;
using UnityEngine.Video; // Required to use video files!

public class EndingManager : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform playerObject;       // player obj pos checks yknow yknow

    // ending 1 vars ---------------------------------------------------------------------------------
    public float heightToTriggerEnding = 50f; // Go above this Y height to trigger
    public GameObject skyImage;               // Drag and drop your sky object here!

    [Header("Video Settings")]
    public VideoPlayer videoPlayer;      // Drag your Video Player object here

    // ---------------------------------------------------------------------------------------------------

    private bool endingHasStarted = false;

    void Start()
    {
        // Tell the video player to alert us automatically when the movie finishes
        videoPlayer.loopPointReached += OnVideoFinished;

        // Make sure the sky image starts completely hidden when the game starts
        if (skyImage != null)
        {
            skyImage.SetActive(false);
        }
    }

    void Update()
    {
        // If an ending already started playing, stop checking everything else
        if (endingHasStarted) return;

        // STEP 1: If the static secret variable is flipped, make the sky image appear
        if (SecretStatue.isSecretEndingEnabled && skyImage != null && !skyImage.activeSelf)
        {
            skyImage.SetActive(true);
        }

        // STEP 2 (ENDING 1): Only check height if the secret global variable is active and image is visible
        if (SecretStatue.isSecretEndingEnabled && playerObject.position.y >= heightToTriggerEnding)
        {
            PlayEndingVideo();
        }
    }

    void PlayEndingVideo()
    {
        endingHasStarted = true;

        // Turn on the video screen and play the mp4 file
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    // Unity automatically calls this function the exact frame the video ends
    void OnVideoFinished(VideoPlayer source)
    {
        Debug.Log("Video finished! Closing game...");
        Application.Quit(); // Closes your game build!
    }
}