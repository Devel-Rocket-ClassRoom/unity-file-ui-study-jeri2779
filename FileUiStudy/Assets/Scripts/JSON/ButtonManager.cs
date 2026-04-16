using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ButtonManager : MonoBehaviour
{
    public GameObject cube;

    private JsonSerializerSettings jsonSettings;

    private List<(PrimitiveType type, GameObject obj)> createObj
        = new List<(PrimitiveType, GameObject)>();

    private readonly PrimitiveType[] objTypes = new PrimitiveType[]
    {
        PrimitiveType.Cube,
        PrimitiveType.Sphere,
        PrimitiveType.Capsule,
        PrimitiveType.Cylinder
    };

    private void Awake()
    {
        jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            Converters = new JsonConverter[]
            {
                new Vector3Converter(),
                new QuaternionConverter(),
                new ColorConverter()
            }
        };
    }

    public void OnClickSave()
    {
        // cube 저장(단일용)
        SomeClass someClass = new SomeClass
        {
            pos = cube.transform.position,
            rot = cube.transform.rotation,
            scale = cube.transform.localScale,
            color = cube.GetComponent<Renderer>().material.color
        };
        string cubePath = Path.Combine(Application.persistentDataPath, "SomeTest", "SomeClass.json");
        Directory.CreateDirectory(Path.GetDirectoryName(cubePath));
        File.WriteAllText(cubePath, JsonConvert.SerializeObject(someClass, jsonSettings));

        // 생성 도형 저장 (다수용)
        var shapeListData = new ShapeListData();
        foreach (var (type, obj) in createObj)
        {
            if (obj == null) continue;
            shapeListData.shapes.Add(new ShapeData
            {
                primitive = type,
                pos = obj.transform.position,
                rot = obj.transform.rotation,
                scale = obj.transform.localScale,
                color = obj.GetComponent<Renderer>().material.color
            }
            );
        }
        string shapesPath = Path.Combine(Application.persistentDataPath, "SomeTest", "ShapeList.json");
        File.WriteAllText(shapesPath, JsonConvert.SerializeObject(shapeListData, jsonSettings));

    }

    public void OnClickLoad()
    {
        // cube 불러오기(단일용)
        string cubePath = Path.Combine(Application.persistentDataPath, "SomeTest", "SomeClass.json");
        if (!File.Exists(cubePath)) { Debug.LogWarning($"파일X: {cubePath}"); return; }

        SomeClass someClass = JsonConvert.DeserializeObject<SomeClass>(
        File.ReadAllText(cubePath), jsonSettings);
        cube.transform.position = someClass.pos;
        cube.transform.rotation = someClass.rot;
        cube.transform.localScale = someClass.scale;
        cube.GetComponent<Renderer>().material.color = someClass.color;

        // 생성 도형 불러오기 (다수용)
        string shapesPath = Path.Combine(Application.persistentDataPath, "SomeTest", "ShapeList.json");
        if (!File.Exists(shapesPath)) return;
        foreach (var (_, obj) in createObj)
        {
            if (obj != null) Destroy(obj);
        }
        createObj.Clear();

        var shapeListData = JsonConvert.DeserializeObject<ShapeListData>(
            File.ReadAllText(shapesPath), jsonSettings);
        foreach (var data in shapeListData.shapes)
        {
            GameObject newObj = GameObject.CreatePrimitive(data.primitive);
            newObj.transform.position = data.pos;
            newObj.transform.rotation = data.rot;
            newObj.transform.localScale = data.scale;
            newObj.GetComponent<Renderer>().material.color = data.color;
            createObj.Add((data.primitive, newObj));
        }

    }

    public void OnClickCreate()
    {
        int randCount = Random.Range(1, 11);
        for (int i = 0; i < randCount; i++)
        {
            PrimitiveType selectedType = objTypes[Random.Range(0, objTypes.Length)];//도형 종류 선택
            GameObject obj = GameObject.CreatePrimitive(selectedType);//도형 생성
            obj.transform.position = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f));//위치

            obj.transform.rotation = Random.rotation;//회전 

            obj.GetComponent<Renderer>().material.color =
                Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);//색상

            createObj.Add((selectedType, obj));
        }
    }


    public void OnClickDelete()
    {
        foreach (var (_, obj) in createObj)
            if (obj != null) Destroy(obj);
        createObj.Clear();

    }
}
