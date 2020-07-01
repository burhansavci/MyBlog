using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Core.Entities.Dtos
{
    public class Page<T> : IDto
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }

        public Page(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public static Page<T> CreatePaginatedResult(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new Page<T>(items, count, pageNumber, pageSize);
        }

        public static List<Page<T>> CreatePaginatedResultList(IQueryable<T> query, int startPageNumber, int endPageNumber, int pageSize)
        {
            var paginatedResult = new List<Page<T>>();
            for (var pageNumber = startPageNumber; pageNumber <= endPageNumber; pageNumber++)
            {
                paginatedResult.Add(CreatePaginatedResult(query, pageNumber, pageSize));
            }
            return paginatedResult;
        }

        public static async Task<Page<T>> CreatePaginatedResultAsync(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Page<T>(items, count, pageNumber, pageSize);
        }

        public static async Task<List<Page<T>>> CreatePaginatedResultListAsync(IQueryable<T> query, int startPageNumber, int endPageNumber, int pageSize)
        {
            var paginatedResult = new List<Page<T>>();
            for (var pageNumber = startPageNumber; pageNumber <= endPageNumber; pageNumber++)
            {
                paginatedResult.Add(await CreatePaginatedResultAsync(query, pageNumber, pageSize));
            }
            return paginatedResult;
        }
    }
}
