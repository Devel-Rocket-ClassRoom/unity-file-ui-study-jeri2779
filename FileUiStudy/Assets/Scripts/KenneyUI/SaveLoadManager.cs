//using Newtonsoft.Json;
//using System.IO;
//using UnityEngine;
//using SaveDataVC = SaveDataV3;

//public enum SaveMode
//{
//    Text,       // JSON 텍스트 (.json) — 개발용
//    Encrypted,  // AES 암호화 바이너리 (.dat) — 릴리즈용
//}

//public class SaveLoadManager
//{
//    public static int SaveDataVersion { get; } = 3;

//    public static SaveMode Mode { get; set; } = SaveMode.Text;

//    public static SaveDataVC Data { get; set; } = new SaveDataVC();

//    private static readonly string[] SaveFileName =
//    {
//        "SaveAuto",
//        "Save1",
//        "Save2",
//        "Save3",
//    };

//    public static string SaveDirectory => $"{Application.persistentDataPath}/Save";

//    public static string GetSaveFilePath(int slot)
//    {
//        if (slot < 0 || slot >= SaveFileName.Length)
//        {
//            throw new System.ArgumentOutOfRangeException(nameof(slot), $"slot은 0 이상 {SaveFileName.Length - 1} 이하여야 합니다.");
//        }

//        var ext = Mode == SaveMode.Text ? ".json" : ".dat";
//        return Path.Combine(SaveDirectory, SaveFileName[slot] + ext);
//    }

//    private static JsonSerializerSettings settings = new JsonSerializerSettings()
//    {
//        Formatting = Formatting.Indented,
//        // TypeNameHandling.All: JSON에 $type 필드를 기록/복원.
//        // DeserializeObject<SaveData>로 부모 타입을 요청해도 $type을 보고 실제 타입(V1/V2/V3)으로 복원되어
//        // 구버전 세이브도 VersionUp() 마이그레이션 체인을 탈 수 있다.
//        TypeNameHandling = TypeNameHandling.All,
//    };

//    public static bool Save(int slot = 0)
//    {
//        if (Data == null || slot < 0 || slot >= SaveFileName.Length)
//        {
//            return false;
//        }

//        try
//        {
//            if (!Directory.Exists(SaveDirectory))
//            {
//                Directory.CreateDirectory(SaveDirectory);
//            }

//            var path = GetSaveFilePath(slot);
//            var json = JsonConvert.SerializeObject(Data, settings);

//            if (Mode == SaveMode.Text)
//            {
//                File.WriteAllText(path, json);
//            }
//            else
//            {
//                File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
//            }

//            return true;
//        }
//        catch
//        {
//            Debug.LogError("Save 예외 발생");
//            return false;
//        }
//    }

//    public static bool Load(int slot = 0)
//    {
//        if (slot < 0 || slot >= SaveFileName.Length)
//        {
//            return false;
//        }

//        var path = GetSaveFilePath(slot);
//        if (!File.Exists(path))
//        {
//            return false;
//        }

//        try
//        {
//            string json;
//            if (Mode == SaveMode.Text)
//            {
//                json = File.ReadAllText(path);
//            }
//            else
//            {
//                json = CryptoUtil.Decrypt(File.ReadAllBytes(path));
//            }

//            var dataSave = JsonConvert.DeserializeObject<SaveData>(json, settings);
//            // 구버전 세이브면 최신 버전까지 한 단계씩 끌어올린다.
//            while (dataSave.Version < SaveDataVersion)
//            {
//                var prevVersion = dataSave.Version;
//                dataSave = dataSave.VersionUp();
//                Debug.Log($"[SaveLoad] 마이그레이션 V{prevVersion} → V{dataSave.Version}");
//            }
//            Data = dataSave as SaveDataVC;
//            return true;
//        }
//        catch
//        {
//            Debug.LogError("Load 예외 발생");
//            return false;
//        }
//    }
//}
