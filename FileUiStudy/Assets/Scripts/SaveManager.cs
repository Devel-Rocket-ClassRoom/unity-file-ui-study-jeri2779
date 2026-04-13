using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    /*
     * ### 문제 1. 세이브 파일 관리 시스템  
**예상 출력**

```
=== 세이브 파일 목록 ===
- save1.txt (.txt)
- save2.txt (.txt)
- save3.txt (.txt)
save1.txt → save1_backup.txt 복사 완료
save3.txt 삭제 완료
=== 작업 후 파일 목록 ===
- save1.txt (.txt)
- save1_backup.txt (.txt)
- save2.txt (.txt)
```
     */
    //키를 누를시 기능이 실행되도록 구현 -> 생성/조회/복사/삭제/최종조회

    private string saveDir;

    void Start()
    {
        saveDir = Path.Combine(Application.persistentDataPath, "SaveData");
       
        Debug.Log("Q : 파일 생성");
        Debug.Log("W : 파일 목록 조회");
        Debug.Log("E : save1.txt → save1_backup.txt 복사");
        Debug.Log("R : save3.txt 삭제");
        Debug.Log("T : 최종 파일 목록 출력");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Create();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            List();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Copy();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Delete();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("=== 작업 후 파일 목록 ===");
            List();
        }
    }

    void Create()
    {
        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
            Debug.Log($"세이브 폴더 생성: {saveDir}");
        }
        else
        {
            Debug.Log($"세이브 폴더 이미 존재: {saveDir}");
        }

        string saveText = "This is a save file.";
        File.WriteAllText(Path.Combine(saveDir, "save1.txt"), saveText);
        File.WriteAllText(Path.Combine(saveDir, "save2.txt"), saveText);
        File.WriteAllText(Path.Combine(saveDir, "save3.txt"), saveText);

        
    }

    void List()
    {
        if (!Directory.Exists(saveDir))
        {
            Debug.Log("세이브 폴더X");
            return;
        }
        Debug.Log("=== 파일 목록 ===");
        string[] listFiles = Directory.GetFiles(saveDir);
        foreach (string file in listFiles)
        {
            string fileName = Path.GetFileName(file);//파일명 추출
            string extension = Path.GetExtension(file);//확장자 추출
            Debug.Log($"{fileName} ({extension})");
        }
    }
    void Copy()
    {
        string copyPath = Path.Combine(saveDir, "save1.txt");
        string backupPath = Path.Combine(saveDir, "save1_backup.txt");

        if (!File.Exists(copyPath))
        {
            Debug.Log("save1.txt 가 없습니다.");
            return; 
        }

        File.Copy(copyPath, backupPath, true);
        Debug.Log("save1.txt → save1_backup.txt 복사 완료");
    }

    void Delete()
    {
        string deletePath = Path.Combine(saveDir, "save3.txt");

        if (!File.Exists(deletePath))
        {
            Debug.Log("save3.txt가 이미 삭제되었습니다.");
            return;
        }

        File.Delete(deletePath);
        Debug.Log("save3.txt 삭제 완료");
    }
}