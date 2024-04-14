﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using anskus.Application.DTOs.Request.Account;
using NetcodeHub.Packages.Extensions.LocalStorage;
namespace anskus.Application.Extensions
{
    public class LocalStorageServices(ILocalStorageService localStorageService)
    {
        private async Task<string> GetBrowserLocalStorage()
        {
            var tokenModel = await localStorageService.GetEncryptedItemAsStringAsync(Constant.BrowserStorageKey);
            return tokenModel;
        }
        public async Task<LocalStorageDTO> GetModelFromToken()
        {
            try
            {
                string token = await GetBrowserLocalStorage();
                if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token)) 
                    return new LocalStorageDTO();
                return DeserializeJsonString<LocalStorageDTO>(token);
                         
                }
            catch (Exception ex)
            {
                return new LocalStorageDTO();
            }
        }
        public async Task SetBrowserLocalStorage(LocalStorageDTO localStorageDTO)
        {
            try
            {
                string token =serializeObj(localStorageDTO);
                await localStorageService.SaveAsEncryptedStringAsync(Constant.BrowserStorageKey,token);

            }
            catch(Exception ex)
            {

            }
        }
        public async Task RemoveTokenFromBrowserLocalStorage()=>await localStorageService.DeleteItemAsync(Constant.BrowserStorageKey);
        private static string serializeObj<T>(T modelObjet)=>JsonSerializer.Serialize(modelObjet,JsonOptions());
        private static T DeserializeJsonString<T>(string jsonStr)=>JsonSerializer.Deserialize<T>(jsonStr, JsonOptions())!;

        private static JsonSerializerOptions JsonOptions()
        {
            return new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip
            };
        }

    }
}
