using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using domain;
namespace model
{
    public class MainWindowViewModel //main window model class
    {
        public LinkedList<Movie> MovieCollection { get; set; } //Listbox for tasks
        public Hashtable movieTable = new Hashtable();

        public ObservableCollection<Movie> movieListBox { get; set; }
        public ObservableCollection<Movie> movieListSearch { get; set; }
        public MovieRentalSystem movieShop { get; set; }
        public Movie SelectedItem { get; set; } //selected value for task in gui
        public User user0 { get; set; }

        public void updateAll(Movie mov)
        {
            movieTable.Remove(mov);
            movieTable[mov.MovieID] = mov;
            MovieCollection.Remove(mov); //remove task from listbox
            MovieCollection.AddLast(mov); //add to listbox
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
                if (search.Title == mov.Title)
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
            search.MovieID = ID;
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

        public MainWindowViewModel() //constructor method used to assign values to the above listboxes
        {
            MovieCollection = new LinkedList<Movie>(); //Listbox items is equal to new observable collection (listbox in gui)
            movieListBox = new ObservableCollection<Movie>();
            movieListSearch = new ObservableCollection<Movie>();
            movieShop = new MovieRentalSystem();
            user0 = new User { Name = "Chris" };

        }


    }


}

