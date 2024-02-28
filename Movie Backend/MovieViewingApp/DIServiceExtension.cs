using MovieViewingApp.Application.Implementations;
using MovieViewingApp.Application.Interface;

namespace MovieViewingApp
{
    public static class DIServiceExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddHttpClient<IMovieService, MovieService>();
        }
    }
}
