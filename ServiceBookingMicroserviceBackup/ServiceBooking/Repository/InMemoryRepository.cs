using ServiceBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBooking.Repository
{
    public class InMemoryRepository : IServiceRepository
    {
        int id = 5;
        public List<ServiceReport> Products = new List<ServiceReport>
        {
            new ServiceReport() {Id=1,ServiceType="general", ActionTaken="Make1", DiagnosisDetails="Medel1",isPaid="Yes", VisitFees=1000,RepairDetails="", ReportDate = System.DateTime.Now,ServiceRequestId=1},
            new ServiceReport() {Id=2,ServiceType="repair", ActionTaken="Make2", DiagnosisDetails="Medel2",isPaid="Yes", VisitFees=2000,RepairDetails="", ReportDate = System.DateTime.Now,ServiceRequestId=2},
            new ServiceReport() {Id=3,ServiceType="support", ActionTaken="Make3", DiagnosisDetails="Medel3",isPaid="Yes", VisitFees=3000,RepairDetails="", ReportDate = System.DateTime.Now,ServiceRequestId=3},
            new ServiceReport() {Id=4,ServiceType="general", ActionTaken="Make4", DiagnosisDetails="Medel4",isPaid="Yes", VisitFees=4000,RepairDetails="", ReportDate = System.DateTime.Now,ServiceRequestId=4},
        };

        public Task<ServiceReport> AddReport(ServiceReport serviceReport)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceRequest> AddRequest(ServiceRequest serviceRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceReport> DeleteReport(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceRequest> DeleteRequest(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceReport> GetReportById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServiceReport>> GetReports()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServiceReport>> GetReportsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceRequest> GetRequestById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServiceRequest>> GetRequests()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceReport> UpdateReport(int id, ServiceReport serviceReport)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceRequest> UpdateRequest(int id, ServiceRequest serviceRequest)
        {
            throw new NotImplementedException();
        }
    }
}
