namespace Krosoft.Extensions.Samples.DotNet9.Api;

public static class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
}