using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class PlayerInfo
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;

    public Dictionary<string, int> scores = new Dictionary<string, int>
    {
        {"Stage1", 100},
        {"Stage2", 200}
    };//JsonUtility는 Dictionary를 지원하지 않으므로, scores 필드는 JSON으로 직렬화되지 않습니다.

}
public class JsonUtilityTest : MonoBehaviour
{
    
    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Save
            PlayerInfo playerInfo = new PlayerInfo
            {
                playerName = "Player1",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f)
            };

            string pathFolder = Path.Combine(Application.persistentDataPath, "JsonTest");

            if(!Directory.Exists(pathFolder))   
            {
                Directory.CreateDirectory(pathFolder);
            }

            string path = Path.Combine(pathFolder, "playerInfo.json");
            string json = JsonUtility.ToJson(playerInfo, prettyPrint: true);
            File.WriteAllText(path, json);
            Debug.Log(path);
            Debug.Log(json);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Load
            string path = Path.Combine(
                Application.persistentDataPath,
                "JsonTest", 
                "playerInfo.json"
                );//파일 경로 설정

            string json = File.ReadAllText(path); //파일에서 json 문자열 읽어오기

            PlayerInfo playerInfo = new PlayerInfo();
            JsonUtility.FromJsonOverwrite(json, playerInfo); //json 문자열을 playerInfo 객체에 덮어쓰기

            //
            //PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(json); //json 문자열을 PlayerInfo 객체로 변환
            //FromJson과 FromJsonOverwrite의 차이점은 FromJson은
            //새로운 객체를 생성하여 반환하는 반면,
            //FromJsonOverwrite는 기존 객체에 데이터를 덮어쓰는 방식.
            //따라서 FromJsonOverwrite를 사용할 때는 기존 객체가 null이 아니어야 하며,
            //필요한 경우 객체를 미리 생성해두어야 한다.   
            Debug.Log(playerInfo.playerName);
            Debug.Log(playerInfo.lives);
            Debug.Log(playerInfo.health);
            Debug.Log(playerInfo.position);
        }
    }
}
