
//using ExcelDataReader;
//using Newtonsoft.Json;
//using System.Data;
//using System.Dynamic;

//var fileName = "./Data/Test.xlsx";
//System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
//ExpandoObject root = new ExpandoObject();
//ExpandoObject rootCopy = root;
//using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
//{
//    using (var reader = ExcelReaderFactory.CreateReader(stream))
//    {
//        string key = string.Empty;
//        ExpandoObject lpkeyValuePairs = null;
//        bool IsArr = false;
//        while (reader.Read()) //Each ROW
//        {
//            root = rootCopy;
//            int i = 0;
//            for (i = 0; i < reader.FieldCount; i++)
//            {
//                if (reader.GetValue(i) != null)
//                {
//                    key = reader.GetValue(i).ToString();
//                    IsArr = key.StartsWith("arr_");
//                    break;
//                }
//            }
//            if (i !=0 && lpkeyValuePairs != null)
//                root = lpkeyValuePairs;

//            for (i = i+1; i < reader.FieldCount-1; i++)
//            {
//                if (reader.GetValue(i) != null)
//                {
//                    if (key.StartsWith("arr_"))
//                    { 
                        
//                    }
//                    ExpandoObject object1 = new ExpandoObject();
//                    lpkeyValuePairs = object1;
//                    root.TryAdd(key, object1);
//                    key = reader.GetValue(i).ToString();
//                    root = object1;
//                }
//            }
//            string value = reader.GetValue(reader.FieldCount - 1).ToString();
//            if (key.StartsWith("arr_"))
//            {
//                root.TryAdd(key,value.Split(";").ToArray());
//            }
//            root.TryAdd(key, reader.GetValue(reader.FieldCount-1));
//        }
//    }

//    var jsonData = JsonConvert.SerializeObject(rootCopy);
//    Console.WriteLine(jsonData);
//}