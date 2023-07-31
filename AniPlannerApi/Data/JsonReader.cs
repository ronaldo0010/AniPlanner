using Newtonsoft.Json;

namespace AniPlannerApi.Data;

public class AnimeSeason
{
    public string Season { get; set; }
    public int Year { get; set; }
}

public class AnimeData
{
    public List<string> Sources { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public int Episodes { get; set; }
    public string Status { get; set; }
    public AnimeSeason AnimeSeason { get; set; }
    public string Picture { get; set; }
    public List<string> Tags { get; set; }
}

public class JsonReader
{
    private readonly string _filePath;
    private readonly int _batchSize;

    public JsonReader(string filePath)
    {
        _filePath = filePath;
        _batchSize = 10;
    }

    public IEnumerable<string> ReadBatches()
    {
        using (var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
        using (var streamReader = new StreamReader(fileStream))
        using (var jsonReader = new JsonTextReader(streamReader))
        {
            var serializer = new JsonSerializer();
            var batch = new List<string>();
            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                {
                    // Read the JSON object as a string and add it to the batch
                    string jsonLine = serializer.Deserialize(jsonReader).ToString();
                    batch.Add(jsonLine);

                    // Check if the batch is full
                    if (batch.Count == _batchSize)
                    {
                        yield return string.Join(Environment.NewLine, batch);
                        batch.Clear();
                    }
                }
            }

            // Return the remaining lines as the last batch
            if (batch.Count > 0)
            {
                yield return string.Join(Environment.NewLine, batch);
            }
        }
    }
}