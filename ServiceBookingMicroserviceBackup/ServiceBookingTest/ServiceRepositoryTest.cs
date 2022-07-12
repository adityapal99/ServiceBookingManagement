using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ServiceBooking.DatabaseContext;
using ServiceBooking.Repository;
using ServiceBooking.Models;

namespace ServiceRepositoryTest
{
    public class ServiceRepositoryTest : IDisposable
    {
        private ServiceContext _context;
        private ServiceRepository _sut;


        public List<ServiceReport> GetReports()
        {
            return new List<ServiceReport>
            {
            new ServiceReport { Id = 1, ServiceType = "A", ActionTaken = "aaa", DiagnosisDetails = "aaa",isPaid="true", VisitFees = 1000,RepairDetails="", ReportDate = DateTime.Now,ServiceRequestId=1 },
            new ServiceReport { Id = 2, ServiceType = "B", ActionTaken = "aaa", DiagnosisDetails = "aaa",isPaid="false", VisitFees = 1000,RepairDetails="", ReportDate = DateTime.Now,ServiceRequestId=2 },
            //new ServiceReport { Id = 3, Name = "C", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now },
            //new ServiceReport { Id = 4, Name = "D", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now }
            };
        }


        public List<ServiceRequest> GetRequests()
        {
            return new List<ServiceRequest>
            {
            new ServiceRequest { Id = 1,Productid=1,Userid=1, Problem = "A", Description = "aaa", Status = "aaa",RequestDate = DateTime.Now},
            new ServiceRequest { Id = 2,Productid=2,Userid=2, Problem = "B", Description = "aaa", Status = "aaa",RequestDate = DateTime.Now},
            //new ServiceReport { Id = 3, Name = "C", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now },
            //new ServiceReport { Id = 4, Name = "D", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now }
            };
        }


        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ServiceContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new ServiceContext(options);
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Check_GetReports()
        {
            List<ServiceReport> reports = GetReports();
            _context.Reports.AddRange(reports);
            _context.SaveChanges();

            _sut = new ServiceRepository(_context);

            var result = await _sut.GetReports();

            Assert.That(result, Is.Not.Null);
            var returnedValue = result as List<ServiceReport>;
            Assert.That(returnedValue, Is.Not.Null);
            Assert.That(returnedValue.Count, Is.EqualTo(reports.Count));
        }

        [Test]
        public async Task Check_GetReportById()
        {
            int id = 2;
            _context.Reports.AddRange(GetReports());
            _context.SaveChanges();
            _sut = new ServiceRepository(_context);

            var result = await _sut.GetReportById(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task Check_CreateReport()
        {
            List<ServiceReport> reports = GetReports();
            _context.Reports.AddRange(reports);
            _context.SaveChanges();
            _sut = new ServiceRepository(_context);
            ServiceReport r = new ServiceReport { ServiceType = "A", ActionTaken = "aaa", DiagnosisDetails = "aaa", isPaid = "true", VisitFees = 1000, RepairDetails = "", ReportDate = DateTime.Now, ServiceRequestId = 1 };


            await _sut.AddReport(r);

            Assert.That(_context.Reports.Count, Is.EqualTo(3));

        }

        [Test]
        public async Task Check_DeleteReport()
        {
            int id = 2;
            List<ServiceReport> reports = GetReports();
            _context.Reports.AddRange(GetReports());
            _context.SaveChanges();
            _sut = new ServiceRepository(_context);

            await _sut.DeleteReport(id);

            Assert.That(_context.Reports.Count, Is.EqualTo(1));

        }

        [Test]
        public async Task Check_PutReport()
        {
            int id = 1;
            List<ServiceReport> reports = GetReports();
            ServiceReport r = reports.Find(x => x.Id == id);
            string type = "A";
            r.ServiceType = type;
            _context.Reports.AddRange(reports);
            _context.SaveChanges();
            _sut = new ServiceRepository(_context);

            ServiceReport returnedProduct = await _sut.UpdateReport(id, r);

            Assert.That(r, Is.EqualTo(returnedProduct));


        }
        [Test]
        public async Task Check_GetRequests()
        {
            List<ServiceRequest> requests = GetRequests();
            _context.Requests.AddRange(requests);
            _context.SaveChanges();

            _sut = new ServiceRepository(_context);

            var result = await _sut.GetRequests();

            Assert.That(result, Is.Not.Null);
            var returnedValue = result as List<ServiceRequest>;
            Assert.That(returnedValue, Is.Not.Null);
            Assert.That(returnedValue.Count, Is.EqualTo(requests.Count));
        }

        [Test]
        public async Task Check_GetRequestById()
        {
            int id = 2;
            _context.Requests.AddRange(GetRequests());
            _context.SaveChanges();
            _sut = new ServiceRepository(_context);

            var result = await _sut.GetRequestById(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public async Task Check_CreateRequest()
        {
            List<ServiceRequest> requests = GetRequests();
            _context.Requests.AddRange(requests);
            _context.SaveChanges();
            _sut = new ServiceRepository(_context);
            ServiceRequest r =  new ServiceRequest { Productid = 1, Userid = 1, Problem = "A", Description = "aaa", Status = "aaa", RequestDate = DateTime.Now  };


            await _sut.AddRequest(r);

            Assert.That(_context.Requests.Count, Is.EqualTo(3));

        }

        [Test]
        public async Task Check_DeleteRequest()
        {
            int id = 2;
            List<ServiceRequest> requests = GetRequests();
            _context.Requests.AddRange(GetRequests());
            _context.SaveChanges();
            _sut = new ServiceRepository(_context);

            await _sut.DeleteRequest(id);

            Assert.That(_context.Requests.Count, Is.EqualTo(1));

        }

        [Test]
        public async Task Check_PutRequest()
        {
            int id = 1;
            List<ServiceRequest> requests = GetRequests();
            ServiceRequest r = requests.Find(x => x.Id == id);
            string state = "aaa";
            r.Status = state;
            _context.Requests.AddRange(requests);
            _context.SaveChanges();
            _sut = new ServiceRepository(_context);

            ServiceRequest returnedProduct = await _sut.UpdateRequest(id, r);

            Assert.That(r, Is.EqualTo(returnedProduct));


        }
    }
}
