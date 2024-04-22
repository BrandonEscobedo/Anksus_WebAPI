﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using anskus.Application.Data;
using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Extensions;
using MediatR;
namespace anskus.Application.Cuestionarios
{
    public class CuestionarioService:ICuestionarioService
    {
        private readonly HttpClientServices _httpClientServices;
        public CuestionarioService(HttpClientServices httpClientServices )
        {
            _httpClientServices = httpClientServices;
            
        }
        private async Task<HttpClient> PrivateClient() => (await _httpClientServices.GetPrivateClient());
        private static string CheckResponseStatus(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return $"Ha ocurrido un error inesperado";
            else
                return null!;
        }
        public async Task<int> Add(CuestionarioDTO cuestionario )
        {
            
            var result = await (await PrivateClient()).PostAsJsonAsync(Constant.CuestionarioRoute, cuestionario);
            var data=await result.Content.ReadFromJsonAsync<int>();
            if (data == 0)
                return 0;
            return data;


        }
        public async Task<bool> Update(CuestionarioDTO cuestionario)
        {
            var result = await (await PrivateClient()).PutAsJsonAsync(Constant.CuestionarioRoute, cuestionario);
            var response = await result.Content.ReadFromJsonAsync<bool>();

            return response;
        }

        public async Task<List<CuestionarioDTO>> GetAllCuestionarios(string email)
        {
            var response = await (await PrivateClient()).GetFromJsonAsync<List<CuestionarioDTO>>($"{Constant.CuestionarioRoute}/{email}");
            if (response != null)
            {
                return response.ToList();
            }
            return null;

        }
    }
}
