using domain;
using model;
namespace sorting
{
    public class SortLinkedList
    {
        public static void BubbleSort(LinkedList<Movie> linkedList)
        {
            if (linkedList.Count <= 1)
                return; // No need to sort if the list has 0 or 1 element

            bool swapped;
            do
            {
                swapped = false;
                var current = linkedList.First;

                while (current != null && current.Next != null)
                {
                    var next = current.Next;

                    // Compare current movie title with the next movie title
                    if (string.Compare(current.Value.Title, next.Value.Title) > 0)
                    {
                        // Swap values
                        Movie temp = current.Value;
                        current.Value = next.Value;
                        next.Value = temp;

                        swapped = true; // A swap occurred
                    }

                    current = next; // Move to the next node
                }
            } while (swapped); // Repeat until no swaps occur
        }

        public static LinkedList<Movie> IntroSort(LinkedList<Movie> list)
    {
        if (list == null || list.Count <= 1)
            return list;

        // Convert to List<Movie>
        var tempList = list.ToList();

        // Use List<T>.Sort with custom comparison (descending)
        tempList.Sort((a, b) => b.RelYear.CompareTo(a.RelYear));

        // Rebuild LinkedList
        var sortedLinkedList = new LinkedList<Movie>();
        foreach (var movie in tempList)
        {
            sortedLinkedList.AddLast(movie);
        }

        return sortedLinkedList;
    }
}
}