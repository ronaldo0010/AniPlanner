using AniPlannerApi.Models;
using AniPlannerApi.Models.Types;
using Newtonsoft.Json;

namespace AniPlannerApi.Data;


public static class AnimeSeed
{
    public class AnimeDataWrapper
    {
        public List<AnimeData> Data { get; set; }
        
    }

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
    
    public static bool SeedData(DataContext context)
    {
        Console.WriteLine("ATTEMPTING SEED...");
        try
        {
            var currentDir = Directory.GetCurrentDirectory();
            var dataFilePath = $@"{currentDir}\Data\anime-data.json";
            
            Console.WriteLine("SEEDING...");
            var fileStream = new FileStream(dataFilePath, FileMode.Open, FileAccess.Read);
            var jsonReader = new JsonTextReader(new StreamReader(fileStream));
                
            var serializer = new JsonSerializer();
            serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
            serializer.NullValueHandling = NullValueHandling.Ignore;
            
            // Deserialize the JSON data to a list of AnimeData
            var animeDataListWrapper = serializer.Deserialize<AnimeDataWrapper>(jsonReader);            
            var animeDataList = animeDataListWrapper?.Data ?? Enumerable.Empty<AnimeData>();
            
            // Map AnimeData to Media and add to the mediaList
            const int batchSize = 1000;
            var batchCount = (int)Math.Ceiling((double)animeDataList.Count() / batchSize);

            for (var i = 0; i < batchCount; i++)
            {
                var batchAnimeData = animeDataList
                    .Skip(i * batchSize)
                    .Take(batchSize)
                    .ToList();
                
                var mediaList = new List<Media>();

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
        
                    media.AddTags(animeData.Tags);

                    lock (mediaList)
                    {
                        mediaList.Add(media);
                    }
                });
                Console.WriteLine($"{i}/{batchCount} completed...");
                context.Media.AddRange(mediaList);
                context.SaveChanges();
            }
            
            Console.WriteLine("SEEDED...");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("FAILED SEEDING!!");
            Console.WriteLine("ERROR:");
            Console.WriteLine("================");
            Console.WriteLine(e);
            Console.WriteLine("================");
            return false;
        }
        
        
    }
    private static MediaType ParseMediaType(string type)
    {
        // Implement your logic to map the string 'type' to the corresponding MediaType enum value
        return MediaType.TV;
    }

    // Helper method to parse string to MediaStatus enum
    private static MediaStatus ParseMediaStatus(string status)
    {
        // Implement your logic to map the string 'status' to the corresponding MediaStatus enum value
        return MediaStatus.ONGOING;
    }

    // Helper method to map tag string to MediaTag
    private static Tag MapToTag(string tag)
    {
        // Implement your logic to map the tag string to a MediaTag object
        // Return a new MediaTag object based on the tag string
        return new Tag { Name = tag};
    }
}