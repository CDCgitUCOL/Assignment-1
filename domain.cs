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
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int RelYear { get; set; }
        public string Availability { get; set; }
        public Queue<User> waitingQueue;
        public Movie(int aMovieID, string aTitle, string aDirect, string aGenre, int aRelYear, string aAvail) //constructor method used to create a Movie item
        {
            //intitialises parameters
            MovieID = aMovieID;
            Title = aTitle;
            Director = aDirect;
            Genre = aGenre;
            RelYear = aRelYear;
            Availability = aAvail;
            waitingQueue = new Queue<User>();
        }
        public Movie() { } // Parameterless constructor for deserialization
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
                //return "Borrowed"; // Movie successfully borrowed
            }
            else
            {
                waitingQueue.Enqueue(user);
                Console.WriteLine($"{user.Name} could not borrow '{Title}'. Added to waiting queue.");
               // return "Borrowed"; // Movie not available, added to queue
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
            }
        }

        private void NotifyUser(User user)
        {
            // Notify the user (e.g., UI messagebox, label update)
            Console.WriteLine($"Notification: {user.Name}, the movie '{Title}' is now available for you to borrow.");
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
