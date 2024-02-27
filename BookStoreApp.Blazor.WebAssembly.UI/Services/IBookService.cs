﻿using BookStoreApp.Blazor.WebAssembly.UI.Models.Book;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services
{
    public interface IBookService
    {
        Task<Response<List<BookReadOnlyDto>>> Get();
        Task<Response<BookDetailsDto>> Get(int Id);
        Task<Response<BookUpdateDto>> GetForUpdate(int Id);
        Task<Response<int>> Create(BookCreateDto book);
        Task<Response<int>> Edit(int id, BookUpdateDto book);
        Task<Response<int>> Delete(int Id);
    }
}
