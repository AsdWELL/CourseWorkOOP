using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        public Visitor(string name, string surname, DateTime visitDate)
        {
            Name = name;
            Surname = surname;
            VisitDate = visitDate;
        }

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
            return fieldValue.ToLower().Equals(value);
        }

        public override string ToString()
        {
            return $"Посетитель {Name} {Surname}\nПоследняя дата визита {VisitDate:d}";
        }
    }
}
