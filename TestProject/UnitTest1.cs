using System.Numerics;
using dotnetapp.Models;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Net;

namespace TestProject
{
    public class Tests
    {
        private ApplicationDbContext _context;

        private HttpClient _httpClient;


        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080/");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

// test to check if the endpoint is returning the expected status code 
        [Test]
        public async Task Test_GetMoviews_EndPoint_Status()
        {
            // Send an HTTP GET request to the API endpoint.
            HttpResponseMessage response = await _httpClient.GetAsync("api/Movie");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseBody);
        }

// // test to check whether the migrtion is applied to the database
//         [Test]
//         public void Test_Migration_Exists()
//         {
//             string folderPath = @"/home/coder/project/workspace/dotnetapp/Migrations/";
//             //string folderPath = @"D:\Dotnet_Weekly_2_Solutions\Movie Review\dotnetapp\dotnetapp\Migrations\"; 
//             bool folderExists = Directory.Exists(folderPath);

//             Assert.IsTrue(folderExists, "The folder does not exist.");

//             if (folderExists)
//             {
//                 string[] files = Directory.GetFiles(folderPath);
//                 Assert.IsTrue(files.Length > 0, "No files found in the folder.");
//             }
//         }

//test to check if the movie is added to the database by calling the AddMovie method in the MovieController
        [Test]
        public void AddMovie_MovieControllerr_AddsTo_DB()
        {
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string controllerTypeName = "dotnetapp.Controllers.MovieController";
            string typeName = "dotnetapp.Models.Movie";

            Type controllerType = assembly.GetType(controllerTypeName);
            Type controllerType2 = assembly.GetType(typeName);

            MethodInfo method = controllerType.GetMethod("AddMovie", new[] { controllerType2 });

            if (method != null)
            {

                var teamData = new Dictionary<string, object>
                    {
                        { "Title", "demo2" },
                        { "Description", "demo" },
                        { "ReleaseDate", new DateTime(2023, 8, 22) },
                        { "Genre", "demo" }
                    };
                var team = Activator.CreateInstance(controllerType2);
                foreach (var kvp in teamData)
                {
                    var propertyInfo = controllerType2.GetProperty(kvp.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(team, kvp.Value);
                    }
                }
                var controller = Activator.CreateInstance(controllerType, _context);
                var result = method.Invoke(controller, new object[] { team });
                Assert.IsNotNull(result);
                Type CourseType = assembly.GetType(typeName);

                PropertyInfo propertyInfo1 = CourseType.GetProperty("MovieID");
                Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
                var propertyInfo2 = contextType.GetProperty("Movies");
                var courses = propertyInfo2.GetValue(_context, null);
                if (courses is IEnumerable<Movie> courseList)
                {
                    var res = courseList.AsEnumerable().FirstOrDefault(r => (int)propertyInfo1.GetValue(r) == 1);
                    if (res != null)
                    {
                        Console.WriteLine(res.ToString());
                        Assert.IsNotNull(res);
                    }
                    else
                    {
                        Assert.Fail();
                    }
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        // test to check if the movie is returned by the GetMovies method in the MovieController as list

        [Test]
        public void GetMovies_MovieController_Returns_List()
        {
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string modelType = "dotnetapp.Models.Movie";
            string controllerTypeName = "dotnetapp.Controllers.MovieController";
            Type controllerType = assembly.GetType(controllerTypeName);
            Type modelTypeName = assembly.GetType(modelType);

            var teamData = new Dictionary<string, object>
                    {
                        { "Title", "demo2" },
                        { "Description", "demo" },
                        { "ReleaseDate", new DateTime(2023, 8, 22) },
                        { "Genre", "demo" }
                    };
            var team = Activator.CreateInstance(modelTypeName);
            foreach (var kvp in teamData)
            {
                var propertyInfo = modelTypeName.GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(team, kvp.Value);
                }
            }

            _context.Movies.Add((Movie)team);

            MethodInfo method = controllerType.GetMethod("GetMovies");

            if (method != null)
            {
                var controller = Activator.CreateInstance(controllerType, _context);
                var result = method.Invoke(controller, null);
                Assert.IsNotNull(result);

                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);

                var items = okResult.Value as dynamic;
                Assert.IsNotNull(items);
                Console.WriteLine(items);
                var items1 = okResult.Value as List<Movie>; // Assuming GetMovies returns a list of movies
                Assert.IsNotNull(items1);

                
                if (items1.Count > 0)
                {
                    var firstItem = items1[0];
                    var titleProperty = firstItem.GetType().GetProperty("Title");

                    if (titleProperty != null)
                    {
                        string firstItemTitle = (string)titleProperty.GetValue(firstItem);
                        Assert.AreEqual("demo2", firstItemTitle);
                    }
                    else
                    {
                        Assert.Fail();
                    }
                }
                else { Assert.Fail(); }

                Console.WriteLine(items);
            }
            else
            {
                Assert.Fail();
            }
        }

// test to check if the ApplicationDbContext contains the DbSet for the Movie model
        [Test]
        public void ApplicationDbContext_ContainsDbSet_Movie()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type MovieType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Movie");
            if (MovieType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Movies");
            if (propertyInfo == null)
            {
                Assert.Fail("Movies property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(MovieType), propertyInfo.PropertyType);
            }
        }

        // test to check if the Movie model exists

        [Test]
        public void Movie_Models_ClassExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Movie";
            Assembly assembly = Assembly.Load(assemblyName);
            Type MovieType = assembly.GetType(typeName);
            Assert.IsNotNull(MovieType);
        }

// test to check if the movie model contains the MovieID property of type int
        [Test]
        public void Movie_MovieID_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Movie";
            Assembly assembly = Assembly.Load(assemblyName);
            Type MovieType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = MovieType.GetProperty("MovieID");
            Assert.IsNotNull(propertyInfo, "Property MovieID does not exist in Movie class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property MovieID in Movie class is not of type int");
        }

// test to check if the movie model contains the Title property of type string
        [Test]
        public void Movie_Title_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Movie";
            Assembly assembly = Assembly.Load(assemblyName);
            Type MovieType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = MovieType.GetProperty("Title");
            Assert.IsNotNull(propertyInfo, "Property Title does not exist in Movie class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Title in Movie class is not of type string");
        }

