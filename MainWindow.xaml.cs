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
        model = new MainWindowViewModel(); //model initialised a new MainWindowViewModel method (which contains listboxes)
        this.DataContext = model; //the data of the gui is equal to the model
        
        
        ScrollViewer sv = new ScrollViewer();
        sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        model.MovieCollection = importExport.importExport.LoadJson(@"C:\Users\Chris\OneDrive\Desktop\BICT25\D201 Advanced Programming\Assignment 1\movies.json");

        UpdateListBox();
        foreach (var item in model.movieListBox)
        {
            // Use the item as the key and a sample value (e.g., an index or a static value)
            model.movieTable[item.MovieID] = item; // You can customize the value as needed
        }
    }


    private void ButtonSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Movie movie = model.SelectedItem; // task declared as taskitem and initialised as selected item in gui


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
            Movie movie = model.SelectedItem; //task declared as taskitem, initialised as selected item from gui
            if (model.movieTable.ContainsKey(movie.MovieID))
            {
                model.MovieCollection.Remove(movie); //remove selected item from listbox
                model.movieTable.Remove(movie.MovieID);
                model.movieListBox.Remove(movie);

            }



        }
        catch (Exception ed) //prevent exception
        {

        }
    ;

    }
    private void ButtonNew_Click(object sender, RoutedEventArgs e) //new task button
    {
        {
            try
            {
                int newId = 0; // int used to determine the highest id in listbox
                foreach (Movie m in model.movieListBox.ToArray()) //iterate through listbox
                {
                    if (m.MovieID > newId) //if any category id is higher than newId....
                    {
                        newId = m.MovieID; //new id is equal to that to allow new tasks to have a higher id (will increment by one)
                    }
                }
                Movie newMovie = new Movie(newId + 1, "title", "director", "genre", 1970, "Available"); //create taskitem with default values as arguments
                if (!model.movieTable.ContainsKey(newMovie.MovieID))
                {
                    model.MovieCollection.AddLast(newMovie); //add to listbox
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
        importExport.importExport.saveJSON(model.MovieCollection, @"C:\Users\Chris\OneDrive\Desktop\BICT25\D201 Advanced Programming\Assignment 1\movies.json");
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
        catch(Exception Eid)
        {

        }

        
    }

    private void BubSortClick(object sender, RoutedEventArgs e) //button to export tasks to markdown
    {
        model.movieListBox.Clear();
       sorting.SortLinkedList.BubbleSort(model.MovieCollection);
       UpdateListBox();

    }
    private void BubMergeSortClick(object sender, RoutedEventArgs e) //button to export tasks to markdown
    {
        model.movieListBox.Clear();
      model.MovieCollection = sorting.SortLinkedList.MergeSort(model.MovieCollection);
       UpdateListBox();

    }
    private void ButtonBorrow_Click(object sender, RoutedEventArgs e) //borrow button
    {
        try 
        {
            Movie movie = model.SelectedItem;
            User user = model.user0;
            model.movieShop.BorrowMovie(movie, user);
            model.updateAll(movie);
                   

        }
        catch(Exception bor)
        {

        }

        
    }
    private void ButtonReturn_Click(object sender, RoutedEventArgs e) //borrow button
    {
        try 
        {
            Movie movie = model.SelectedItem;

            model.movieShop.ReturnMovie(movie);
            model.updateAll(movie);
                   

        }
        catch(Exception ret)
        {

        }

        
    }
}
