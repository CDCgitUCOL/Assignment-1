namespace domain 
{
    public class Movie
    {
        public int MovieID {get; set;}
        public string Title {get; set;}
        public string Director {get; set;}
        public string Genre {get; set;}
        public string RelYear {get; set;}
        public string Availability {get; set;}
        public Movie(int aMovieID, string aTitle, string aDirect, string aGenre, string aRelYear, string aAvail) //constructor method used to create a Movie item
        {
            //intitialises parameters
            MovieID = aMovieID; 
            Title = aTitle;
            Director = aDirect;
            Genre = aGenre; 
            RelYear = aRelYear;
            Availability = aAvail;
        }
        public override string ToString() // displays as a string the above, in the following format.
        {
            return $"Movie: {MovieID}, {Title}, {Director}, {Genre}, {RelYear}, {Availability} ";
        }
    }
}