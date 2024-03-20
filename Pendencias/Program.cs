// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Pendencias;

internal class Program
{
	private static void Main()
	{
		var json = File.ReadAllText("Dados.json");
		List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
		int countRetorno = 0;

		List<Item> roupaLimpa = new List<Item>();
		List<Item> roupaSuja = new List<Item>();

		foreach (var item in items)
		{
			if (item.assetLocationId == 3)
			{
				roupaSuja.Add(item);
			}
			if (item.assetLocationId == 8)
			{
				roupaLimpa.Add(item);
			}
		}

		foreach (var item in roupaSuja)
		{
			if (item.dateTime < DateTime.Now.AddMinutes(-30))
			{
				foreach (var itemLimpo in roupaLimpa)
				{
					if (itemLimpo.id == item.id && itemLimpo.dateTime > item.dateTime)
					{
						countRetorno++;
					}
				}
			}
		}

		var countPendencias = roupaSuja.Count - countRetorno;
		Console.WriteLine(countPendencias);
	}
}