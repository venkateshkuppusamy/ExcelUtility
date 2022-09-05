// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json.Linq;

//Console.WriteLine("Hello, World!");

//JObject emp = new JObject();
//emp.Add("Name", "venki");
//emp.Add("Id", "1");

//Console.WriteLine(emp.ToString());

//JObject emp2 = new JObject(emp);
//emp2["Name"] = "anitha";
//emp2["Id"] = "2";

//JArray emps = new JArray();
//emps.Add(emp);
//emps.Add(emp2);

//Console.WriteLine(emps);

List<List<string>> maps = new List<List<string>>();

maps.Add(new List<string>() {"property1","value1" });
maps.Add(new List<string>() { "property2", "value2" });
maps.Add(new List<string>() { "object1", "property3", "value3" });
maps.Add(new List<string>() { "object1", "property4", "value4" });
maps.Add(new List<string>() { "object3", "object4", "property7", "value7" });
maps.Add(new List<string>() { "object3", "object4", "property8", "value8" });
maps.Add(new List<string>() { "arr_property10", "property10", "value10"});
maps.Add(new List<string>() { "arr_property10", "property11", "value11" });
maps.Add(new List<string>() { "arr_property10", "property10", "value10" });
maps.Add(new List<string>() { "arr_property10", "property11", "value11" });
maps.Add(new List<string>() { "arr_property11", "property10", "value10" });
maps.Add(new List<string>() { "arr_property11", "property11", "value11" });


JContainer root = new JObject();
JContainer head = root;
foreach (var map in maps)
{
	int i = 0;
	head = root;
	for (i = 0; i < map.Count-2; i++)
	{
		if (map[i].StartsWith("arr_"))
		{
			var obj = (JObject)head;

			if (obj.ContainsKey(map[i]))
			{
				if (((JObject)((JArray)obj.Property(map[i]).Value).Last).ContainsKey(map[i + 1]))
				{
					head = (JArray)obj.Property(map[i]).Value;
				}
				else
				{
					head = (JObject)obj.Property(map[i]).Value.Last;
				}

            }
			else
			{
				var arr = new JArray();
				obj.Add(map[i], arr);
				head = arr;
			}
			
		}
		else
		{
			if (head.Type == JTokenType.Object)
			{
				var obj = new JObject();
				var property = ((JObject)head).Property(map[i]);
				if (property == null)
				{
					((JObject)head).Add(map[i], obj);
					head = obj;
				}
				else
				{
					head = ((JObject)head[property.Name]);
				}
			}
			else
			{
                var obj = new JObject();
				((JArray)head).Add(obj);
				head = obj;
            }
		}
	}

	if (head.Type == JTokenType.Object)
		((JObject)head).Add(map[i], map[i + 1]);
	else
	{
        var obj = new JObject();
		obj.Add(map[i], map[i + 1]);
		((JArray)head).Add(obj);
	}
}

Console.WriteLine(root);

