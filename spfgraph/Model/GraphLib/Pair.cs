namespace Model {
    public class Pair<T> {
        public T First { get; set; }
        public T Second { get; set; }

        public Pair(T item1, T item2) {
            First = item1;
            Second = item2;
        }

    }

    public class Pair<T, V> {
        public T First { get; set; }
        public V Second { get; set; }

        public Pair(T a, V b) {
            First = a;
            Second = b;
        }
    }

    //public class Pair<T, V>
    //    where T : class, new()
    //    where V : struct {

    //    public T First { get; set; }
    //    public V Second { get; set; }

    //    public Pair(T item1, V item2) {
    //        First = item1;
    //        Second = item2;
    //    }
    //}



}
