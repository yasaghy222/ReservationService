using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReservationService.Shared
{
    public static class Extensions
    {
        public static string ToJson<T>(this T item) => JsonSerializer.Serialize(item);
        public static T? FromJson<T>(this string json) => JsonSerializer.Deserialize<T>(json);
    }
}