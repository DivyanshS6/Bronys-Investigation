using UnityEngine;

public class SecretStatue : MonoBehaviour
{
    // just a global var to remember

    public static bool isSecretEndingEnabled = false;

    
    void OnDestroy() // ( what if I was always destroyed ;c )
    {
        isSecretEndingEnabled = true;

        // debuggg
        Debug.Log("The secret statue was destroyed! Secret ending unlocked!");
    }
}