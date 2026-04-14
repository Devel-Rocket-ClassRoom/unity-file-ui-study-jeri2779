using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocalizationItem : MonoBehaviour
{
    private ItemTable itemTable;

    public string id;

    
    public TextMeshProUGUI text;


    public void SetItem(ItemData data)
    {
         text.text = data.Name;
    }

}
//4개의 버튼에 아이템 스프라이트/이름/설명이 있다.
//버튼 클릭시 중앙의 메인 버튼에 아이템 스프라이트/이름/설명이 나타난다.
//4개의 버튼은 스프라이트와 이름만 나타난다. (설명은 나타나지 않음)
//아이템 데이터는 csv파일에 있는것을 연동하여 가져온다.
 