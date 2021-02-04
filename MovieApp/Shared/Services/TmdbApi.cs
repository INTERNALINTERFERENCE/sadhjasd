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
        public int id;

        public async Task<MovieDataDetail> GetAllMovieInfo(string title, string lang = "ru")
        {
            var results = await _client.SearchMovieAsync(title, lang);
            var id = results.Results.FirstOrDefault(p => p.Title == title).Id;
            var movieData = await _client.GetMovieAsync(id, TMDbLib.Objects.Movies.MovieMethods.ExternalIds | TMDbLib.Objects.Movies.MovieMethods.Videos | TMDbLib.Objects.Movies.MovieMethods.Credits | TMDbLib.Objects.Movies.MovieMethods.Keywords);

            return new MovieDataDetail
            {
                ImdbId = movieData.ImdbId,
                Title = movieData.Title,
                OriginalTitle = movieData.OriginalTitle,
                Overview = movieData.Overview,
                ReleaseDate = movieData.ReleaseDate,
                Popularity = movieData.Popularity,
                Genres = movieData.Genres,
                Keywords = movieData.Keywords


                //ImdId = movieData.Id,
                //Title = movieData.Title,
                //OriginalTitle = movieData.OriginalTitle,
                //Overview = movieData.Overview,
                //ReleaseDate = movieData.ReleaseDate,
                //Popularity = movieData.Popularity,
            };
        }

        public Task<IEnumerable<MovieDataDetail>> Search(string title)
            => _client.GetMovieAsync(id, TMDbLib.Objects.Movies.MovieMethods.ExternalIds | TMDbLib.Objects.Movies.MovieMethods.Videos | TMDbLib.Objects.Movies.MovieMethods.Credits | TMDbLib.Objects.Movies.MovieMethods.Keywords)
            .ContinueWith(async res => (await res).Select(x => new MovieDataDetail
            {
                Title = x.Title,
                OriginalTitle = x.OriginalTitle,
                Overview = x.Overview,
                ReleaseDate = x.ReleaseDate,
                Popularity = x.Popularity,
            }))
            .Unwrap();           
    }
}
