using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ServiceBooking.Repository;
using ServiceBooking.Controllers;
using ServiceBooking.Models;

namespace ServiceControllerTest
{

    public class ServiceControllerTest
    {
        Mock<IServiceRepository> _repository = new Mock<IServiceRepository>();
        ServiceController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new ServiceController(_repository.Object);

        }

        [Test]
        public async Task CheckGetReports()
        {
            List<ServiceReport> reports = new List<ServiceReport>()
            {
            new ServiceReport { Id = 1, ServiceType = "A", ActionTaken = "aaa", DiagnosisDetails = "aaa",isPaid="true", VisitFees = 1000,RepairDetails="", ReportDate = DateTime.Now,ServiceRequestId=1 },
            new ServiceReport { Id = 2, ServiceType = "B", ActionTaken = "aaa", DiagnosisDetails = "aaa",isPaid="false", VisitFees = 1000,RepairDetails="", ReportDate = DateTime.Now,ServiceRequestId=2 },
            //new ServiceReport { Id = 3, Name = "C", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now },
            //new ServiceReport { Id = 4, Name = "D", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now }
            };


            _repository.Setup(x => x.GetReports()).ReturnsAsync(reports);


            //var response = await _controller.GetProducts();
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //OkObjectResult result = response.Result as OkObjectResult;
            //var returedValues = result.Value as IEnumerable<Product>;
            //Assert.That(returedValues.Count(), Is.EqualTo(products.Count));
            //Assert.That(products, Is.EqualTo(returedValues));



            var response = await _controller.GetReports();
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValues = converted.payload as IEnumerable<ServiceReport>;
            Assert.That(returedValues.Count(), Is.EqualTo(reports.Count));
            Assert.That(reports, Is.EqualTo(returedValues));
        }

