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

    }


    private void ButtonSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Movie movie = model.SelectedItem; // task declared as taskitem and initialised as selected item in gui


            model.MovieCollection.Remove(movie); //remove task from listbox
            model.MovieCollection.AddFirst(movie); //add selected task to listbox


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


                model.MovieCollection.Remove(movie); //remove selected item from listbox
                model.movieListBox.Remove(movie);


            }
            catch (Exception ed) //prevent exception
            {

            };

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
                Movie newMovie = new Movie(newId + 1, "title", "director", "genre", "release year", "availability"); //create taskitem with default values as arguments

                model.MovieCollection.AddFirst(newMovie); //add to listbox
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
     private void UpdateDelListBox()
    {

        
    }
    private void ButtonSaveDisk_Click(object sender, RoutedEventArgs e) //save to disk
    {

    }
    //categorybuttons
    private void ButtonCatSave_Click(object sender, RoutedEventArgs e) //save category button
    {

    }
    private void ButtonCatDel_Click(object sender, RoutedEventArgs e) //delete category button
    {

    }
    private void ButtonCatNew_Click(object sender, RoutedEventArgs e) //new category button
    {

    }
    private void MarkdownBtnClick(object sender, RoutedEventArgs e) //button to export tasks to markdown
    {


    }
}
