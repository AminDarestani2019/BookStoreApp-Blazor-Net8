using BookStoreApp.Blazor.Server.UI.Models.Book;

namespace BookStoreApp.Blazor.Server.UI.Models.Author;

public class AuthorDetailsDto : AuthorReadOnlyDto
{
    public List<BookReadOnlyDto> Books { get; set; }
}
