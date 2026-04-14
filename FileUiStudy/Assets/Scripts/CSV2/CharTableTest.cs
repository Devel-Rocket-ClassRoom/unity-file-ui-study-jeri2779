using UnityEngine;

public class CharTableTest : MonoBehaviour
{ 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var charData = DataTableManager.CharacterTable.Get("Char1");
            Debug.Log(charData);
        }
    }
}//확인 완료
