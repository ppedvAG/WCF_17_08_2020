using Bogus;
using System.Collections.Generic;

namespace WcfSelfHosting.Host
{

    public class BookService : IBookService
    {
        public IEnumerable<Book> GetAllBooks()
        {
            var f = new Faker();

            yield return new Book()
            {
                Id = 1,
                ISBN = "1111",
                Title = "C# for Dummys",
                ReleaseDate = f.Date.Past(20),
                Authors = new[] { f.Name.FullName(), f.Name.FullName() }
            };

            yield return new Book()
            {
                Id = 2,
                Title = "Programming WCF",
                ISBN = "978-0-596-80548-7",
                ReleaseDate = f.Date.Past(10),
                Authors = new[] { f.Name.FullName(), f.Name.FullName() }
            };

            yield return new Book()
            {
                Id = 3,
                Title = "Cooking for Coders (How to call PizzaAPI)",
                ISBN = "99999999",
                ReleaseDate = f.Date.Past(5),
                Authors = new[] { f.Name.FullName(), f.Name.FullName() }
            };
        }
    }
}
