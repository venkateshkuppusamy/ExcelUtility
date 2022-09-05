
using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Dynamic;

var fileName = "./Data/Test.xlsx";
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
JObject root = new JObject();
JObject rootCopy = root;
using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
{
    using (var reader = ExcelReaderFactory.CreateReader(stream))
    {
        string key = string.Empty;
        string property = string.Empty;
        JObject lpkeyValuePairs = null;
        bool IsArrStart = false;
        while (reader.Read()) //Each ROW
        {
            root = rootCopy;
            int i = 0;
            for (i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetValue(i) != null)
                {
                    key = reader.GetValue(i).ToString();
                    break;
                }
            }
            if (i !=0 && lpkeyValuePairs != null)
                root = lpkeyValuePairs;
            if (i != 0 && ((JProperty)root.Last).Value.ToString().StartsWith("arr_"))
            {
                key = ((JProperty)root.Last).Value.ToString();
            }

            for (i = i+1; i < reader.FieldCount-1; i++)
            {
                if (reader.GetValue(i) != null)
                {
                    if (key.StartsWith("arr_"))
                    {
                        IsArrStart = true;
                        var array = new JArray();
                        root.Add(key, array);
                        property = reader.GetValue(i).ToString();
                    }
                    else
                    {
                        JObject object1 = new JObject();
                        lpkeyValuePairs = object1;
                        root.Add(key, object1);
                        key = reader.GetValue(i).ToString();
                        root = object1;
                    }
                }
            }

            if (key.StartsWith("arr_"))
            {
                string value = reader.GetValue(reader.FieldCount - 1).ToString();
                if (value.Contains(";"))
                    root.Add(key, new JArray(value.Split(";").ToArray()));
                else
                {
                    var array = ((JArray)((JProperty)root.Last).Value);
                    var obj = new JObject();
                    obj.Add(property, value);
                    array.Add(obj);
                }
            }
            else
            {
                root.Add(key, reader.GetValue(reader.FieldCount - 1).ToString());
            }
        }
    }

    var jsonData = JsonConvert.SerializeObject(rootCopy);
    Console.WriteLine(jsonData);
}