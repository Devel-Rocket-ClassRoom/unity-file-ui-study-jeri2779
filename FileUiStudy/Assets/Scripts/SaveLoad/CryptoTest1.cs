using UnityEngine;

public class CryptoTest1 : MonoBehaviour
{ 
    private byte[] encrypted;

    // Update is called once per frame
    void Update()
    {
       Encrypted();
       Decrypted();
    }

    private void Encrypted()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            string plainText = "Hello, World!";
            encrypted = CryptoUtil.Encrypt(plainText);

            Debug.Log($"Original: {plainText}");
            Debug.Log($"Encrypted: {encrypted}");
            Debug.Log($"Encrypted: {System.BitConverter.ToString(encrypted).Replace("-", "")}"); // Display encrypted data in hex format
        }
    }

    private void Decrypted()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (encrypted != null)
            {
                string plainText = CryptoUtil.Decrypt(encrypted);
                Debug.Log($"Decrypted: {plainText}");
            }
            else
            {
                Debug.Log("No encrypted data to decrypt.");
            }
        }
    }
}
