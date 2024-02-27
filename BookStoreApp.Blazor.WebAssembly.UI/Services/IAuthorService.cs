﻿using BookStoreApp.Blazor.WebAssembly.UI.Models.Author;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDto>>> Get();
        Task<Response<AuthorDetailsDto>> Get(int Id);
        Task<Response<AuthorUpdateDto>> GetForUpdate(int Id);
        Task<Response<int>> Create(AuthorCreateDto author); 
        Task<Response<int>> Edit(int id , AuthorUpdateDto author);
        Task<Response<int>> Delete(int Id);
    }
}