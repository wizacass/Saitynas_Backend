using System.Linq;
using System.Threading.Tasks;
using Saitynas_API.Models.Common.Interfaces;

namespace Saitynas_API.Models.MessageEntity
{
    public class MessageSeed : ISeed
    {
        private readonly ApiContext _context;

        public MessageSeed(ApiContext context)
        {
            _context = context;
        }

        public async Task EnsureCreated()
        {
            if (!ShouldSeed()) return;

            var message = new Message("Hello, World!");

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        private bool ShouldSeed()
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == 1);
            return message == null;
        }
    }
}
