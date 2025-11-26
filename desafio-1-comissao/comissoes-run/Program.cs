using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Linq;

class Venda { public string vendedor { get; set; } = string.Empty; public double valor { get; set; } }
class Root { public List<Venda> vendas { get; set; } = new List<Venda>(); }

class Program
{
    private const string JSON_FILENAME = "vendas.json";

    static void Main()
    {
        var filePath = FindJsonFile();
        if (filePath == null)
            return;

        string jsonText;
        try { jsonText = File.ReadAllText(filePath); }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
            return;
        }

        Root root;
        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            root = JsonSerializer.Deserialize<Root>(jsonText, options) ?? new Root();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao desserializar JSON: {ex.Message}");
            return;
        }

        if (root.vendas == null || root.vendas.Count == 0)
        {
            Console.Error.WriteLine("Nenhuma venda encontrada no JSON.");
            return;
        }

        var resultado = new Dictionary<string, double>();

        foreach (var venda in root.vendas)
        {
            double comissao = 0;
            if (venda.valor < 100) comissao = 0;
            else if (venda.valor < 500) comissao = venda.valor * 0.01;
            else comissao = venda.valor * 0.05;

            if (!resultado.ContainsKey(venda.vendedor)) resultado[venda.vendedor] = 0;
            resultado[venda.vendedor] += comissao;
        }

        var culture = new CultureInfo("pt-BR");
        Console.WriteLine("Comissões:");
        foreach (var kv in resultado)
        {
            var totalArredondado = Math.Round(kv.Value, 2);
            Console.WriteLine($"{kv.Key}: {totalArredondado.ToString("C", culture)}");
        }
    }
    static string? FindJsonFile()
    {
        var candidates = new[] {
            Path.Combine(AppContext.BaseDirectory, JSON_FILENAME),
            Path.Combine(Directory.GetCurrentDirectory(), JSON_FILENAME),
            Path.Combine(Directory.GetCurrentDirectory(), "..", JSON_FILENAME)
        };

        foreach (var candidate in candidates)
        {
            if (File.Exists(candidate))
            {
                Console.WriteLine($"Arquivo encontrado: {Path.GetFullPath(candidate)}\n");
                return Path.GetFullPath(candidate);
            }
        }

        Console.Error.WriteLine($"Erro: '{JSON_FILENAME}' não encontrado nos locais esperados.");
        foreach (var c in candidates)
            Console.Error.WriteLine($"  - {Path.GetFullPath(c)}");
        return null;
    }
}
