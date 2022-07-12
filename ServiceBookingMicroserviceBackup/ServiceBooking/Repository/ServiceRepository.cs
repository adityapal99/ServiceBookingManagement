using ServiceBooking.DatabaseContext;
using ServiceBooking.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBooking.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        public readonly ServiceContext _context;
        public ServiceRepository(ServiceContext context)
        {
            _context = context;
        }
        public async Task<ServiceReport> AddReport(ServiceReport serviceReport)
        {
            _context.Reports.Add(serviceReport);
            await _context.SaveChangesAsync();
            return serviceReport;
        }

        public async Task<ServiceRequest> AddRequest(ServiceRequest serviceRequest)
        {
            _context.Requests.Add(serviceRequest);
            await _context.SaveChangesAsync();
            return serviceRequest;
        }

        public async Task<ServiceReport> DeleteReport(int id)
        {
            ServiceReport serviceReport = _context.Reports.Find(id);
            if (serviceReport == null)
            {
                return null;
            }
            _context.Reports.Remove(serviceReport);
            await _context.SaveChangesAsync();
            return serviceReport;

        }

        public async Task<ServiceRequest> DeleteRequest(int id)
        {
            ServiceRequest serviceRequest = _context.Requests.Find(id);
            if (serviceRequest == null)
            {
                return null;
            }
            _context.Requests.Remove(serviceRequest);
            await _context.SaveChangesAsync();
            return serviceRequest;
         }

        public async Task<ServiceReport> GetReportById(int id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task<IEnumerable<ServiceReport>> GetReports()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<IEnumerable<ServiceReport>> GetReportsByUserId(int id)
        {
            List<int> requestIds = _context.Requests.Where(x => x.Userid == id).Select(x => x.Id).ToList();
            IEnumerable<ServiceReport> reports = _context.Reports
                .Where(x => requestIds.Contains(x.ServiceRequest.Id));
            return reports;
        }

        public async Task<ServiceRequest> GetRequestById(int id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task<IEnumerable<ServiceRequest>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        public async Task<ServiceReport> UpdateReport(int id, ServiceReport serviceReport)
        {
            _context.Entry(serviceReport).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return serviceReport;
            
        }

        public async Task<ServiceRequest> UpdateRequest(int id, ServiceRequest serviceRequest)
        {
            _context.Entry(serviceRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return serviceRequest;
        }
    }
}
