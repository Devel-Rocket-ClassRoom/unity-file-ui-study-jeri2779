using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

[Serializable]// 해당 클래스가 직렬화 가능하다는 것을 나타내는 어트리뷰트, JSON으로 변환할 때 필요
public class PlayerState

{
    public string PlayerName;
    public int lives;
    public float health;

    //[JsonConverter(typeof(Vector3Converter))]//position 필드에 Vector3Converter를 사용하여 JSON으로 변환할 때 커스텀 방식으로 처리하도록 지정

    public Vector3 position;

    public override string ToString()
    {
        return $" {PlayerName}, {lives}, {health}";
    }

}
public class JsonTest1 : MonoBehaviour
{

    private JsonSerializerSettings jsonSetting;

    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented, //JSON 문자열을 들여쓰기하여 가독성 향상
            Converters = new JsonConverter[] { new Vector3Converter() } //Vector3 타입을 JSON으로 변환할 때 Vector3Converter를 사용하도록 설정
        };
        //jsonSetting.Converters.Add(new Vector3Converter());

    }
    private void Update()
    {
        SetJson();
        GetJson();
    }

    private void SetJson()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Save
            PlayerState playerState = new PlayerState
            {
                PlayerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f)

            };

            string pathFolder = Path.Combine(Application.persistentDataPath, "JsonTest");

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            string path = Path.Combine(pathFolder, "playerJson2.json");
            string json = JsonConvert.SerializeObject(
                playerState,
                Formatting.Indented,
                jsonSetting
                ); //JsonUtility.ToJson(playerInfo, prettyPrint: true);
            File.WriteAllText(path, json);
            Debug.Log(path);
            Debug.Log(json);

        }
    }
    private void GetJson()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Load
            string path = Path.Combine(
                Application.persistentDataPath,
                "JsonTest",
                "playerJson2.json"
                );//파일 경로 설정
            string json = File.ReadAllText(path); //파일에서 json 문자열 읽어오기
            PlayerState playerState = JsonConvert.DeserializeObject<PlayerState>(json, jsonSetting); //json 문자열을 PlayerState 객체로 변환
            Debug.Log(json);
            Debug.Log(playerState);
            Debug.Log(playerState.position);

        }
    }
}


