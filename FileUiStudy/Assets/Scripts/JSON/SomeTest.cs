using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using Random = UnityEngine.Random;

//JsonTest2

[Serializable]
public class SomeClass  
{
    //각 필드를 직렬화 하여 json변환
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;

    public override string ToString()
    {
        return $"pos: {pos}, rot: {rot}, scale: {scale}, color: {color}";
    }
}
public class ShapeData
{
    public PrimitiveType primitive;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
}
[Serializable]
public class ShapeListData
{
    public List<ShapeData> shapes = new List<ShapeData>();
}

[Serializable]
public class ObjectSaveData
{
    public string prefabName;
    //각 필드를 직렬화 하여 json변환
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;

    public override string ToString()
    {
        return $"pos: {pos}, rot: {rot}, scale: {scale}, color: {color}";
    }
}

public class SomeTest : MonoBehaviour
{
    
    private JsonSerializerSettings jsonSetting;

    public GameObject target;
    public string FileName = "SomeClass.json";

    public string FullFilePath => Path.Combine(Application.persistentDataPath, "SomeTest", FileName);

    public string[] prefabNames =
    {   
        "Cube",
        "Sphere",
        "Capsule",
        "Cylinder"
    };



    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            Converters = new JsonConverter[] {
                new Vector3Converter(),
                new QuaternionConverter(),
                new ColorConverter() }
        };
    }
    private void Update()
    {

        SaveJson();
        LoadJson();

    }
    private void SaveJson()
    {
        //if문 alpah1 눌렀을 때 저장하는 로직
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SomeClass someClass = new SomeClass();

            someClass.pos = target.transform.position;
            someClass.rot = target.transform.rotation;
            someClass.scale = target.transform.localScale;
            someClass.color = target.GetComponent<Renderer>().material.color;
            var json = JsonConvert.SerializeObject(someClass, jsonSetting);
            string pathFolder = Path.Combine(Application.persistentDataPath, "SomeTest");

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            string path = Path.Combine(pathFolder, "SomeClass.json");


            //string json = JsonConvert.SerializeObject(someClass, jsonSetting);

            File.WriteAllText(path, json);
            Debug.Log(path);
            Debug.Log(json);


        }

    }

    private void LoadJson()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Load
            string path = Path.Combine(
                    Application.persistentDataPath,
                    "SomeTest",
                    "SomeClass.json"
                    );//파일 경로 설정
            if (!File.Exists(path))
            {
                Debug.LogWarning($"파일 없음: {path}");
                return;
            }
            string json = File.ReadAllText(path); //파일에서 json 문자열 읽어오기
            SomeClass someClass = JsonConvert.DeserializeObject<SomeClass>(json, jsonSetting);

            // someClass.pos = target.transform.position;
            // someClass.rot = target.transform.rotation;
            // someClass.scale = target.transform.localScale;
            // someClass.color = target.GetComponent<Renderer>().material.color;
            target.transform.position = someClass.pos;
            target.transform.rotation = someClass.rot;
            target.transform.localScale = someClass.scale;
            target.GetComponent<Renderer>().material.color = someClass.color;

            Debug.Log(json);
            Debug.Log(someClass.pos);
            Debug.Log(someClass.rot);
            Debug.Log(someClass.scale);
            Debug.Log(someClass.color);
        }
    }


    private void CreateRandomObject()
    {
        var prefabName = prefabNames[Random.Range(0, prefabNames.Length)];
        var prefab = Resources.Load<JsonTestObject>(prefabName);
        var obj = Instantiate(prefab);

        obj.transform.position = Random.insideUnitSphere * 10f;
        obj.transform.rotation = Random.rotation;
        obj.transform.localScale = Vector3.one * Random.Range(0.5f, 3f);
        obj.GetComponent<Renderer>().material.color = Random.ColorHSV();

    }
    public void OnCreate()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateRandomObject();
        }
    }
    public void OnClear()
    {
        var obj = GameObject.FindGameObjectsWithTag("TestObject");
        foreach (var objs in obj)
        {
            Destroy(objs);
        }
    }

    public void OnSave()
    {
        var objs = GameObject.FindGameObjectsWithTag("TestObject");
        var saveList = new List<ObjectSaveData>();
        foreach (var obj in objs)
        {
            var jsonTestObject = obj.GetComponent<JsonTestObject>();
            saveList.Add(jsonTestObject.GetSaveData());
            var json = JsonConvert.SerializeObject(saveList, jsonSetting);
            File.WriteAllText(FullFilePath, json);
        }
    }

    public void OnLoad()
    {
        OnClear();
        var json = File.ReadAllText(FullFilePath);
        var saveList = JsonConvert.DeserializeObject<List<ObjectSaveData>>(json, jsonSetting);

        foreach (var SaveData in saveList)
        {
            var prefab = Resources.Load<JsonTestObject>(SaveData.prefabName);
            var jsonTestObj = Instantiate(prefab);
            jsonTestObj.Set(SaveData);

        }
    }
}