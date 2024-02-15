using TestAnskus.Client.Services.Interfaces;
using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Implementacion
{
    public class StateConteiner : IStateConteiner
    {
        public ParticipanteEnCuestDTO Participante { get; set; }

        public event Action StateChange;

        public void SetValor(ParticipanteEnCuestDTO participante)
        {
            Participante = participante;
            StateChange?.Invoke();
        }

    }
}
