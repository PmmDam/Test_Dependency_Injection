using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test_Dependency_Injection.Services
{
    public static class SerializerServices
    {

        private static JsonSerializerOptions _serializerOptions { get; set; }

        public static JsonSerializerOptions SerializerOptions
        {
            get
            {
                if(_serializerOptions == null)
                {
                    _serializerOptions = new JsonSerializerOptions()
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        PropertyNameCaseInsensitive = true,
                        ReadCommentHandling = JsonCommentHandling.Skip,
                        WriteIndented = true,
                    };
                }
                return _serializerOptions;
            }
        }


        public static async Task SerializeIntoJson<T>(string jsonFilePath, T model)
        {
            string json = JsonSerializer.Serialize<T>(model,SerializerOptions);
            await File.WriteAllTextAsync(jsonFilePath, json);
        }

        public static async Task<T> DeserializeFromJson <T>(string jsonFIlePath)
        {
            string json = await File.ReadAllTextAsync(jsonFIlePath);
            T model = JsonSerializer.Deserialize<T>(json,SerializerOptions);
            return model;
        }
    }
}