// test to check if the movie model contains the Description property of type string
        [Test]
        public void Movie_Description_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Movie";
            Assembly assembly = Assembly.Load(assemblyName);
            Type MovieType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = MovieType.GetProperty("Description");
            Assert.IsNotNull(propertyInfo, "Property Description does not exist in Movie class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Description in Movie class is not of type string");
        }

// test to check if the movie model contains the ReleaseDate property of type DateTime
        [Test]
        public void Movie_ReleaseDate_PropertyExists_ReturnExpectedDataTypes_DateTime()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Movie";
            Assembly assembly = Assembly.Load(assemblyName);
            Type MovieType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = MovieType.GetProperty("ReleaseDate");
            Assert.IsNotNull(propertyInfo, "Property ReleaseDate does not exist in Movie class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(DateTime), expectedType, "Property ReleaseDate in Movie class is not of type DateTime");
        }

// test to check if the movie model contains the Genre property of type string
        [Test]
        public void Movie_Genre_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Movie";
            Assembly assembly = Assembly.Load(assemblyName);
            Type MovieType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = MovieType.GetProperty("Genre");
            Assert.IsNotNull(propertyInfo, "Property Genre does not exist in Movie class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Genre in Movie class is not of type string");
        }

// test to check if the MovieController class exists
        [Test]
        public void MovieController_Controllers_ClassExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.MovieController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type MovieControllerType = assembly.GetType(typeName);
            Assert.IsNotNull(MovieControllerType);
        }

        // Test to Check MovieController Controllers Method AddMovie Exists
        [Test]
        public void MovieController_AddMovie_MethodExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.MovieController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ShoppingCartControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = ShoppingCartControllerType.GetMethod("AddMovie");
            Assert.IsNotNull(methodInfo, "Method AddMovie does not exist in MovieController class");
        }
        // Test to Check MovieController Controllers Method AddMovie Exists
        [Test]
        public void MovieController_GetMovies_MethodExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.MovieController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ShoppingCartControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = ShoppingCartControllerType.GetMethod("GetMovies");
            Assert.IsNotNull(methodInfo, "Method GetMovies does not exist in MovieController class");
        }

    }
}
