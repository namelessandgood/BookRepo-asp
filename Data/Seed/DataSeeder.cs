using Microsoft.AspNetCore.Identity;
using BooksArchivingSystem.Data.Models;

namespace BooksArchivingSystem.Data.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            await SeedRolesAsync(roleManager);
            
            // Seed Users
            await SeedUsersAsync(userManager);
            
            // Seed Categories
            await SeedCategoriesAsync(context);
            
            // Seed Authors
            await SeedAuthorsAsync(context);
            
            // Seed Books
            await SeedBooksAsync(context);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Librarian", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            // Admin User
            if (await userManager.FindByEmailAsync("admin@bookarchive.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@bookarchive.com",
                    Email = "admin@bookarchive.com",
                    FirstName = "System",
                    LastName = "Administrator",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Librarian User
            if (await userManager.FindByEmailAsync("librarian@bookarchive.com") == null)
            {
                var librarianUser = new ApplicationUser
                {
                    UserName = "librarian@bookarchive.com",
                    Email = "librarian@bookarchive.com",
                    FirstName = "Jane",
                    LastName = "Librarian",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(librarianUser, "Librarian123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(librarianUser, "Librarian");
                }
            }

            // Regular User
            if (await userManager.FindByEmailAsync("user@bookarchive.com") == null)
            {
                var regularUser = new ApplicationUser
                {
                    UserName = "user@bookarchive.com",
                    Email = "user@bookarchive.com",
                    FirstName = "John",
                    LastName = "Reader",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(regularUser, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularUser, "User");
                }
            }
        }

        private static async Task SeedCategoriesAsync(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Fiction", Description = "Fictional literature and novels" },
                    new Category { Name = "Non-Fiction", Description = "Non-fictional books and documentaries" },
                    new Category { Name = "Science", Description = "Scientific books and research" },
                    new Category { Name = "Technology", Description = "Technology and computer science books" },
                    new Category { Name = "History", Description = "Historical books and documentaries" },
                    new Category { Name = "Biography", Description = "Biographical books and memoirs" },
                    new Category { Name = "Self-Help", Description = "Self-improvement and motivational books" },
                    new Category { Name = "Children", Description = "Children's books and educational materials" }
                };

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedAuthorsAsync(ApplicationDbContext context)
        {
            if (!context.Authors.Any())
            {
                var authors = new[]
                {
                    new Author 
                    { 
                        Name = "J.K. Rowling", 
                        Biography = "British author best known for the Harry Potter series",
                        BirthDate = new DateTime(1965, 7, 31),
                        Nationality = "British"
                    },
                    new Author 
                    { 
                        Name = "George Orwell", 
                        Biography = "English novelist and journalist, known for 1984 and Animal Farm",
                        BirthDate = new DateTime(1903, 6, 25),
                        DeathDate = new DateTime(1950, 1, 21),
                        Nationality = "British"
                    },
                    new Author 
                    { 
                        Name = "Stephen King", 
                        Biography = "American author of horror, supernatural fiction, suspense, crime, science-fiction, and fantasy novels",
                        BirthDate = new DateTime(1947, 9, 21),
                        Nationality = "American"
                    },
                    new Author 
                    { 
                        Name = "Agatha Christie", 
                        Biography = "English writer known for her detective novels",
                        BirthDate = new DateTime(1890, 9, 15),
                        DeathDate = new DateTime(1976, 1, 12),
                        Nationality = "British"
                    },
                    new Author 
                    { 
                        Name = "Isaac Asimov", 
                        Biography = "American writer and professor of biochemistry, known for science fiction works",
                        BirthDate = new DateTime(1920, 1, 2),
                        DeathDate = new DateTime(1992, 4, 6),
                        Nationality = "American"
                    },
                    new Author 
                    { 
                        Name = "Maya Angelou", 
                        Biography = "American memoirist, poet, and civil rights activist",
                        BirthDate = new DateTime(1928, 4, 4),
                        DeathDate = new DateTime(2014, 5, 28),
                        Nationality = "American"
                    }
                };

                context.Authors.AddRange(authors);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedBooksAsync(ApplicationDbContext context)
        {
            if (!context.Books.Any())
            {
                var books = new[]
                {
                    new Book 
                    { 
                        Title = "Harry Potter and the Philosopher's Stone", 
                        ISBN = "9780747532699",
                        Publisher = "Bloomsbury",
                        PublicationDate = new DateTime(1997, 6, 26),
                        Description = "The first book in the Harry Potter series",
                        CategoryId = 1, // Fiction
                        Pages = 223,
                        Language = "English"
                    },
                    new Book 
                    { 
                        Title = "1984", 
                        ISBN = "9780451524935",
                        Publisher = "Secker & Warburg",
                        PublicationDate = new DateTime(1949, 6, 8),
                        Description = "A dystopian social science fiction novel",
                        CategoryId = 1, // Fiction
                        Pages = 328,
                        Language = "English"
                    },
                    new Book 
                    { 
                        Title = "The Shining", 
                        ISBN = "9780385121675",
                        Publisher = "Doubleday",
                        PublicationDate = new DateTime(1977, 1, 28),
                        Description = "A horror novel about a haunted hotel",
                        CategoryId = 1, // Fiction
                        Pages = 447,
                        Language = "English"
                    },
                    new Book 
                    { 
                        Title = "Murder on the Orient Express", 
                        ISBN = "9780007119318",
                        Publisher = "Collins Crime Club",
                        PublicationDate = new DateTime(1934, 1, 1),
                        Description = "A detective novel featuring Hercule Poirot",
                        CategoryId = 1, // Fiction
                        Pages = 256,
                        Language = "English"
                    },
                    new Book 
                    { 
                        Title = "Foundation", 
                        ISBN = "9780553293357",
                        Publisher = "Gnome Press",
                        PublicationDate = new DateTime(1951, 5, 1),
                        Description = "A science fiction novel about psychohistory",
                        CategoryId = 3, // Science
                        Pages = 244,
                        Language = "English"
                    },
                    new Book 
                    { 
                        Title = "I Know Why the Caged Bird Sings", 
                        ISBN = "9780345514400",
                        Publisher = "Random House",
                        PublicationDate = new DateTime(1969, 1, 1),
                        Description = "An autobiographical work by Maya Angelou",
                        CategoryId = 6, // Biography
                        Pages = 281,
                        Language = "English"
                    }
                };

                context.Books.AddRange(books);
                await context.SaveChangesAsync();

                // Add book-author relationships
                var bookAuthors = new[]
                {
                    new BookAuthor { BookId = 1, AuthorId = 1 }, // Harry Potter - J.K. Rowling
                    new BookAuthor { BookId = 2, AuthorId = 2 }, // 1984 - George Orwell
                    new BookAuthor { BookId = 3, AuthorId = 3 }, // The Shining - Stephen King
                    new BookAuthor { BookId = 4, AuthorId = 4 }, // Murder on the Orient Express - Agatha Christie
                    new BookAuthor { BookId = 5, AuthorId = 5 }, // Foundation - Isaac Asimov
                    new BookAuthor { BookId = 6, AuthorId = 6 }  // I Know Why the Caged Bird Sings - Maya Angelou
                };

                context.BookAuthors.AddRange(bookAuthors);
                await context.SaveChangesAsync();
            }
        }
    }
}
