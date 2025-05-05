using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using domain;
namespace model
{
    public class MainWindowViewModel //main window model class
    {
        public LinkedList<Movie> MovieCollection { get; set; } 
        public Hashtable movieTable = new Hashtable();

        public ObservableCollection<Movie> movieListBox { get; set; }
        public ObservableCollection<string> borrowHistory { get; set; }
        public ObservableCollection<Movie> movieListSearch { get; set; }
        public MovieRentalSystem movieShop { get; set; }
        public Movie SelectedItem { get; set; } 
        public User user0 { get; set; }

        public void updateAll(Movie mov)
        {
            movieTable.Remove(mov);
            movieTable[mov.MovieID] = mov;
            MovieCollection.Remove(mov); 
            MovieCollection.AddLast(mov); 
            movieListBox.Remove(mov);
            movieListBox.Add(mov);
        }
        public void SearchMovie(string title)
        {
            movieListSearch.Clear();
            Movie search = new Movie();
            search.Title = title;
            Movie[] array = MovieCollection.ToArray();
            List<Movie> searchR = new List<Movie>();
            foreach (Movie mov in array)
            {
                if (search.Title.ToLower() == mov.Title.ToLower())
                {
                    {
                        Movie found = new Movie
                        {
                            MovieID = mov.MovieID,
                            Title = mov.Title,
                            Director = mov.Director,
                            Genre = mov.Genre,
                            RelYear = mov.RelYear,
                            Availability = mov.Availability

                        };
                        movieListSearch.Add(found);
                    }
                    ;
                }
            }
        }

        public void SearchMovieID(int ID)
        {
            movieListSearch.Clear();
            Movie search = new Movie();
            search.MovieID = Convert.ToString(ID);
            Movie[] array = MovieCollection.ToArray();
            List<Movie> searchR = new List<Movie>();
            foreach (Movie mov in array)
            {
                if (search.MovieID == mov.MovieID)
                {
                    {
                        Movie found = new Movie
                        {
                            MovieID = mov.MovieID,
                            Title = mov.Title,
                            Director = mov.Director,
                            Genre = mov.Genre,
                            RelYear = mov.RelYear,
                            Availability = mov.Availability

                        };
                        movieListSearch.Add(found);
                    }
                    ;
                }
            }


        }

        public MainWindowViewModel() 
        {
            MovieCollection = new LinkedList<Movie>(); 
            movieListBox = new ObservableCollection<Movie>();
            movieListSearch = new ObservableCollection<Movie>();
            movieShop = new MovieRentalSystem();
            borrowHistory = new ObservableCollection<string>();
            user0 = new User { Name = "Chris" };

        }


    }


}

