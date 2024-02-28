namespace MovieViewingApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieViewingApp.Application.Interface;
    using MovieViewingApp.Domain;
    using MovieViewingApp.Domain.Models;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }



        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<List<Movie>>>> SearchMoviesByTitle([FromQuery] string title)
        {
            var result = await _movieService.SearchMoviesByTitleAsync(title);

            // Add the search query to the search history
            _movieService.AddToHistory(title);

            return Ok(result);
        }



        [HttpGet("{imdbId}")]
        public async Task<IActionResult> GetMovieDetails(string imdbId)
        {
            try
            {
                ApiResponse<ExtendedDetails> movieDetails = await _movieService.GetMovieDetailsAsync(imdbId);
                _movieService.AddToHistory(imdbId);                 
                return Ok(movieDetails);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }



        [HttpGet("searchHistory")]
        public ActionResult<List<string>> GetSearchHistory()
        {
            // Retrieve and return the search history
            var searchHistory = _movieService.GetSearchHistory();
            return Ok(searchHistory);
        }


    }

}
