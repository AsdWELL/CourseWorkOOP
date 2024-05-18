namespace CourseWork
{
    public class EmptyArgumentException : Exception
    {
        public EmptyArgumentException(string paramName) : base($"{paramName} не может быть пустым") { }
    }

    public class NumberInParamException : Exception
    {
        public NumberInParamException(string paramName) : base($"{paramName} не может содержать цифр") { }
    }

    public class InvalidDateException : Exception
    {
        public InvalidDateException() : base("Введенная дата не может быть позднее текущей даты") { }
    }

    public class NegativePriceException : Exception
    {
        public NegativePriceException() : base("Цена экспоната не может быть отрицательной") { }
    }

    public class ItemNotInCollectionException : Exception
    {
        public ItemNotInCollectionException() : base("В коллекции нет указанного элемента") { }
    }
}