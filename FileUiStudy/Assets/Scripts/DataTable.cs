using UnityEngine;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;

public abstract class DataTable  
{
    public static readonly string FormatPath = "DataTables/{0}";  
 

    public virtual void Load(string filename)
    {
        throw new System.NotImplementedException();
    }

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

 
