using Izki_Club.Data;
using Izki_Club.Services.Interfaces;

namespace Izki_Club.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly DataContext _context;
        public PlayerService(DataContext context)
        {
            _context = context;
        }

    }
}
