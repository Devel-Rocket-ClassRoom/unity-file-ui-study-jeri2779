using UnityEngine;

public class JsonTestObject : MonoBehaviour
{
    private Renderer ren;
    public string prefabName;

    private void Awake()
    {
        ren = GetComponent<Renderer>();
    }
    
    public void Set(ObjectSaveData data)
    {
        //
        transform.position = data.pos;
        transform.rotation = data.rot;
        transform.localScale = data.scale;
        ren.material.color = data.color;
    }
    public ObjectSaveData GetSaveData()
    {
        ObjectSaveData data = new ObjectSaveData
        {
            prefabName = prefabName,
            pos = transform.position,
            rot = transform.rotation,
            scale = transform.localScale,
            color = ren.material.color
        };
        return data;   
    }
     
}
