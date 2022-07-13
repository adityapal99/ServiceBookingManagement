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
        public List<ServiceReport> Reports = new List<ServiceReport>
        {
            new ServiceReport() {Id=1,ServiceType="general", ActionTaken="Make1", DiagnosisDetails="Medel1",isPaid="Yes", VisitFees=1000,RepairDetails="", ReportDate = System.DateTime.Now,ServiceRequestId=1},
            new ServiceReport() {Id=2,ServiceType="repair", ActionTaken="Make2", DiagnosisDetails="Medel2",isPaid="Yes", VisitFees=2000,RepairDetails="", ReportDate = System.DateTime.Now,ServiceRequestId=2},
            new ServiceReport() {Id=3,ServiceType="support", ActionTaken="Make3", DiagnosisDetails="Medel3",isPaid="Yes", VisitFees=3000,RepairDetails="", ReportDate = System.DateTime.Now,ServiceRequestId=3},
            new ServiceReport() {Id=4,ServiceType="general", ActionTaken="Make4", DiagnosisDetails="Medel4",isPaid="Yes", VisitFees=4000,RepairDetails="", ReportDate = System.DateTime.Now,ServiceRequestId=4},
        };

        public List<ServiceRequest> Requests = new List<ServiceRequest>
        {
            new ServiceRequest() { Id = 1,Productid=1,Userid=1, Problem = "A", Description = "aaa", Status = "aaa",RequestDate = DateTime.Now},
            new ServiceRequest() { Id = 2,Productid=2,Userid=2, Problem = "B", Description = "bbb", Status = "bbb",RequestDate = DateTime.Now},
            new ServiceRequest() { Id = 3,Productid=3,Userid=3, Problem = "C", Description = "ccc", Status = "cccc",RequestDate = DateTime.Now},
            new ServiceRequest() { Id = 4,Productid=4,Userid=4, Problem = "D", Description = "ddd", Status = "dddd",RequestDate = DateTime.Now}
        };

        public async Task<ServiceReport> AddReport(ServiceReport serviceReport)
        {
            serviceReport.Id = id;
            id++;
            Reports.Add(serviceReport);
            return await Task.FromResult(serviceReport);
        }

        public async Task<ServiceRequest> AddRequest(ServiceRequest serviceRequest)
        {
            serviceRequest.Id = id;
            id++;
            Requests.Add(serviceRequest);
            return await Task.FromResult(serviceRequest);
        }

        public async Task<ServiceReport> DeleteReport(int id)
        {
            ServiceReport serviceReport = Reports.Find(x => x.Id == id);
            Reports.Remove(serviceReport);
            return await Task.FromResult(serviceReport);
        }

        public async Task<ServiceRequest> DeleteRequest(int id)
        {
            ServiceRequest serviceRequest = Requests.Find(x => x.Id == id);
            Requests.Remove(serviceRequest);
            return await Task.FromResult(serviceRequest);
        }

        public async Task<ServiceReport> GetReportById(int id)
        {
            ServiceReport report = Reports.Find(x => x.Id == id);
            return await Task.FromResult(report);
        }

        public async Task<IEnumerable<ServiceReport>> GetReports()
        {
            IEnumerable<ServiceReport> reports = Reports;
            return await Task.FromResult(reports);
        }

        public async Task<IEnumerable<ServiceReport>> GetReportsByUserId(int id)
        {
            var requestId = Requests.FindAll(x => x.Userid == id).Select(x=>x.Id);
            IEnumerable<ServiceReport> reports = Reports.FindAll(x=>requestId.Contains(x.Id)).ToList();
            return await Task.FromResult(reports);
        }

        public async Task<ServiceRequest> GetRequestById(int id)
        {
            ServiceRequest request = Requests.Find(x => x.Id == id);
            return await Task.FromResult(request);
        }

        public async Task<IEnumerable<ServiceRequest>> GetRequests()
        {
            IEnumerable<ServiceRequest> requests = Requests;
            return await Task.FromResult(requests);
        }

        public async Task<ServiceReport> UpdateReport(int id, ServiceReport serviceReport)
        {
            ServiceReport Original = Reports.Find(x => x.Id == id);
            Original.ServiceType = serviceReport.ServiceType;
            Original.ActionTaken = serviceReport.ActionTaken;
            Original.DiagnosisDetails = serviceReport.DiagnosisDetails;
            Original.VisitFees = serviceReport.VisitFees;
            Original.RepairDetails = serviceReport.RepairDetails;
            Original.ReportDate = serviceReport.ReportDate;
            Original.ServiceRequestId = serviceReport.ServiceRequestId;
            return await Task.FromResult(serviceReport);
        }

        public async Task<ServiceRequest> UpdateRequest(int id, ServiceRequest serviceRequest)
        {
            ServiceRequest Original = Requests.Find(x => x.Id == id);
            Original.Productid = serviceRequest.Productid;
            Original.Userid = serviceRequest.Userid;
            Original.Problem = serviceRequest.Problem;
            Original.Description = serviceRequest.Description;
            Original.Status = serviceRequest.Status;
            Original.RequestDate = serviceRequest.RequestDate;
            return await Task.FromResult(serviceRequest);
        }
    }
}
