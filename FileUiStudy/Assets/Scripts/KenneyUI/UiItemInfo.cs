//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class UiItemInfo : MonoBehaviour
//{
//    public Image imageIcon;
//    public TextMeshProUGUI textName;
//    public TextMeshProUGUI textDescription;
//    public TextMeshProUGUI textType;
//    public TextMeshProUGUI textValue;
//    public TextMeshProUGUI textCost;

//    public void SetEmpty()
//    {
//        imageIcon.sprite = null;
//        textName.text = string.Empty;
//        textDescription.text = string.Empty;
//        textType.text = string.Empty;
//        textValue.text = string.Empty;
//        textCost.text = string.Empty;
//    }

//    public void SetSaveItemData(SaveItemData saveItemData)
//    {
//        ItemData data = saveItemData.ItemData;

//        imageIcon.sprite = data.SpriteIcon;;
//        textName.text = data.StringName;
//        textDescription.text = data.StringDesc;
//        textType.text = data.Type.ToString();
//        textValue.text = data.Value.ToString();
//        textCost.text = data.Cost.ToString();
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Alpha1))
//        {
//            SetEmpty();
//        }

//        if (Input.GetKeyDown(KeyCode.Alpha2))
//        {
//            SetSaveItemData(SaveItemData.GetRandomItem());
//        }
//    }
//}
