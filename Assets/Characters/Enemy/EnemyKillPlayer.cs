using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyKillPlayer : MonoBehaviour
{
    public string playerTag = "Player";

    public static bool isCameraLockedOnMJ = false;

    // Cache variables to remember where the feet targets started out
    private Vector3 savedLeftLocalPos;
    private Vector3 savedRightLocalPos;
    private bool hasSavedHomePositions = false;

    void Start()
    {
        int deathCount = PlayerPrefs.GetInt("Deaths", 0);

        proceduralWalker walker = GetComponentInChildren<proceduralWalker>();

        // Save where the original foot targets live relative to the character
        if (walker != null && !hasSavedHomePositions)
        {
            if (walker.leftFootTarget != null) savedLeftLocalPos = walker.leftFootTarget.localPosition;
            if (walker.rightFootTarget != null) savedRightLocalPos = walker.rightFootTarget.localPosition;
            hasSavedHomePositions = true;
        }

        if (deathCount >= 5)
        {
            isCameraLockedOnMJ = true;

            if (walker != null)
            {
                // 1. Turn off the math moving the targets procedurally
                walker.enabled = false;

                // 2. Clear out their parent attachments so they float freely
                if (walker.leftFootTarget != null) walker.leftFootTarget.parent = null;
                if (walker.rightFootTarget != null) walker.rightFootTarget.parent = null;

                // 3. Teleport the physical target objects directly into the spine coordinates!
                // This forces the existing Rig 1 constraints to snap his joints to the torso instantly.
                if (walker.leftFootTarget != null && walker.glitchLeftTarget != null)
                {
                    walker.leftFootTarget.position = walker.glitchLeftTarget.position;
                }

                if (walker.rightFootTarget != null && walker.glitchRightTarget != null)
                {
                    walker.rightFootTarget.position = walker.glitchRightTarget.position;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            int currentDeaths = PlayerPrefs.GetInt("Deaths", 0);
            currentDeaths++;
            PlayerPrefs.SetInt("Deaths", currentDeaths);
            PlayerPrefs.Save();

            Debug.Log("Player died! Total deaths: " + currentDeaths);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    [ContextMenu("Reset Death Count")]
    public void ResetDeaths()
    {
        PlayerPrefs.SetInt("Deaths", 0);
        PlayerPrefs.Save();
        Debug.Log("Deaths wiped back to 0!");
    }
}