
using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Employee>> GetAllAsync() {return _repo.GetAllAsync(); }
        public Task<Employee> GetByIdAsync(int id) {return _repo.GetByIdAsync(id); } 
        public Task AddAsync(Employee e) {return _repo.AddAsync(e);}
        public Task UpdateAsync(Employee e) {return _repo.UpdateAsync(e); } 
        public Task DeleteAsync(int id) {return _repo.DeleteAsync(id); } 
    }
}
