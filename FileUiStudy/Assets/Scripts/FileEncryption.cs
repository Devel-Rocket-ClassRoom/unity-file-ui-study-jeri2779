using UnityEngine;
using System.IO;

public class FileEncryption : MonoBehaviour
{
    /*
**예상 출력**

```
원본: Hello Unity World
암호화 완료 (파일 크기: 17 bytes)
복호화 완료
복호화 결과: Hello Unity World
원본과 일치: True
```
     */

    // XOR 암복호화에 사용할 키 상수
    private const byte changeKey = 0xAB;

    private string secretPath;     // 원본 파일 생성
    private string encryptedPath;   // 암호화된 파일 생성
    private string decryptedPath;   // 복호화된 파일 생성
    private string message;         // 원본 출력

    void Start()
    {
        secretPath   = Path.Combine(Application.persistentDataPath, "secret.txt");
        encryptedPath = Path.Combine(Application.persistentDataPath, "encrypted.dat");
        decryptedPath = Path.Combine(Application.persistentDataPath, "decrypted.txt");
        message = "Hello Unity World";

        // 원본 파일 생성은 최초 1회 고정 출력
        File.WriteAllText(secretPath, message);
        Debug.Log($"원본: {message}");

        Debug.Log("Q : 암호화");
        Debug.Log("W : 복호화");
        Debug.Log("E : 복호화 결과 출력");
        Debug.Log("R : 원본과 일치 여부 확인");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Encrypt();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Decrypt();

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PrintDecrypted();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CheckEquals();
        }
    }

    void Encrypt()
    {
        if (!File.Exists(secretPath)) 
        { 
            Debug.Log("파일X"); 
            return; 
        }

        using (FileStream reader = File.OpenRead(secretPath))
        using (FileStream writer = File.Create(encryptedPath))
        {
            while (true)
            {
                int byteValue = reader.ReadByte();
                if (byteValue == -1) break;

                writer.WriteByte((byte)(byteValue ^ changeKey)); //  암호화
            }
        }

        FileInfo encryptedFileInfo = new FileInfo(encryptedPath);
        Debug.Log($"암호화 완료 (파일 크기: {encryptedFileInfo.Length} bytes)");
    }

    void Decrypt()
    {
        if (!File.Exists(encryptedPath)) 
        { 
            Debug.Log("암호화 파일X"); 
            return; 
        }

        using (FileStream reader = File.OpenRead(encryptedPath)) 
        using (FileStream writer = File.Create(decryptedPath)) 
        {
            while (true)
            {
                int byteValue = reader.ReadByte();//한 바이트씩 read
                if (byteValue == -1) break;

                writer.WriteByte((byte)(byteValue ^ changeKey));// 복호화
            }
        }

        Debug.Log("복호화 완료");
    }

    void PrintDecrypted()
    {
        if (!File.Exists(decryptedPath)) 
        { 
            Debug.Log("복호화 파일X"); 
            return; 
        }
        string decrypted = File.ReadAllText(decryptedPath);
        Debug.Log($"복호화 결과: {decrypted}");
    }

    void CheckEquals()
    {
        if (!File.Exists(decryptedPath)) 
        { 
            Debug.Log("복호화 파일X"); 
            return; 
        }
        string decrypted = File.ReadAllText(decryptedPath);
        Debug.Log($"원본과 일치: {message == decrypted}");
    }
}