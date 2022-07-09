using ServiceBooking.DatabaseContext;
using ServiceBooking.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ServiceBooking.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        public readonly ServiceContext _context;
        public ServiceRepository(ServiceContext context)
        {
            _context = context;
        }
        public ServiceReport AddReport(ServiceReport serviceReport)
        {
            _context.Reports.Add(serviceReport);
            _context.SaveChanges();
            return serviceReport;
        }

        public ServiceRequest AddRequest(ServiceRequest serviceRequest)
        {
            _context.Requests.Add(serviceRequest);
            _context.SaveChanges();
            return serviceRequest;
        }

        public ServiceReport DeleteReport(int id)
        {
            ServiceReport serviceReport = _context.Reports.Find(id);
            if (serviceReport == null)
            {
                return null;
            }
            _context.Reports.Remove(serviceReport);
            _context.SaveChanges();
            return serviceReport;

        }

        public ServiceRequest DeleteRequest(int id)
        {
            ServiceRequest serviceRequest = _context.Requests.Find(id);
            if(serviceRequest == null)
            {
                return null;
            }
            _context.Requests.Remove(serviceRequest);
            _context.SaveChanges();
            return serviceRequest;
        }

        public ServiceReport GetReportById(int id)
        {
            return _context.Reports.Find(id);
        }

        public IEnumerable<ServiceReport> GetReports()
        {
            return _context.Reports;
        }

        public IEnumerable<ServiceReport> GetReportsByUserId(int id)
        {
            List<int> requestIds = _context.Requests.Where(x => x.Userid == id).Select(x => x.Id).ToList();
            IEnumerable<ServiceReport> reports = _context.Reports
                .Where(x => requestIds.Contains(x.ServiceRequest.Id));
            return reports;
        }

        public ServiceRequest GetRequestById(int id)
        {
            return _context.Requests.Find(id);
        }

        public IEnumerable<ServiceRequest> GetRequests()
        {
            return _context.Requests;
        }

        public ServiceReport UpdateReport(int id, ServiceReport serviceReport)
        {
            _context.Entry(serviceReport).State = EntityState.Modified;
            _context.SaveChanges();
            return serviceReport;
            
        }

        public ServiceRequest UpdateRequest(int id, ServiceRequest serviceRequest)
        {
            _context.Entry(serviceRequest).State = EntityState.Modified;
            _context.SaveChanges();
            return serviceRequest;
        }
    }
}
