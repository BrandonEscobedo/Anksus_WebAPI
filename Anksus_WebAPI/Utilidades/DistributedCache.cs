﻿using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TestAnskus.Shared;

namespace Anksus_WebAPI.Server.Utilidades
{
    public static class DistributedCache
    {
        public static async Task<bool> CreateRoomCache(this IDistributedCache distributedCache,
            string RoomCode,         
            TimeSpan? AbsoluteTimeExpire =null,
            TimeSpan? UnusedTimeExpire=null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = AbsoluteTimeExpire ?? TimeSpan.FromHours(3);
            options.SlidingExpiration = UnusedTimeExpire;
      
            var jsonData = JsonSerializer.Serialize(new List<ParticipanteEnCuestDTO>()) ;    
            await distributedCache.SetStringAsync(RoomCode,jsonData,options);
            return true;
        }
        public static async Task<bool> AddUserToRoomCache( this IDistributedCache distributedCache,string RoomCode,ParticipanteEnCuestDTO participante)
        {
            var Room=await distributedCache.GetStringAsync(RoomCode);
            if (Room != null)
            {
               var List = JsonSerializer.Deserialize<List<ParticipanteEnCuestDTO>>(Room);
                if (List != null)
                {
                    if (List.Any(x => x.Nombre == participante.Nombre))
                    {
                        return false;
                    }
                    else
                    {
                        List.Add(participante);
                        var jsonData =JsonSerializer.Serialize(List);
                        await distributedCache.SetStringAsync(RoomCode, jsonData);
                        return true;
                    } 
                }
              
            }
            return false;
        }

        public static async Task<T> GetUserRoom<T>(this IDistributedCache distributedCache,string RoomCode)
        {
            var jsonData=await distributedCache.GetStringAsync(RoomCode);
            if(jsonData  is  null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
