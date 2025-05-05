using System.Collections;
using System.Windows.Controls;
using Assignment_1;
using domain;
using model;
using sorting;
namespace MovieApp.Tests;
public class MovieTests : IDisposable
{
    private readonly LinkedList<Movie> _movieCollection;
    private readonly Hashtable _movieTable;
    private readonly MainWindowViewModel model;
    public MovieTests()
    {
        _movieCollection = new LinkedList<Movie>();
        _movieTable = new Hashtable();
        model = new MainWindowViewModel();
    }
    internal void AddMovie(LinkedList<Movie> movieCol, Hashtable movTable, Movie mov) //method follows same logic used when adding movie in main program
    {
        int newId = 0;
        foreach (Movie m in movieCol.ToArray())
        {
            if (Convert.ToInt32(m.MovieID) > newId)
            {
                newId = Convert.ToInt32(m.MovieID);
            }
        }

        if (!movTable.ContainsKey(mov.MovieID))
        {
            movieCol.AddLast(mov);
            movTable[mov.MovieID] = mov;
        }
    }

    [Fact]
    public void AddMovie_ShouldAddMovie_WhenValidMovie() //adding a valid movie to  the movie linked list and hash table should work
    {
        var movie = new Movie { MovieID = "1", Title = "Inception", Genre = "Sci-Fi" };
        AddMovie(_movieCollection,_movieTable,movie);
        var result = _movieCollection.Last;
        Assert.NotNull(result);
        Assert.Equal("Inception", result.Value.Title); // the expected output: "Inception" is equal to result.Value.Title
    }
    [Fact]
    public void AddMovie_ShouldNotAdd_WhenDuplicateMovieId()
    {
        var movie1 = new Movie { MovieID = "1", Title = "Inception", Genre = "Sci-Fi" };
        var movie2 = new Movie { MovieID = "1", Title = "Interstellar", Genre = "Sci-Fi" };

        AddMovie(_movieCollection, _movieTable, movie1);
        AddMovie(_movieCollection, _movieTable, movie2);

        Assert.Single(_movieCollection);
    }
 [Fact]
    public void Search_return_result() //Test searching by title returns correct result
    {
        var movie1 = new Movie { MovieID = "1", Title = "Inception", Genre = "Sci-Fi" };
        
        model.MovieCollection.AddLast(movie1);
        model.SearchMovie("Inception");
        string result = model.movieListSearch[0].Title;

        Assert.Equal(model.MovieCollection.Last.Value.Title, result );
    }

     [Fact]
    public void SearchID_return_result()  //Test searching by ID returns correct result
    {
        var movie1 = new Movie { MovieID = "1", Title = "Inception", Genre = "Sci-Fi" };
        
        model.MovieCollection.AddLast(movie1);
        model.SearchMovieID(1);
        string result = model.movieListSearch[0].MovieID;

        Assert.Equal(model.MovieCollection.Last.Value.MovieID, result );
    }


    [Fact]
    public void BubbleSort_SortsMoviesByTitle()
    {
        // Arrange
        var linkedList = new LinkedList<Movie>();
        var movie1 = new Movie { MovieID = "1", Title = "Avengers", Genre = "Fantasy" };
        var movie2 = new Movie { MovieID = "2", Title = "Back to the Future", Genre = "Fantasy" };
        var movie3 = new Movie { MovieID = "3", Title = "Casablanca", Genre = "Romance" };
        linkedList.AddLast(movie1);
        linkedList.AddLast(movie2);
        linkedList.AddLast(movie3);


        // Act
        SortLinkedList.BubbleSort(linkedList);

        // Assert
        var sortedTitles = new List<string>();
        foreach (var movie in linkedList)
        {
            sortedTitles.Add(movie.Title);
        }

        // Check if the titles are sorted
        Assert.Equal("Avengers", sortedTitles[0]);
        Assert.Equal("Back to the Future", sortedTitles[1]);
        Assert.Equal("Casablanca", sortedTitles[2]);
    }
    [Fact]
    public void IntroSort_SortsMoviesByReleaseYear()
    {
        // Arrange
        var linkedList = new LinkedList<Movie>();
        var movie1 = new Movie { MovieID = "1", Title = "Back to the Future", Genre = "Fantasy", RelYear = 1985 };
        var movie2 = new Movie { MovieID = "2", Title = "Avengers", Genre = "Fantasy", RelYear = 2012};
        var movie3 = new Movie { MovieID = "3", Title = "Casablanca", Genre = "Romance", RelYear = 1942 };
        linkedList.AddLast(movie1);
        linkedList.AddLast(movie2);
        linkedList.AddLast(movie3);

        // Act
        var sortedList = SortLinkedList.IntroSort(linkedList);

        // Assert
        var sortedMovies = new List<Movie>(sortedList); //Expected outputs
        Assert.Equal(2012, sortedMovies[0].RelYear); //2012
        Assert.Equal(1985, sortedMovies[1].RelYear); //1985
        Assert.Equal(1942, sortedMovies[2].RelYear); //1942
       
    }
     [Fact]
        public void BorrowMovie_WhenAvailable_ChangesAvailabilityToBorrowed()
        {
            // Arrange
            var movie = new Movie(1, "Inception", "Christopher Nolan", "Sci-Fi", 2010, "Available");
            var user = new User("Alice");

            // Act
            movie.Borrow(user);

            // Assert
            Assert.Equal("Borrowed", movie.Availability);
        }

        [Fact]
        public void BorrowMovie_WhenNotAvailable_AddsUserToWaitingQueue()
        {
            // Arrange
            var movie = new Movie(2, "Star Wars", "George Lucas", "Space Fantasy", 1977, "Available");
            var user1 = new User("Alice");
            var user2 = new User("Bob");

            // Act
            movie.Borrow(user1); // First user tries to borrow
            movie.Borrow(user2); // Second user tries to borrow

            // Assert
            Assert.Equal(1, movie.waitingQueue.Count);
            Assert.Equal(user2, movie.waitingQueue.Dequeue()); // Check if the second user is in the queue
        }

        [Fact]
        public void ReturnMovie_WhenCalled_AssignsMovietoNextinQueue()
        {
            // Arrange
            var movie = new Movie(1, "Inception", "Christopher Nolan", "Sci-Fi", 2010, "Borrowed");
            var user = new User("Alice");
            movie.Borrow(user); // Borrow the movie first

            // Act
            movie.ReturnMovie();

            // Assert
            Assert.Equal("Borrowed", movie.Availability); // Because Alice is in the queue, the movie is assigned to her causing the availablity to be unchanged
        }
        [Fact]
        public void BorrowMovie_WhenMovieIsNull_ThrowsArgumentNullException() //throws null exception when movie collection is empty
        {
            // Arrange
            var rentalSystem = new MovieRentalSystem();
            var user = new User("Alice");

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => rentalSystem.BorrowMovie(null, user));
        }

 

        [Fact]
        public void ReturnMovie_WhenMovieIsNull_ThrowsNullReferenceException()
        {
            // Arrange
            var rentalSystem = new MovieRentalSystem();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => rentalSystem.ReturnMovie(null));
        }

 [Fact]
     
    public void Dispose()
    {
        // throw new NotImplementedException();
    }
}
