namespace spfgraph.Model.Data {
    public class Pair<T> {
        public T First { get; set; }
        public T Second { get; set; }

        public Pair(T item1, T item2) {
            First = item1;
            Second = item2;
        }

    }
}
