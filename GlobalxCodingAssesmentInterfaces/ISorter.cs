namespace GlobalxCodingAssesment
{
    // interface to sort some generic element
    public interface ISorter<T>
    {
        public T Sort();

        public T GetUnsortedList();

        public void Write();
    }
}
