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

		List<Item> roupasPendentes = new List<Item>();
		foreach (var item in roupaSuja)
		{
			bool retornou = false;
			if (item.dateTime < DateTime.Now.AddHours(-45))
			{
				foreach (var itemLimpo in roupaLimpa)
				{
					if (itemLimpo.id == item.id && itemLimpo.dateTime > item.dateTime)
					{
						retornou = true;
						break;
					}
				}
				if (!retornou)
				{
					roupasPendentes.Add(item);
				}
			}
		}

		foreach (var item in roupasPendentes)
		{
			Console.WriteLine($"Id: {item.id}, Tipo: {item.assetTypeName}");
		}
	}
}