using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;
using CsvHelper;
using TMPro;

public class DataTableTest : MonoBehaviour
{
    public string NameStringTablekr = "StringTableKr";
    public string NameStringTableEn = "StringTableEn";
    public string NameStringTableJp = "StringTableJp";

    public TextMeshProUGUI text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Variables.language = Language.Korean;
        if (Input.GetKeyDown(KeyCode.Alpha2)) Variables.language = Language.English;
        if (Input.GetKeyDown(KeyCode.Alpha3)) Variables.language = Language.Japanese;
    }

    public void OnClickStringTableKr()
    {
        //LoadStringTable(NameStringTablekr);
        Debug.Log(DataTableManager.StringTable.Get("Hello"));
        //var table = new StringTable();
        //table.Load(NameStringTablekr);
        //Debug.Log(table.Get("Hello"));

    }
    public void OnClickStringTableEn()
    {
        Debug.Log(DataTableManager.StringTable.Get("Abc"));
        //LoadStringTable(NameStringTableEn);
        //var table = new StringTable();
        //table.Load(NameStringTableEn);
        //Debug.Log(table.Get("Abc"));  
    }
    public void OnClickStringTableJp()
    {
        Debug.Log(DataTableManager.StringTable.Get("jp01"));
        //LoadStringTable(NameStringTableJp);
        //var table = new StringTable();
        //table.Load(NameStringTableJp);
        //Debug.Log(table.Get("jp01"));  
    }

    public void LoadStringTable(string strname)
    {
        var table = new StringTable();
        table.Load(strname);

        if (strname == NameStringTablekr)
        {
            Debug.Log(table.Get("Hello"));
        }
        else if (strname == NameStringTableEn)
        {
            Debug.Log(table.Get("Abc"));
        }
        else if (strname == NameStringTableJp)
        {
            Debug.Log(table.Get("jp01"));
        }
       

    }

  

}
