﻿using anskus.Application.DTOs.Cuestionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.HubSignalr.StateContainerHub
{
    public class StateConteiner:IStateConteiner
    {
        public ParticipanteEnCuestDTO Participante { get; set; } = new();
        public event Action<ParticipanteEnCuestDTO>? OnAgregarUsuario;
        public event Action<PreguntasDTO, string?>? OnSiguientePregunta;
        public string titulo { get; set; } = "";
        public List<ParticipanteEnCuestDTO> participanteEnCuest { get; set; } = new();
        public CuestionarioDTO Cuestionario { get; set; } = new();
        public PreguntasDTO pregunta { get; set; } = new();
        public void AddParticipante(ParticipanteEnCuestDTO participante)

        {
            participanteEnCuest.Add(participante);
            OnAgregarUsuario?.Invoke(participante);
        }

        public void RemoveParticipante(ParticipanteEnCuestDTO participante)
        {
            var user = participanteEnCuest
                .Where(x => x.IdPeC == participante.IdPeC)
                .FirstOrDefault();
            if (user != null)
            {
                participanteEnCuest.Remove(user);
                OnAgregarUsuario?.Invoke(participante);
            }
        }

        public async Task SetCuestionario(CuestionarioDTO cuestionario, int codigo)
        {
            Cuestionario = cuestionario;
        }
        public CuestionarioDTO GetCuestionario()
        {
            return Cuestionario;
        }
        public void SiguientePregunta(PreguntasDTO pregunta, string? titulo)
        {
            OnSiguientePregunta?.Invoke(pregunta, titulo);
        }
        public void SetParticipante(ParticipanteEnCuestDTO participante)
        {
            Participante = participante;
        }
    }
}