using UnityEngine;
using SaveDataVC = SaveDataV3;
using Newtonsoft.Json;
using System.IO;
using System = UnityEngine.Random;
 
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using CsvHelper.Configuration.Attributes;
using UnityEditor;
using Newtonsoft.Json.Serialization;
 





public static class SaveLoadManager

{
    public enum SaveMode
    {
        Text,       //.json
        Encrypted //.dat  
    }
    public static SaveMode Mode = SaveMode.Encrypted;
    private static byte[] encrypted;

    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";//저장 파일이 위치할 디렉토리 경로, Unity의 영구 데이터 경로에 "Saves" 폴더 추가
    private static int SaveDataVersion { get;} = 3;//현재 저장 데이터 버전, 향후 데이터 구조 변경 시 버전 업 필요

    private static readonly string[] SaveFileNames =
    {
        "SaveAuto",
        "Save1",
        "Save2",
        "Save3",
    };//저장 파일 이름 배열, 자동 저장과 3개의 수동 저장 슬롯을 포함
    //private static readonly string[] EncryptedSaveFileNames =
    //{
    //    "SaveAuto.dat",
    //    "Save1.dat",
    //    "Save2.dat",
    //    "Save3.dat",
    //};//암호화된 저장 파일 이름 배열, .dat 확장자를 사용하여 텍스트 저장과 구분

    private static string GetSaveFilePath(int slot, SaveMode mode)
    {
        var ext = mode == SaveMode.Text ? ".json" : ".dat";
        return Path.Combine(SaveDirectory, $"{SaveFileNames[slot]}{ext}");
    }

    private static string GetSaveFilePath(int slot)
    {
        var ext = Mode == SaveMode.Text ? ".json" : ".dat";
        return Path.Combine(SaveDirectory, $"{SaveFileNames[slot]}{ext}");
    }
    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    private static JsonSerializerSettings jsonSettings = new JsonSerializerSettings
    {
        Formatting = Formatting.Indented, //JSON 문자열을 들여쓰기하여 가독성 향상
        TypeNameHandling = TypeNameHandling.All,
        //Converters = new JsonConverter[] { new Vector3Converter() }

    };
    
    //저장시 암호화/ 로드시 복호화 
    

    public static bool Save(int slot = 0)
    {
        return Save(slot, Mode);
    }

    public static bool Save(int slot, SaveMode mode)
    {
         
        if(Data == null || slot < 0 || slot >= SaveFileNames.Length)
        {
            Debug.LogError($"저장 실패: {slot})");
            return false;
        }

        try
        {
            if(!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            var json = JsonConvert.SerializeObject(Data, jsonSettings);//현재 저장 데이터를 JSON 문자열로 직렬화, jsonSettings를 사용하여 포맷팅과 타입 정보 포함
            string path = GetSaveFilePath(slot, mode);

            Debug.Log($"저장 경로: {path}");
            switch (mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    break;
                case SaveMode.Encrypted:
                    var encryptedData = CryptoUtil.Encrypt(json);
                    File.WriteAllBytes(path, encryptedData);
                    break;
            }
            Debug.Log($"저장 성공: {path}");
        }
        catch
        {
            Debug.LogError($"세이브 예외:");
            return false;
        }

        return true;
    }

    public static bool Load(int slot = 0)
    {
        return Load(slot, Mode);
    }

    public static bool Load(int slot, SaveMode mode)
    {
        if (slot < 0 || slot >= SaveFileNames.Length)
        {
            Debug.LogError($"로드 실패: {slot}");
            return false;
        }
        if(!File.Exists(GetSaveFilePath(slot, mode)))
        {
            Debug.LogError($"데이터 없음.");
            return false;
        }
        try
        {

            string path = GetSaveFilePath(slot, mode);  
            string json = string.Empty;
            switch (mode)
            {
                case SaveMode.Text:
                    json = File.ReadAllText(path);
                    break;
                case SaveMode.Encrypted:
                    var encryptedData = File.ReadAllBytes(path);
                    json = CryptoUtil.Decrypt(encryptedData);
                    break;  
            }


            //string json = File.ReadAllText(path);
            var saveData = JsonConvert.DeserializeObject<SaveData>(json, jsonSettings);
            while (saveData.Version < SaveDataVersion)
            {
                
                saveData = saveData.VersionUp();
                Debug.Log(saveData.Version);
            }
            Data = saveData as SaveDataVC;

        }
        catch
        {
            Debug.LogError($"로드 예외:");
            return false;
        }


        return true;
    }

  


    public static List<string> CreateRandomItems()
    {
        var allIds = DataTableManager.itemTable.GetAllIds();//itemtable에서 id조회
        var randomItems = Random.Range(0, allIds.Count);//랜덤하게 아이템 하나 선택
        var selectedId = allIds[randomItems];//선택된 아이템 id
        var items = new List<string> { selectedId };//선택된 아이템 id를 리스트에 추가
        return items;


        //var count = Random.Range(1, 4);
        //var itemList = new List<string>();

        //for (int i = 0; i < count; i++)
        //{
        //    itemList.Add(allIds[Random.Range(0, allIds.Count)]);
        //}//
        //return itemList;
    }

     
}
