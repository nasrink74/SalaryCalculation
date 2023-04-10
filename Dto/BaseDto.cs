namespace Dto
{
    public class BaseDto<T>
    {
        public T Id { get; set; }
    }
    public class BaseDto: BaseDto<int>
    {
        public int Id { get; set; }
    }
}
