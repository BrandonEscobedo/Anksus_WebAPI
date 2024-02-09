using Microsoft.AspNetCore.SignalR;

namespace Anksus_WebAPI.Server.Hubs.Implementacion
{
    public class HubManager
    {
        private readonly Dictionary<int, Hub> _hubmap;

        public HubManager(Dictionary<int, Hub> hubmap)
        {
            _hubmap = hubmap;
        }
        public void AddHub(int code, Hub hub)
        {
            _hubmap[code] = hub;
        }
        public bool TryGetHub(int code, out Hub hub)
        {
            return _hubmap.TryGetValue(code, out hub!);
        }
    }
}
