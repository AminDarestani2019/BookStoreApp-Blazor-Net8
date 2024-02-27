using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI.Models.Book;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services
{
    public class BookService : BaseHttpService,IBookService
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        public BookService(IClient client,ILocalStorageService localStorage,IMapper mapper):base(client,localStorage)
        {
            _client = client;
            _mapper = mapper;
        }
        public async Task<Response<int>> Create(BookCreateDto book)
        {
            Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await _client.BooksPOSTAsync(book);
            }
            catch (ApiException apiException)
            {
                response = ConvertApiExceptions<int>(apiException);
            }
            return response;
        }

        public async Task<Response<int>> Delete(int Id)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await _client.BooksDELETEAsync(Id);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }
            return response;
        }

        public async Task<Response<int>> Edit(int id, BookUpdateDto book)
        {
            Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await _client.BooksPUTAsync(id, book);
            }
            catch (ApiException apiException)
            {
                response = ConvertApiExceptions<int>(apiException);
            }
            return response;
        }

        public async Task<Response<List<BookReadOnlyDto>>> Get()
        {
            Response<List<BookReadOnlyDto>> response;
            try
            {
                await GetBearerToken();
                var data = await _client.BooksAllAsync();
                response = new Response<List<BookReadOnlyDto>>
                {
                    Data = data.ToList(),
                    Success = true,
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiExceptions<List<BookReadOnlyDto>>(apiException);
            }
            return response;
        }

        public async Task<Response<BookDetailsDto>> Get(int Id)
        {
            Response<BookDetailsDto> response;
            try
            {
                await GetBearerToken();
                var data = await _client.BooksGETAsync(Id);
                response = new Response<BookDetailsDto>
                {
                    Data = data,
                    Success = true,
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiExceptions<BookDetailsDto>(apiException);
            }
            return response;
        }

        public async Task<Response<BookUpdateDto>> GetForUpdate(int Id)
        {
            Response<BookUpdateDto> response;
            try
            {
                await GetBearerToken();
                var data = await _client.BooksGETAsync(Id);
                response = new Response<BookUpdateDto>
                {
                    Data = _mapper.Map<BookUpdateDto>(data),
                    Success = true,
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiExceptions<BookUpdateDto>(apiException);
            }
            return response;
        }
    }
}
