using System.Text.Json;
using Spectre.Console;

class Program
{
    static async Task Main(string[] args)
    {
        Console.CursorVisible = false;
        string url = "http://cis-230-sp25.azurewebsites.net/api/StringArray"; // Replace with your desired URL

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string responseText = await client.GetStringAsync(url);
                responseText = responseText.Trim('[', ']');
                string[] responseArray = responseText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                WriteResponse(responseArray);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            catch (JsonException e)
            {
                Console.WriteLine($"JSON error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
            Console.ReadKey(true);
        }
    }

    private static void WriteResponse(string[] response)
    {
        for (int i = 1; i < response.Length; i++)
        {
            string color = GetColor(i);
            AnsiConsole.MarkupLine($"[{color}]{i}. {response[i]}[/]");
        }
    }

    private static string GetColor(int index)
    {
        while (index > 9)
        {
            index -= 9;
        }
        return index switch
        {
            1 => "red",
            2 => "#FE9900",
            3 => "#FCF649",
            4 => "green",
            5 => "cyan",
            6 => "blue",
            7 => "purple",
            8 => "magenta",
            9 => "#FF01F2",
            _ => "white"
        };
    }
}