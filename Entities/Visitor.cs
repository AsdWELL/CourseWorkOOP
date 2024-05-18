namespace CourseWork
{
    public enum VisitorFields
    {
        Name,
        Surname,
        VisitDate,
        Any
    }

    public class Visitor
    {
        public int Id { get; set; }
        
        private string _name;

        /// <summary>
        /// Имя посетителя
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new EmptyArgumentException("Имя");
                if (value.Any(char.IsNumber))
                    throw new NumberInParamException("Имя");
                _name = value;
            }
        }

        private string _surname;

        /// <summary>
        /// Фамилия посетителя
        /// </summary>
        public string Surname
        {
            get => _surname;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new EmptyArgumentException("Фамилия");
                if (value.Any(char.IsNumber))
                    throw new NumberInParamException("Фамилия");
                _surname = value;
            }
        }

        private DateTime _visitDate;

        /// <summary>
        /// Дата последнего посещения
        /// </summary>
        public DateTime VisitDate
        {
            get => _visitDate;
            set
            {
                if (value > DateTime.Now)
                    throw new InvalidDateException();
                _visitDate = (DateTime)value;
            }
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="name">Имя посетителя</param>
        /// <param name="surname">Фамилия посетителя</param>
        /// <param name="visitDate">Дата посещения музея</param>
        public Visitor(string name, string surname, DateTime visitDate)
        {
            Name = name;
            Surname = surname;
            VisitDate = visitDate;
        }

        /// <summary>
        /// Проверяет, значение параметра <paramref name="field"/> равно <paramref name="value"/>
        /// </summary>
        /// <param name="field">Параметр экспонат</param>
        /// <param name="value">Значение параметра</param>
        /// <returns>True, если равно, иначе False</returns>
        public bool IsFieldEqulsValue(VisitorFields field, string value)
        {
            value = value.ToLower();
            if (field == VisitorFields.Any)
                return Name.ToLower() == value ||
                    Surname.ToLower() == value ||
                    VisitDate.ToString("d") == value;
            
            string fieldValue = field switch
            {
                VisitorFields.Name => Name,
                VisitorFields.Surname => Surname,
                VisitorFields.VisitDate => VisitDate.ToString("d")
            };

            if (field == VisitorFields.VisitDate)
                return fieldValue.ToLower().Contains(value);

            return fieldValue.ToLower().Equals(value);
        }
    }
}
