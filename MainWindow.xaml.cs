using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using domain;
using model;
using sorting;

namespace Assignment_1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    MainWindowViewModel model; //declare model as type(class) MainWindowViewModel


    public MainWindow()
    {
        InitializeComponent();
        model = new MainWindowViewModel(); //model initialised a new MainWindowViewModel method 
        this.DataContext = model; //the data of the gui is equal to the model

        ScrollViewer sv = new ScrollViewer();
        sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        model.MovieCollection = importExport.importExport.LoadJson(@"movies.json");

        UpdateListBox();
        foreach (var item in model.movieListBox)
        {
            // Use the item as the key and a sample value
            model.movieTable[item.MovieID] = item; 
        }
    }

    private void seeHistory_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bool history = false;
            model.borrowHistory.Clear();
            foreach (string name in model.SelectedItem.borrowUser)
            {
                model.borrowHistory.Add(name);
                history = true;
            }

            if(history == false)
            {notify.Text = $"Movie has not been previously borrowed";}
        }

        catch (Exception test)
        {
            notify.Text = $"You must select a movie first.";
        }
    }

    private void ButtonSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Movie movie = model.SelectedItem; 


            if (model.movieTable.ContainsKey(movie.MovieID))
            {
                model.updateAll(movie);


            }
        }


        catch (Exception fe) //prevent crashing
        {

        }

    }
    private void ButtonDel_Click(object sender, RoutedEventArgs e) //delete task button
    {
        try
        {
            Movie movie = model.SelectedItem; 
            if (model.movieTable.ContainsKey(movie.MovieID))
            {
                model.MovieCollection.Remove(movie); //remove selected item
                model.movieTable.Remove(movie.MovieID);
                model.movieListBox.Remove(movie);

            }



        }
        catch (Exception ed) //prevent exception
        {

        }
    ;

    }
    private void ButtonNew_Click(object sender, RoutedEventArgs e) //new movie button
    {
        {
            try
            {
                int newId = 0; 
                foreach (Movie m in model.movieListBox.ToArray()) 
                {
                    if (Convert.ToInt32(m.MovieID) > newId) 
                    {
                        newId = Convert.ToInt32(m.MovieID);
                    }
                }
                Movie newMovie = new Movie(newId + 1, "title", "director", "genre", 1970, "Available"); 
                if (!model.movieTable.ContainsKey(newMovie.MovieID))
                {
                    model.MovieCollection.AddLast(newMovie); 
                    model.movieTable[newMovie.MovieID] = newMovie;
                }
                UpdateListBox();
            }
            catch (Exception efg) //prevent exceptions
            {

            }
        }
    }
    private void UpdateListBox()
    {

        foreach (var movie in model.MovieCollection)
        {
            if (!model.movieListBox.Any(m => m.MovieID == movie.MovieID))
            {
                model.movieListBox.Add(movie);
            }


        }
    }

    private void ButtonSaveDisk_Click(object sender, RoutedEventArgs e) //save to disk
    {
        importExport.importExport.saveJSON(model.MovieCollection, "movies.json");
    }
    private void ButtonSearchTitle_Click(object sender, RoutedEventArgs e) //search button
    {
        string searchText = SearchTitle.Text;
        model.SearchMovie(searchText);
    }
    private void ButtonSearchID_Click(object sender, RoutedEventArgs e) //search button
    {
        try
        {
            int searchID = Convert.ToInt32(SearchID.Text);
            model.SearchMovieID(searchID);
        }
        catch (Exception Eid)
        {

        }


    }

    private void BubSortClick(object sender, RoutedEventArgs e) 
    {
        model.movieListBox.Clear();
        sorting.SortLinkedList.BubbleSort(model.MovieCollection);
        UpdateListBox();

    }
    private void BubMergeSortClick(object sender, RoutedEventArgs e) 
    {
        model.movieListBox.Clear();
        model.MovieCollection = sorting.SortLinkedList.MergeSort(model.MovieCollection);
        UpdateListBox();

    }
    private void ButtonBorrow_Click(object sender, RoutedEventArgs e) 
    {
        try
        {
            Movie movie = model.SelectedItem;
            User user = model.user0;
            string avail = movie.Availability;
            model.movieShop.BorrowMovie(movie, user);
            model.updateAll(movie);
            if (avail != movie.Availability)
            {
                notify.Text = $"{user.Name} has borrowed '{movie.Title}'.";
                movie.borrowUser.Add(user.Name);

            }
            else { notify.Text = $"'{movie.Title}' is unavailable. Added to waiting queue. Movie will be loaned to you once available."; }

        }
        catch (Exception bor)
        {

        }


    }
    private void ButtonReturn_Click(object sender, RoutedEventArgs e) 
    {
        try
        {
            Movie movie = model.SelectedItem;
            string avail = movie.Availability;
            model.movieShop.ReturnMovie(movie);
            model.updateAll(movie);
            if (avail != movie.Availability)
            { notify.Text = $"'{movie.Title}' returned succesfully."; }
            else { notify.Text = $"Cannot return '{movie.Title}', movie is not loaned to you."; }


        }
        catch (Exception ret)
        {

        }


    }
}
