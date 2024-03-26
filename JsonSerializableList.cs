﻿using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Automation.Peers;

namespace CourseWork
{
    public delegate void OnAdd<T>(T item);

    public abstract class JsonSerializableList<T> : List<T>
    {
        public event OnAdd<T> OnAdd;
        
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
    }
}