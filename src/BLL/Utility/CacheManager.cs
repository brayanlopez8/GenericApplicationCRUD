using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace BLL.Utitliy
{
    public class CacheManager<T>
    {
        private static bool EnableCache = Convert.ToBoolean(ConfigurationManager.AppSettings["CacheEnable"]);
        private static int TimeHourDefault = 1;
        /// <summary>
        /// Obtiene una valor de cache si está habilitado el cache
        /// </summary>
        /// <param name="cacheKey">Llave del valor en cache</param>
        /// <returns></returns>
        public static T TryGetFromCache(string cacheKey)
        {
            T cacheValue = default(T);

            if (EnableCache && HttpRuntime.Cache[cacheKey] != null)
            {
                Debug.WriteLine(string.Format("CacheManager: {0} from cache", cacheKey));
                cacheValue = (T)HttpRuntime.Cache[cacheKey];
            }

            return cacheValue;
        }

        public static void RemoveItemFromCache(string cacheKey)
        {
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                HttpRuntime.Cache.Remove(cacheKey);
            }
        }


        /// <summary>
        /// Agrega un valor al cache si está habilitado
        /// </summary>
        /// <param name="cacheKey">Llave del valor en cache</param>
        /// <param name="cacheValue">Valor a agregar a cache</param>
        /// <param name="hours">Tiempo de permanencia en cache en horas</param>
        /// <param name="minutes">Tiempo de permanencia en cache en minutes</param>
        /// <param name="seconds">Tiempo de permanencia en cache en segundos</param>
        public static void TryAddToCache(string cacheKey, T cacheValue, int hours, int minutes, int seconds)
        {
            if (EnableCache && cacheValue != null)
            {
                if (HttpRuntime.Cache[cacheKey] != null)
                    HttpRuntime.Cache.Remove(cacheKey);
                TimeSpan timeSpan = new TimeSpan(hours, minutes, seconds);
                HttpRuntime.Cache.Add(cacheKey, cacheValue, null, DateTime.Now.Add(timeSpan), Cache.NoSlidingExpiration, CacheItemPriority.Low, null);
            }
        }

        /// <summary>
        /// Agrega un valor al cache si está habilitado
        /// </summary>
        /// <param name="cacheKey">Llave del valor en cache</param>
        /// <param name="cacheValue">Valor a agregar a cache</param>
        /// <param name="hours">Tiempo de permanencia en cache en horas</param>
        /// <param name="minutes">Tiempo de permanencia en cache en minutes</param>
        public static void TryAddToCache(string cacheKey, T cacheValue, int hours, int minutes)
        {
            TryAddToCache(cacheKey, cacheValue, hours, minutes, 0);
        }

        /// <summary>
        /// Agrega un valor al cache si está habilitado
        /// </summary>
        /// <param name="cacheKey">Llave del valor en cache</param>
        /// <param name="cacheValue">Valor a agregar a cache</param>
        /// <param name="hours">Tiempo de permanencia en cache en horas</param>
        public static void TryAddToCache(string cacheKey, T cacheValue, int hours)
        {
            TryAddToCache(cacheKey, cacheValue, hours, 0, 0);
        }

        public static void TryAddToCacheDefaultTime(string cacheKey, T cacheValue)
        {
            TryAddToCache(cacheKey, cacheValue,TimeHourDefault, 0, 0);
        }
    }
}
