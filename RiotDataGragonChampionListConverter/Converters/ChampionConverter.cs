using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RiotDataGragonChampionListConverter.Models;

namespace BlazorWebAssemblyDemo.UI.Services;

public class ChampionConverter : JsonConverter<IEnumerable<ChampionResultModel>>
{
    private const string _version = "14.3.1";
    private const string _championPath = $"https://ddragon.leagueoflegends.com/cdn/{_version}/img/champion/";

    public override IEnumerable<ChampionResultModel>? ReadJson(JsonReader reader, Type objectType, IEnumerable<ChampionResultModel>? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);

        var values = jObject.GetValue("data")?.Values().ToList();

        if (values is null)
            return null;

        var parsedModels = values.Select(x => x.ToObject<ChampionSourceModel>()!).ToList()
            .Select(x => new ChampionResultModel(x.Name, x.Title, x.Blurb, _championPath + x.Image.ImageFullPath));

        return parsedModels;
    }

    public override void WriteJson(JsonWriter writer, IEnumerable<ChampionResultModel>? value, JsonSerializer serializer)
    {
        throw new NotSupportedException();
    }

    private record ChampionSourceModel
    (
        [JsonProperty("id")]
        string Name,    
    
        string Title,

        string Blurb,

        [JsonProperty("image")]
        ChampionImageModel Image
    );

    private record ChampionImageModel
    (
        [JsonProperty("full")]
        string ImageFullPath
    );
}