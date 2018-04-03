using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Movies.Interfaces;

namespace MoviesApi
{
    public static class MapModels
    {
        public static IList<MovieResult> DoMapping(IList<IMoviesDto> moviesDto)
        {
            var mappedResults = new List<MovieResult>();

            if (moviesDto.Any())
            {
                mappedResults.AddRange(moviesDto.Select(dto => new MovieResult
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Genre = dto.Genre,
                    RunningTime = dto.RunningTime,
                    YearOfRelease = dto.YearOfRelease,
                    AverageRating = Math.Ceiling(dto.AverageRating * 20.0) / 20.0
                }));
            }

            return mappedResults;
        }
    }
}