using ServiceBooking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBooking.Repository
{
    public interface IServiceRepository
    {
        public Task<IEnumerable<ServiceRequest>> GetRequests();
        public Task<ServiceRequest> GetRequestById(int id); 
        public Task<ServiceRequest> AddRequest(ServiceRequest serviceRequest);
        public Task<ServiceRequest> UpdateRequest(int id, ServiceRequest serviceRequest);
        public Task<ServiceRequest> DeleteRequest(int id);

        public Task<IEnumerable<ServiceReport>> GetReports();
        public Task<ServiceReport> GetReportById(int id);
        public Task<ServiceReport> AddReport(ServiceReport serviceReport);
        public Task<ServiceReport> UpdateReport(int id, ServiceReport serviceReport);
        public Task<ServiceReport> DeleteReport(int id);

        public Task<IEnumerable<ServiceReport>> GetReportsByUserId(int id);
    }
}
