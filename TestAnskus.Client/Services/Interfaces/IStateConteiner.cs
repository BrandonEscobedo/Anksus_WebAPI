using TestAnskus.Shared;

namespace TestAnskus.Client.Services.Interfaces
{
    public interface IStateConteiner
    {
        event Action StateChange;
        ParticipanteEnCuestDTO Participante { get; set; }
        void SetValor(ParticipanteEnCuestDTO participante);
    }
}
