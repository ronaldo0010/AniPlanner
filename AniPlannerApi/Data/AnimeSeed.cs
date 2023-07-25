using AniPlannerApi.Models;

namespace AniPlannerApi.Data;

public class AnimeSeed
{
    public static bool SeedData(DataContext context)
    {
        Console.WriteLine("ATTEMPTING SEED...");
        try
        {
            Console.WriteLine("SEEDING...");
            var reader = new JsonReader("");

            foreach (var batch in reader.ReadBatches())
            {
                foreach (var line in batch)
                {
                    Console.WriteLine(string.Join(",", line));
                }
            }

            context.Media.Add(new Media
            {
                
            });
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
}