        [Test]
        public async Task CheckGetReportById_ReportPresent()
        {
            int id = 1;
            ServiceReport r = new ServiceReport { Id = 1, ServiceType = "A", ActionTaken = "aaa", DiagnosisDetails = "aaa", isPaid = "true", VisitFees = 1000, RepairDetails = "", ReportDate = DateTime.Now, ServiceRequestId = 1 };
            _repository.Setup(x => x.GetReportById(id)).ReturnsAsync(r);

            //var response = await _controller.GetProduct(id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //Product returedValue = result.Value as Product;
            //Assert.That(returedValue.Id, Is.EqualTo(id));
            //Assert.That(p, Is.EqualTo(returedValue));

            var response = await _controller.GetReporttById(id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as ServiceReport;
            Assert.That(r, Is.EqualTo(returedValue));

        }

        [Test]
        public async Task CheckGetReportById_ReportMissing()
        {
            int id = 6;

            _repository.Setup(x => x.GetReportById(id)).ReturnsAsync((ServiceReport)null);

            var response = await _controller.GetReporttById(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }

        [Test]
        public async Task CheckPutReport_ValidInputs()
        {
            ServiceReport report = new ServiceReport { Id = 1, ServiceType = "A", ActionTaken = "aaa", DiagnosisDetails = "aaa", isPaid = "true", VisitFees = 1000, RepairDetails = "", ReportDate = DateTime.Now, ServiceRequestId = 1 };

            _repository.Setup(x => x.UpdateReport(report.Id, report)).ReturnsAsync(report);

            //var response = await _controller.PutProduct(product.Id, product);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedValue = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedValue.Id));

            var response = await _controller.UpdateReport(report.Id, report);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as ServiceReport;
            Assert.That(returedValue, Is.EqualTo(report));
        }
        [Test]
        public async Task CheckPutReport_InvalidInputs()
        {
            int id = 6;
            ServiceReport r = new ServiceReport { Id = 1, ServiceType = "A", ActionTaken = "aaa", DiagnosisDetails = "aaa", isPaid = "true", VisitFees = 1000, RepairDetails = "", ReportDate = DateTime.Now, ServiceRequestId = 1 };

            _repository.Setup(x => x.UpdateReport(id, r)).ReturnsAsync(r);

            var response = await _controller.UpdateReport(id, r);
            Assert.IsInstanceOf<BadRequestResult>(response.Result);
        }
        [Test]
        public async Task CheckPostReport_ValidInputs()
        {
            ServiceReport report = new ServiceReport { ServiceType = "A", ActionTaken = "aaa", DiagnosisDetails = "aaa", isPaid = "true", VisitFees = 1000, RepairDetails = "", ReportDate = DateTime.Now, ServiceRequestId = 1 };
            ServiceReport reportFinal = new ServiceReport { Id = 1, ServiceType = "A", ActionTaken = "aaa", DiagnosisDetails = "aaa", isPaid = "true", VisitFees = 1000, RepairDetails = "", ReportDate = DateTime.Now, ServiceRequestId = 1 };

            _repository.Setup(x => x.AddReport(report)).ReturnsAsync(reportFinal);

            //var response = await _controller.PostProduct(product);
            //Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            //var result = response.Result as CreatedAtActionResult;
            //var returedValue = result.Value as Product;
            //Assert.IsNotNull(returedValue.Id);

            var response = await _controller.AddReport(report);
            Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            var result = response.Result as CreatedAtActionResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as ServiceReport;
            //Assert.That(returedValue, Is.EqualTo(reportFinal));
            Assert.That(returedValue.Id, Is.EqualTo(reportFinal.Id));
        }
        [Test]
        public async Task CheckPostReport_InvalidInputs()
        {
            ServiceReport report = new ServiceReport();

            _repository.Setup(x => x.AddReport(report)).ReturnsAsync(report);

            var response = await _controller.AddReport(report);
            Assert.IsInstanceOf<BadRequestResult>(response.Result);
        }

        [Test]
        public async Task CheckDeleteReport_ReportPresent()
        {
            ServiceReport report = new ServiceReport { Id = 1, ServiceType = "A", ActionTaken = "aaa", DiagnosisDetails = "aaa", isPaid = "true", VisitFees = 1000, RepairDetails = "", ReportDate = DateTime.Now, ServiceRequestId = 1 };

            _repository.Setup(x => x.DeleteReport(report.Id)).ReturnsAsync(report);

            //var response = await _controller.DeleteProduct(product.Id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedProduct = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedProduct.Id));
            //Assert.That(returedProduct, Is.EqualTo(product));

            var response = await _controller.DeleteReport(report.Id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedReport = converted.payload as ServiceReport;
            Assert.That(report.Id, Is.EqualTo(returedReport.Id));
            Assert.That(returedReport, Is.EqualTo(report));
        }

        [Test]
        public async Task CheckDeleteReport_ReportMissing()
        {
            int id = 10;

            _repository.Setup(x => x.DeleteReport(id)).ReturnsAsync((ServiceReport)null);

            var response = await _controller.DeleteReport(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }

        [Test]
        public async Task CheckGetRequests()
        {
            List<ServiceRequest> requests = new List<ServiceRequest>()
            {
            new ServiceRequest { Id = 1,Productid=1,Userid=1, Problem = "A", Description = "aaa", Status = "aaa",RequestDate = DateTime.Now},
            new ServiceRequest { Id = 2,Productid=2,Userid=2, Problem = "B", Description = "aaa", Status = "aaa",RequestDate = DateTime.Now},
            //new ServiceReport { Id = 3, Name = "C", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now },
            //new ServiceReport { Id = 4, Name = "D", Make = "aaa", Model = "aaa", Cost = 1000, CreatedDate = DateTime.Now }
            };


            _repository.Setup(x => x.GetRequests()).ReturnsAsync(requests);


            //var response = await _controller.GetProducts();
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //OkObjectResult result = response.Result as OkObjectResult;
            //var returedValues = result.Value as IEnumerable<Product>;
            //Assert.That(returedValues.Count(), Is.EqualTo(products.Count));
            //Assert.That(products, Is.EqualTo(returedValues));



            var response = await _controller.GetRequests();
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValues = converted.payload as IEnumerable<ServiceRequest>;
            Assert.That(returedValues.Count(), Is.EqualTo(requests.Count));
            Assert.That(requests, Is.EqualTo(returedValues));
        }

        [Test]
        public async Task CheckGetRequestById_RequestPresent()
        {
            int id = 1;
            ServiceRequest request= new ServiceRequest{ Id = 1,Productid = 1,Userid = 1, Problem = "A", Description = "aaa", Status = "aaa",RequestDate = DateTime.Now};
            _repository.Setup(x => x.GetRequestById(id)).ReturnsAsync(request);

            //var response = await _controller.GetProduct(id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //Product returedValue = result.Value as Product;
            //Assert.That(returedValue.Id, Is.EqualTo(id));
            //Assert.That(p, Is.EqualTo(returedValue));

            var response = await _controller.GetRequestById(id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as ServiceRequest;
            Assert.That(request, Is.EqualTo(returedValue));

        }

        [Test]
        public async Task CheckGetRequestById_RequestMissing()
        {
            int id = 6;

            _repository.Setup(x => x.GetRequestById(id)).ReturnsAsync((ServiceRequest)null);

            var response = await _controller.GetReporttById(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }

        [Test]
        public async Task CheckPutRequest_ValidInputs()
        {
            ServiceRequest request = new ServiceRequest { Id = 1, Productid = 1, Userid = 1, Problem = "A", Description = "aaa", Status = "aaa", RequestDate = DateTime.Now };

            _repository.Setup(x => x.UpdateRequest(request.Id, request)).ReturnsAsync(request);

            //var response = await _controller.PutProduct(product.Id, product);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedValue = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedValue.Id));

            var response = await _controller.UpdateRequest(request.Id, request);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as ServiceRequest;
            Assert.That(returedValue, Is.EqualTo(request));
        }
        [Test]
        public async Task CheckPutRequest_InvalidInputs()
        {
            int id = 6;
            ServiceRequest request = new ServiceRequest { Id = 1, Productid = 1, Userid = 1, Problem = "A", Description = "aaa", Status = "aaa", RequestDate = DateTime.Now };

            _repository.Setup(x => x.UpdateRequest(id, request)).ReturnsAsync(request);

            var response = await _controller.UpdateRequest(id, request);
            Assert.IsInstanceOf<BadRequestResult>(response.Result);
        }
        [Test]
        public async Task CheckPostRequest_ValidInputs()
        {
            ServiceRequest request = new ServiceRequest { Productid = 1, Userid = 1, Problem = "A", Description = "aaa", Status = "aaa", RequestDate = DateTime.Now };
            ServiceRequest requestFinal = new ServiceRequest { Id = 1, Productid = 1, Userid = 1, Problem = "A", Description = "aaa", Status = "aaa", RequestDate = DateTime.Now };

            _repository.Setup(x => x.AddRequest(request)).ReturnsAsync(requestFinal);

            //var response = await _controller.PostProduct(product);
            //Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            //var result = response.Result as CreatedAtActionResult;
            //var returedValue = result.Value as Product;
            //Assert.IsNotNull(returedValue.Id);

            var response = await _controller.AddRequest(request);
            Assert.IsInstanceOf<CreatedAtActionResult>(response.Result);
            var result = response.Result as CreatedAtActionResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedValue = converted.payload as ServiceRequest;
            //Assert.That(returedValue, Is.EqualTo(reportFinal));
            Assert.That(returedValue.Id, Is.EqualTo(requestFinal.Id));
        }
        [Test]
        public async Task CheckPostRequest_InvalidInputs()
        {
            ServiceRequest request = new ServiceRequest();

            _repository.Setup(x => x.AddRequest(request)).ReturnsAsync(request);

            var response = await _controller.AddRequest(request);
            Assert.IsInstanceOf<BadRequestResult>(response.Result);
        }

        [Test]
        public async Task CheckDeleteRequest_RequestPresent()
        {
            ServiceRequest request = new ServiceRequest { Id = 1, Productid = 1, Userid = 1, Problem = "A", Description = "aaa", Status = "aaa", RequestDate = DateTime.Now };

            _repository.Setup(x => x.DeleteRequest(request.Id)).ReturnsAsync(request);

            //var response = await _controller.DeleteProduct(product.Id);
            //Assert.IsInstanceOf<OkObjectResult>(response.Result);
            //var result = response.Result as OkObjectResult;
            //var returedProduct = result.Value as Product;
            //Assert.That(product.Id, Is.EqualTo(returedProduct.Id));
            //Assert.That(returedProduct, Is.EqualTo(product));

            var response = await _controller.DeleteRequest(request.Id);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
            var result = response.Result as OkObjectResult;
            ResponseObj converted = result.Value as ResponseObj;
            Assert.IsNotNull(converted.payload);
            var returedRequest = converted.payload as ServiceRequest;
            Assert.That(request.Id, Is.EqualTo(returedRequest.Id));
            Assert.That(returedRequest, Is.EqualTo(request));
        }

        [Test]
        public async Task CheckDeleteRequest_RequestMissing()
        {
            int id = 10;

            _repository.Setup(x => x.DeleteRequest(id)).ReturnsAsync((ServiceRequest)null);

            var response = await _controller.DeleteRequest(id);
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }

    }
}