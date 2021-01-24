using MovieApp.Shared.Models.MovieData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;

namespace MovieApp.Shared.Services
{
    public class TmdbApi
    {
        //TODO: use config
        private TMDbClient _client = new TMDbClient("dd47404357d7dfca4f753c17e666f789");

        public async Task<MovieDataDetail> GetAllMovieInfo(string title, string lang = "ru")
        {
            var results = await _client.SearchMovieAsync(title, lang);
            var data = results.Results.FirstOrDefault(p => p.Title == title);

            return new MovieDataDetail
            {
                Id = data.Id,
                Title = data.Title,
                OriginalTitle = data.OriginalTitle,
                Overview = data.Overview,
                ReleaseDate = data.ReleaseDate,
                Popularity = data.Popularity,                
            };
        }
    }
}
