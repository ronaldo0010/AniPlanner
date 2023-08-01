using Entities.Models;
using Entities.Types;
using Newtonsoft.Json;

namespace AnimeSeed;

public static class DataSeeding
{
    
    // TODO: Add documentation
    public class AnimeDataWrapper
    {
        public List<AnimeData> Data { get; set; }
    }
    
    // TODO: Add documentation 
    public class AnimeSeason
    {
        public string Season { get; set; }
        public int Year { get; set; }
    }
    
    // TODO: Add documentation 
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
    
    // TODO: Add documentation 
    // DIP implementation from SOLID principles
    public static async IAsyncEnumerable<List<Media>> ProcessDataAsync()
    {
        const string fileName = "anime-data.json";
        const int batchSize = 1000;
        
        // TODO: move file to root of solution sproject 
        var filePath = $@"{Directory.GetCurrentDirectory()}\Data\{fileName}";
        var animeDataList = GetDataList(filePath).ToList();
        
        // TODO: possible performance refactor such that yield returns batches 
        // Map AnimeData to Media and add to the mediaList
        var batchCount = GetBatchCount(animeDataList.Count, batchSize);

        Console.WriteLine("==== START PROCESSING ====");
        for (var i = 0; i < batchCount; i++)
        {
            var batchAnimeData = animeDataList
                .Skip(i * batchSize)
                .Take(batchSize)
                .ToList();

            var mediaList = new List<Media>();

            await Task.Run(() =>
            {
                Parallel.ForEach(batchAnimeData, animeData =>
                {
                    var media = new Media
                    {
                        Type = ParseMediaType(animeData.Type),
                        Title = animeData.Title,
                        Status = ParseMediaStatus(animeData.Status),
                        PictureUrl = animeData.Picture,
                        Episodes = animeData.Episodes
                    };

                    // TODO: clean up for duplicate tags
                    media.AddTags(animeData.Tags);
                    
                    lock (mediaList)
                    {
                        mediaList.Add(media);
                    }
                });
            });

            Console.WriteLine($"{i + 1}/{batchCount} completed...");

            yield return mediaList;
        }
        Console.WriteLine("==== FINISHED PROCESSING ====");
    }

    // TODO: Add documentation 
    private static int GetBatchCount(int count, int batchSize)
    {
        return (count + batchSize - 1) / batchSize;
    }
    
    // TODO: Add documentation 
    private static IEnumerable<AnimeData> GetDataList(string dataFilePath)
    {
        var fileStream = new FileStream(dataFilePath, FileMode.Open, FileAccess.Read);
        var jsonReader = new JsonTextReader(new StreamReader(fileStream));
                
        var serializer = new JsonSerializer();
        serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
        serializer.NullValueHandling = NullValueHandling.Ignore;
            
        // Deserialize the JSON data to a list of AnimeData
        var animeDataListWrapper = serializer.Deserialize<AnimeDataWrapper>(jsonReader);            
        
        return animeDataListWrapper?.Data ?? Enumerable.Empty<AnimeData>();
    }

    // TODO: Add documentation 
    private static MediaType ParseMediaType(string type)
    {
        return type.ToLower() switch
        {
            "tv" => MediaType.TV,
            "movie" => MediaType.MOVIE,
            "ova" => MediaType.OVA,
            "ONA" => MediaType.ONA,
            "SPECIAL" => MediaType.SPECIAL,
            _ => MediaType.UNKNOWN
        };
    }

    // TODO: Add documentation 
    private static MediaStatus ParseMediaStatus(string status)
    {
        return status.ToLower() switch
        {
            "finished" => MediaStatus.FINISHED,
            "ongoing" => MediaStatus.ONGOING,
            "upcoming" => MediaStatus.UPCOMING,
            _ => MediaStatus.UNKNOWN
        };
    }
}