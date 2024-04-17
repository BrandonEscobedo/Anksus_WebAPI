﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Anksus_WebAPI.Models.dbModels;
using Microsoft.EntityFrameworkCore;

namespace anskus.Application.DTOs.Cuestionarios
{
    public class RespuestasDTO
    {  
        public int IdRespuesta { get; set; }  
        public int IdPregunta { get; set; }

        public string respuesta { get; set; } = null!;

        [Column("R_correcta")]
        public bool RCorrecta { get; set; }

    }
}
