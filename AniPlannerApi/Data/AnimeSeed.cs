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
    
    public static async Task<bool> SeedData(DataContext context)
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
            var batchSize = 1000;
            
            // Map AnimeData to Media and add to the mediaList
            var batchCount = (animeDataList.Count() + batchSize - 1) / batchSize;

            var tasks = new List<Task>();

            for (int i = 0; i < batchCount; i++)
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

                tasks.Add(context.Media.AddRangeAsync(mediaList));
            }

            await Task.WhenAll(tasks);
            await context.SaveChangesAsync();
            
            
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

    // Helper method to parse string to MediaStatus enum
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