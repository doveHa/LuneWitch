using System.IO;
using System.Text.Json;
using UnityEngine.Device;

namespace Script.Manager
{
    public class PersistentDataReadWriteManager : ManagerBase<PersistentDataReadWriteManager>
    {
        public void Write(string path, string data)
        {
            string fullPath = Path.Combine(Application.persistentDataPath, path);
            File.WriteAllText(fullPath, data);
        }

        public T ReadJson<T>(string jsonPath)
        {
            return JsonSerializer.Deserialize<T>(File.ReadAllText(jsonPath));
        }
    }
}