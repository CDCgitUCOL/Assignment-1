using System.Collections.ObjectModel;
using System.Windows;
using model;
namespace domain
{
    public class MovieRentalSystem
    {
        public void BorrowMovie(Movie movie, User user)
        {
            movie.Borrow(user);
        }

        public void ReturnMovie(Movie movie)
        {
            movie.ReturnMovie();
        }
    }
    public class Movie
    {
        public string MovieID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int RelYear { get; set; }
        public string Availability { get; set; }
        public Queue<User> waitingQueue;
        public ObservableCollection<string> borrowUser;
        public Movie(int aMovieID, string aTitle, string aDirect, string aGenre, int aRelYear, string aAvail) 
        {
            //intitialises parameters
            MovieID = Convert.ToString(aMovieID);
            Title = aTitle;
            Director = aDirect;
            Genre = aGenre;
            RelYear = aRelYear;
            Availability = aAvail;
            waitingQueue = new Queue<User>();
            borrowUser = new ObservableCollection<string>();
        }
        public Movie() {  waitingQueue = new Queue<User>();
        borrowUser = new ObservableCollection<string>();} // Parameterless constructor for deserialization
        public override string ToString() // displays as a string the above, in the following format.
        {
            return $"Movie: {MovieID}, {Title}, {Director}, {Genre}, {RelYear}, {Availability} ";
        }
        public void Borrow(User user)
        {
            if (Availability == "Available")
            {
                Availability = "Borrowed";
                Console.WriteLine($"{user.Name} has borrowed '{Title}'.");
            }
            else
            {
                waitingQueue.Enqueue(user);
                Console.WriteLine($"{user.Name} could not borrow '{Title}'. Added to waiting queue.");
               
            }
        }
        public void ReturnMovie()
        {
            Availability = "Available";
            Console.WriteLine($"The movie '{Title}' has been returned and is now available.");

            if (waitingQueue.Count > 0)
            {
                User nextUser = waitingQueue.Dequeue();
                NotifyUser(nextUser);
                Borrow(nextUser);
            }
        }

        private void NotifyUser(User user)
        {
            MessageBox.Show($"Notification: {user.Name}, the movie '{Title}' is available and will now be loaned to you.", "Movie");
        }
        public class MovieRentalSystem
        {
            public void BorrowMovie(Movie movie, User user)
            {
                movie.Borrow(user);
            }

            public void ReturnMovie(Movie movie)
            {
                movie.ReturnMovie();
            }
        }
    }
}

public class User
{
    public string Name { get; set; }

    public User(string name)
    {
        Name = name;
    }
  public User() { } // Parameterless constructor for deserialization
}
