using AutoMapper;
using BookStoreApp.Blazor.WebAssembly.UI.Models.Author;
using BookStoreApp.Blazor.WebAssembly.UI.Models.Book;

namespace BookStoreApp.Blazor.WebAssembly.UI.Configuration
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorDetailsDto, AuthorUpdateDto>().ReverseMap();
            CreateMap<BookDetailsDto, BookUpdateDto>().ReverseMap();
        }
    }
}
