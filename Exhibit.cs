using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace CourseWork
{
    public class Exhibit
    {
        private string _title;
        /// <summary>
        /// Название экспоната
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                if (value.Length == 0)
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
                if (value.Length == 0)
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
                if (value.Length == 0)
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

        public override string ToString()
        {
            return $"Экспонат {Title}\n" +
                $"Описание экспоната: {Description}\n" +
                $"Эпоха экспоната: {Epoch}\n" +
                $"Стоимость экспоната: {Price} руб.";
        }
    }
}