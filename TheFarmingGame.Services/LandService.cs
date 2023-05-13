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

        public async Task<Land> GenerateNewLand()
        {
            try
            {
                var land = await _landRepository.CreateLandAsync(new Land());
                return land;
            }
            catch (Exception ex)
            {
                // Log the error and return an error result to the caller
                throw new Exception("Error saving entity.", ex);
            }
        }

        public async Task<Land?> UpdateLand(Land land)
        {
            return await _landRepository.UpdateLand(land);
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
        public async Task<IEnumerable<Land>> GetLandByUserIdAsync(int UserId)
        {
            return await _landRepository.GetLandByUserIdAsync(UserId);
        }
        public async Task<IEnumerable<Land>> GetLandByIdAsync(int Id)
        {
            return await _landRepository.GetLandByUserIdAsync(Id);
        }
    }
}
