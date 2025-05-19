using SalesTrack.Api.Hosting;

namespace SalesTrack.Api;

public class Program
{
    public static void Main(string[] args)
    {
        ApiHost.Run<Startup>(args);
    }
}
