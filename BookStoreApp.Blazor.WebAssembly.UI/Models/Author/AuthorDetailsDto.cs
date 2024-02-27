using BookStoreApp.Blazor.WebAssembly.UI.Models.Book;

namespace BookStoreApp.Blazor.WebAssembly.UI.Models.Author;

public class AuthorDetailsDto : AuthorReadOnlyDto
{
    public List<BookReadOnlyDto> Books { get; set; }
}
