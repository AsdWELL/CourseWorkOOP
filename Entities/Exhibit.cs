using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;

namespace CourseWork
{
    public enum ExhibitFields
    {
        Title,
        Description,
        Epoch,
        Price,
        Any
    }
    
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

        private string _description;
        /// <summary>
        /// Описание экспоната
        /// </summary>
        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new EmptyArgumentException("Описание экспоната");
                _description = value;
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

        public Exhibit(string title, string description, string epoch, double price)
        {
            Title = title;
            Description = description;
            Epoch = epoch;
            Price = price;
        }

        public bool IsFieldEqulsValue(ExhibitFields field, string value)
        {
            value = value.ToLower();

            if (field == ExhibitFields.Price)
                return Price.ToString().Equals(value);

            if (field == ExhibitFields.Any)
                return Title.ToLower().Contains(value) ||
                    Description.ToLower().Contains(value) ||
                    Epoch.ToLower().Contains(value) ||
                    Price.ToString().Equals(value);

            string fieldValue = field switch
            {
                ExhibitFields.Title => Title,
                ExhibitFields.Description => Description,
                ExhibitFields.Epoch => Epoch,
            };

            return fieldValue.ToLower().Contains(value);
        }

        public override string ToString()
        {
            return $"Экспонат {Title}\n" +
                $"Описание экспоната: {Description}\n" +
                $"Эпоха экспоната: {Epoch}\n" +
                $"Стоимость экспоната: {Price} руб.";
        }
    }
}