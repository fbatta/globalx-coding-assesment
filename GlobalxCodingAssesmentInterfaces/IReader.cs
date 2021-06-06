namespace GlobalxCodingAssesment
{
    // an interface to read something from somewhere
    public interface IReader<T>
    {
        public T Read();
    }
}
