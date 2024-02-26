using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Models.Author;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public class AuthorService: BaseHttpService,IAuthorService
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        public AuthorService(IClient client, ILocalStorageService localStorage,IMapper mapper) : base(client, localStorage)
        { 
            _client = client;
            _mapper = mapper;
        }
        public async Task<Response<List<AuthorReadOnlyDto>>> Get()
        {
            Response<List<AuthorReadOnlyDto>> response;
            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsAllAsync();
                response = new Response<List<AuthorReadOnlyDto>>
                {
                    Data = data.ToList(),
                    Success = true,
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiExceptions<List<AuthorReadOnlyDto>>(apiException);
            }
            return response;
        }
        public async Task<Response<int>> Create(AuthorCreateDto author)
        {
            Response<int> response = new Response<int> { Success = true};
            try
            {
                await GetBearerToken();
                await _client.AuthorsPOSTAsync(author);
            }
            catch (ApiException apiException)
            {
                response = ConvertApiExceptions<int>(apiException);
            }
            return response;
        }
        public async Task<Response<int>> Edit(int id,AuthorUpdateDto author)
        {
            Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await _client.AuthorsPUTAsync(id,author);
            }
            catch (ApiException apiException)
            {
                response = ConvertApiExceptions<int>(apiException);
            }
            return response;
        }
        public async Task<Response<AuthorDetailsDto>> Get(int Id)
        {
            Response<AuthorDetailsDto> response;
            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsGETAsync(Id);
                response = new Response<AuthorDetailsDto>
                {
                    Data = data,
                    Success = true,
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiExceptions<AuthorDetailsDto>(apiException);
            }
            return response;
        }
        public async Task<Response<AuthorUpdateDto>> GetForUpdate(int Id)
        {
            Response<AuthorUpdateDto> response;
            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsGETAsync(Id);
                response = new Response<AuthorUpdateDto>
                {
                    Data = _mapper.Map<AuthorUpdateDto>(data),
                    Success = true,
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiExceptions<AuthorUpdateDto>(apiException);
            }
            return response;
        }

        public async Task<Response<int>> Delete(int Id)
        {
            Response<int> response = new() ;
            try
            {
                await GetBearerToken() ;
                await _client.AuthorsDELETEAsync(Id);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }
            return response;
        }
    }
} 
