using EquipmentServiceDesk.Data;
using EquipmentServiceDesk.Models;
using EquipmentServiceDesk.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EquipmentServiceDesk.Services
{
    public class RequestService
    {
        private readonly IGenericRepository<Request> _repository;

        public RequestService(IGenericRepository<Request> repository)
        {
            _repository = repository;
        }

        public async Task<List<Request>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(Request request)
        {
            await _repository.AddAsync(request);
        }

        public async Task UpdateAsync(Request request)
        {
            await _repository.UpdateAsync(request);
        }

        public async Task DeleteAsync(Request request)
        {
            await _repository.DeleteAsync(request);
        }
    }
}