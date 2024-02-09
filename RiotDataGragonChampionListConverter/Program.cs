using BlazorWebAssemblyDemo.UI.Services;
using Newtonsoft.Json;
using RiotDataGragonChampionListConverter.Models;

Console.WriteLine("Insert SOURCE json filepath:");
var sourceFile = Console.ReadLine();

if (!File.Exists(sourceFile))
{
    Console.WriteLine("Source file was not found!");
    return;
}

Console.WriteLine("Insert RESULT json filepath:");
var resultFile = Console.ReadLine();

if (File.Exists(resultFile))
{
    Console.WriteLine("Result file already exists!");
    return;
}

string content;

try
{
    content = File.ReadAllText(sourceFile);
}
catch(Exception ex)
{
    Console.WriteLine($"Error reading file: {ex.Message}");
    return;
}

if (string.IsNullOrEmpty(sourceFile))
{
    Console.WriteLine("Json is empty!");
    return;
}

var champions = JsonConvert.DeserializeObject<IEnumerable<ChampionResultModel>>(content, new ChampionConverter());
var outputJson = JsonConvert.SerializeObject(champions);

Console.WriteLine("Conversion success!");

try
{
    File.WriteAllText(resultFile!, outputJson);
}
catch (Exception ex)
{
    Console.WriteLine($"Error reading file: {ex.Message}");
    return;
}
