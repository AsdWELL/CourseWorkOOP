namespace CourseWork
{
    public enum ExhibitFields
    {
        Title,
        Author,
        Epoch,
        Price,
        Any
    }
    
    /// <summary>
    /// Экспонат
    /// </summary>
    public class Exhibit
    {
        public int Id {  get; set; }
        
        private string _title;
        /// <summary>
        /// Название экспоната
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new EmptyArgumentException("Название экспоната");
                _title = value;
            }
        }

        private string _author;
        /// <summary>
        /// Автор экспоната
        /// </summary>
        public string Author
        {
            get => _author;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new EmptyArgumentException("Автор");
                _author = value;
            }
        }

        private string _epoch;
        /// <summary>
        /// Эпоха или дата создания экспоната
        /// </summary>
        public string Epoch
        {
            get => _epoch;
            set
            {
                if (string.IsNullOrEmpty(value))
                    _epoch = "Неизвестно";
                _epoch = value;
            }
        }

        private double _price;
        /// <summary>
        /// Стоимость экспоната в рублях
        /// </summary>
        public double Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new NegativePriceException();
                _price = value;
            }
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="title">Название экспоната</param>
        /// <param name="author">Автор экспоната</param>
        /// <param name="epoch">Эпоха создания</param>
        /// <param name="price">Цена</param>
        public Exhibit(string title, string author, string epoch, double price)
        {
            Title = title;
            Author = author;
            Epoch = epoch;
            Price = price;
        }

        /// <summary>
        /// Проверяет, значение параметра <paramref name="field"/> равно <paramref name="value"/>
        /// </summary>
        /// <param name="field">Параметр экспонат</param>
        /// <param name="value">Значение параметра</param>
        /// <returns>True, если равно, иначе False</returns>
        public bool IsFieldEqulsValue(ExhibitFields field, string value)
        {
            value = value.ToLower();

            if (field == ExhibitFields.Price)
                return Price.ToString().Equals(value);

            if (field == ExhibitFields.Any)
                return Title.ToLower().Contains(value) ||
                    Author.ToLower().Contains(value) ||
                    Epoch.ToLower().Contains(value) ||
                    Price.ToString().Equals(value);

            string fieldValue = field switch
            {
                ExhibitFields.Title => Title,
                ExhibitFields.Author => Author,
                ExhibitFields.Epoch => Epoch,
            };

            return fieldValue.ToLower().Equals(value);
        }
    }
}