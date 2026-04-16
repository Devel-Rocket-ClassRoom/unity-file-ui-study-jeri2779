using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileSetKeyValue : MonoBehaviour
{
    /*
 
**예상 출력**

```
설정 로드 완료 (항목 5개)
--- 변경 전 ---
bgm_volume = 70
language = kr
--- 변경 후 저장 ---
bgm_volume = 50
language = en
--- 최종 파일 내용 ---
master_volume=80
bgm_volume=50
sfx_volume=90
language=en
show_damage=true
```
     */
    //키를 누를시 기능이 실행되도록 구현 ->  변경전/변경후/최종출력
    private string path; //세팅 파일 생성
    private Dictionary<string, string> settings;//딕셔너리 생성

    void Start()
    {
        //파일 생성
        path = Path.Combine(Application.persistentDataPath, "settings.cfg");

        //KV 작성
        settings = new Dictionary<string, string>()
        {
            { "master_volume", "80" },//key-value
            { "bgm_volume", "70" },
            { "sfx_volume", "90" },
            { "language", "kr" },
            { "show_damage", "true" }
        };
        //딕셔너리값 -> 파일 쓰기
        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach (var kv in settings)
            {
                writer.WriteLine($"{kv.Key}={kv.Value}");
            }
        }

        //파일 읽어오기 & 딕셔너리 값 파싱
        settings = new Dictionary<string, string>();
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string key = parts[0];
                    string value = parts[1];
                    settings[key] = value;
                }
            }
        }

        Debug.Log($"설정 로드 완료 (항목 {settings.Count}개)");
        Debug.Log("Q : 변경 전 출력");
        Debug.Log(" W : 변경 후 저장");
        Debug.Log("E : 최종 파일 내용");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BeforeModify();
        }
        if (Input.GetKeyDown(KeyCode.W))  
        {
            AfterModify();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            FinalPrint();
        }
    }

    void BeforeModify()
    {
        Debug.Log("--- 변경 전 ---");
        Debug.Log($"bgm_volume = {settings["bgm_volume"]}");
        Debug.Log($"language = {settings["language"]}");
        //settings["bgm_volume"] = "70";
        //settings["language"] = "kr";
    }

    void AfterModify()
    {
        //값 변경   
        settings["bgm_volume"] = "50";
        settings["language"] = "en";

        Debug.Log("--- 변경 후 저장 ---");
        Debug.Log($"bgm_volume = {settings["bgm_volume"]}");
        Debug.Log($"language = {settings["language"]}");

        using (StreamWriter writer2 = new StreamWriter(path))
        {
            foreach (var kv in settings)
            {
                writer2.WriteLine($"{kv.Key}={kv.Value}");
            }
        }
    }
    void FinalPrint()
    { 
        Debug.Log("--- 최종 파일 내용 ---");
        Debug.Log(File.ReadAllText(path));
    }
}