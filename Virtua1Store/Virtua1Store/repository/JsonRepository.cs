using Newtonsoft.Json;
using VirtStore.interfaces;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace VirtStore.repository
{
    public class JsonRepository<T> : IRepository<T>
    {
        private readonly string _filePath;

        public JsonRepository(string filePath)
        {
            _filePath = filePath;
        }

        private List<T> ReadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }

            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
        }

        private void SaveToFile(List<T> entities)
        {
            var jsonData = JsonConvert.SerializeObject(entities, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }

        public void Add(T entity)
        {
            var entities = ReadFromFile();
            entities.Add(entity);
            SaveToFile(entities);
        }

        public void Update(T entity)
        {
            var entities = ReadFromFile();
            var index = entities.FindIndex(e => ((dynamic)e).Id == ((dynamic)entity).Id);
            if (index != -1)
            {
                entities[index] = entity;
                SaveToFile(entities);
            }
        }

        public void Delete(int id)
        {
            var entities = ReadFromFile();
            var entityToRemove = entities.FirstOrDefault(e => ((dynamic)e).Id == id);
            if (entityToRemove != null)
            {
                entities.Remove(entityToRemove);
                SaveToFile(entities);
            }
        }

        public T GetById(int id)
        {
            var entities = ReadFromFile();
            return entities.FirstOrDefault(e => ((dynamic)e).Id == id);
        }

        public List<T> GetAll()
        {
            return ReadFromFile();
        }
        public void Save(List<T> entities)
        {
            SaveToFile(entities);
        }
    }
}