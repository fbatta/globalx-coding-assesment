namespace GlobalxCodingAssesment
{
    // an interface to write something somewhere
    public interface IWriter<T>
    {
        public void Write(T thingToWrite);
    }
}
