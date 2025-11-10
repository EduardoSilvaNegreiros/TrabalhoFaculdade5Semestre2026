using Newtonsoft.Json; // Certifique-se de ter o pacote Newtonsoft.Json instalado

namespace WebApplication1.Helpers
{
    public static class SessionExtensions
    {
        // Salva um objeto na sessão como JSON
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Recupera um objeto da sessão a partir do JSON
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}