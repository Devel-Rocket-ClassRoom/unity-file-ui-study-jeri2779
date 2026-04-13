using System.Globalization;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Text;


public class  CSVData
{
    public string Id { get; set; }    
    public string String { get; set; }
}
public class CSVTest1 : MonoBehaviour
{

    public TextAsset textAsset;

   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            TextAsset textAsset = Resources.Load<TextAsset>("DataTables/StringTableKr");
            string csv = textAsset.text;
            using (var reader = new StringReader(csv))
            using (var csvReader = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<CSVData>();
                foreach (var record in records)
                {
                    Debug.Log($"Id: {record.Id}, String: {record.String}");
                }
            }
        }
    }
}
