using UnityEngine;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;

public abstract class DataTable  
{
    public static readonly string FormatPath = "DataTables/{0}";

    public abstract void Load(string filename);
  

    protected List<T> LoadCSV<T>(string text)
    {
        using (var reader = new StringReader(text))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<T>();
            return records.ToList();    
        }
    }
}

 
