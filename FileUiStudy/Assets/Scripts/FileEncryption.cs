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
원본과 일치: True */
    //키를 누를시 기능이 실행되도록 구현 ->  암호화/복호화/복호화결과/원본과일치


    private const byte changeKey = 0xAB; // xor연산용 16진수 키

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
        Debug.Log("E : 복호화 결과");
        Debug.Log("R : 원본과 일치");
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
        // 원본 -> 암호화
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
       // 암호화 -> 복호화
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
        string decrypted = File.ReadAllText(decryptedPath);
        Debug.Log($"복호화 결과: {decrypted}");
    }

    void CheckEquals()
    {
        string decrypted = File.ReadAllText(decryptedPath);
        Debug.Log($"원본과 일치: {message == decrypted}");
    }
}