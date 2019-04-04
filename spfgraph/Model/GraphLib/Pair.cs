namespace Model {
    public class Pair<T, V> {
        public T First { get; set; }
        public V Second { get; set; }

        public Pair(T item1, V item2) {
            First = item1;
            Second = item2;
        }

    }
}
