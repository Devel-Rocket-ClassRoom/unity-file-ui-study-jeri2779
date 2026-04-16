using UnityEngine;


public class SaveLoadTest1 : MonoBehaviour
{
   

    private void Update()
    {
        SaveInfo();
        LoadInfo();
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //SaveLoadManager.Data.ItemId.AddRange(SaveLoadManager.CreateRandomItems());
            //Debug.Log($"목록: {string.Join(", ", SaveLoadManager.Data.ItemId)}");
        }

    }

    private void SaveInfo()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data.Name = "TEST1234";
            SaveLoadManager.Data.Gold = 9999;
            SaveLoadManager.Data.ItemId.Add("Item1");

            SaveLoadManager.Save();
            Debug.Log($"Player Name: {SaveLoadManager.Data.Name}");
            Debug.Log($"Player Gold: {SaveLoadManager.Data.Gold}");
            //Debug.Log($"Player Items: {string.Join(", ", SaveLoadManager.Data.ItemId)}");
        }
    }

    private void LoadInfo()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load())
            {
                Debug.Log($"Player Name: {SaveLoadManager.Data.Name}");
                Debug.Log($"Player Gold: {SaveLoadManager.Data.Gold}");
                //Debug.Log($"Player Items: {string.Join(", ", SaveLoadManager.Data.ItemId)}");
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
        }
    }


}
