
using ServiceBooking.Models;
using System.Collections.Generic;

namespace ServiceBooking.Repository
{
    public interface IServiceRepository
    {
        public IEnumerable<ServiceRequest> GetRequests();
        public ServiceRequest GetRequestById(int id); 
        public ServiceRequest AddRequest(ServiceRequest serviceRequest);
        public ServiceRequest UpdateRequest(int id, ServiceRequest serviceRequest);
        public ServiceRequest DeleteRequest(int id);

        public IEnumerable<ServiceReport> GetReports();
        public ServiceReport GetReportById(int id);
        public ServiceReport AddReport(ServiceReport serviceReport);
        public ServiceReport UpdateReport(int id, ServiceReport serviceReport);
        public ServiceReport DeleteReport(int id);

        public IEnumerable<ServiceReport> GetReportsByUserId(int id);
    }
}
