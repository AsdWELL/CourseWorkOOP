using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows.Automation.Peers;

namespace CourseWork
{
    public delegate void AddNewItemHandler<T>(T item);

    public delegate void RemoveItemHandler(int index);

    public delegate void ChangeItemHandler<T>(int index, T newItem);

    public abstract class JsonSerializableList<T> : List<T>
    {
        public event AddNewItemHandler<T> OnAdd;

        public event RemoveItemHandler OnRemove;

        public event ChangeItemHandler<T> OnChange;
        
        protected string path;

        /// <summary>
        /// Получает или устанавливает элемент по указанному индексу
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Элемент с указанным индексом</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public new T this[int index]
        {
            get => base[index];
            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException("В коллекции нет элемента с указанным индексом");
                OnChange?.Invoke(index, value);
                base[index] = value;
            }
        }

        public JsonSerializableList(string path) : base()
        {
            this.path = path;
            ReadFromJson();
        }

        /// <summary>
        /// Сохраняет коллекцию в json файл
        /// </summary>
        public void SaveToJson()
        {
            using (FileStream fstream = new FileStream(path, FileMode.Truncate))
            {
                fstream.Write(Encoding.Default.GetBytes(JsonSerializer.Serialize(this)));
            }
        }

        private void ReadFromJson()
        {
            string json;
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (fstream.Length == 0)
                    return;
                byte[] buffer = new byte[fstream.Length];
                fstream.Read(buffer);
                json = Encoding.Default.GetString(buffer);
            }
            AddRange(JsonSerializer.Deserialize<List<T>>(json));
        }

        /// <summary>
        /// Добавляет новый элемент в коллекцию
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public new void Add(T item)
        {
            base.Add(item);
            OnAdd?.Invoke(item);
        }

        /// <summary>
        /// Удаляет элемент из коллекции по индексу
        /// </summary>
        /// <param name="index">Индекс удаляемого элемента</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public new void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("В коллекции нет элемента с указанным индексом");
            OnRemove?.Invoke(index);
            base.RemoveAt(index);
        }

        /// <summary>
        /// Удаляет указанный элемент
        /// </summary>
        /// <param name="item">Удаляемый элемент</param>
        /// <exception cref="ItemNotInCollectionException"></exception>
        public new void Remove(T item)
        {
            int index = IndexOf(item);
            if (index < 0)
                throw new ItemNotInCollectionException();
            RemoveAt(index);
        }
    }
}