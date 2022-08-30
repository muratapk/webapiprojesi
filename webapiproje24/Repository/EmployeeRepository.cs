using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapiproje24.DataDbContext;

namespace webapiproje24.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int Id)
        {
            var result = await _context.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
           if(result!=null)
            {
                _context.Employees.Remove(result);
               await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            //üst kısıma async methodunu ekliyoruz 
            return await _context.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
            //id ile gönderilen veri satır olarak çekiyoruz.
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            //üst kısıma async methodunu ekliyoruz 
            return await _context.Employees.ToListAsync();
            //veri tabanındaki var tablodaki tüm verileri çekiyoruz.
        }

        public async Task<IEnumerable<Employee>> Search(string name)
        {

            IQueryable<Employee> sorgu = _context.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                //IQueryable<Employee> sorgu = _context.Employees.Where(a => a.Name.Contains(name));
                sorgu = sorgu.Where(a => a.Name.Contains(name));
               
            }
            return await sorgu.ToListAsync();
            //else
            //{
            //    return null;
            //}

        }

        public async Task<Employee> UpdateEmployee(Employee employee, int Id)
        {
            //üst kısıma async methodunu ekliyoruz 
            var result = await _context.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
            //güncellencek veri çekiyoruz. tek bir satır olarak
            if(result!=null)
            {
                result.Name = employee.Name;
                result.City = employee.City;
                await _context.SaveChangesAsync();
            }
            return result;
        }
    }
}
