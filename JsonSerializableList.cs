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

    public abstract class JsonSerializableList<T> : List<T>
    {
        public event AddNewItemHandler<T> OnAdd;

        public event RemoveItemHandler OnRemove;
        
        protected string path;

        protected JsonSerializableList(string path) : base()
        {
            this.path = path;
            ReadFromJson();
        }

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

        public new void Add(T item)
        {
            base.Add(item);
            OnAdd?.Invoke(item);
        }

        public new void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("В коллекции нет элемента с указанным индексом");
            OnRemove?.Invoke(index);
            base.RemoveAt(index);
        }

        public new void Remove(T item)
        {
            int index = IndexOf(item);
            if (index < 0)
                throw new ItemNotInCollectionException();
            RemoveAt(index);
        }
    }
}