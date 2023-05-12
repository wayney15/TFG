using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;
using TheFarmingGame.Repositories;

namespace TheFarmingGame.Services
{
    public class LandService : ILandService
    {
        private readonly ILandRepository _landRepository;

        public LandService(ILandRepository landRepository)
        {
            _landRepository = landRepository;
        }

        public async Task GenerateNewLand()
        {
            try
            {
                await _landRepository.CreateLandAsync(new Land());
            }
            catch (Exception ex)
            {
                // Log the error and return an error result to the caller
                throw new Exception("Error saving entity.", ex);
            }
        }

        public Task<String> ListLands()
        {
            return null;
        }
        public Task<String> ListUserLands(string Id)
        {
            return null;
        }

        public async Task<IEnumerable<Land>> GetAllLandAsync()
        {
            return await _landRepository.GetAllLandAsync();
        }
    }
}
