using System.Collections.ObjectModel;
using domain;
namespace model
{
    public class MainWindowViewModel //main window model class
    {
        public LinkedList<Movie> MovieCollection { get; set; } //Listbox for tasks

        public Movie SelectedItem { get; set; } //selected value for task in gui

        public MainWindowViewModel() //constructor method used to assign values to the above listboxes
        {
            MovieCollection = new LinkedList<Movie>(); //Listbox items is equal to new observable collection (listbox in gui)
            
        }


    }


}

