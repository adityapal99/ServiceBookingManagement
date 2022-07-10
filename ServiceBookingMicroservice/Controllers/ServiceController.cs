using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBooking.Models;
using ServiceBooking.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ServiceBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ServiceController));
        private readonly IServiceRepository _repository;
        public ServiceController(IServiceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ServiceRequest>> GetRequests()
        {
            try
            {
                _log4net.Info("GetRequests Method Called");
                IEnumerable<ServiceRequest> requests = _repository.GetRequests();
                return Ok(new { status = StatusCodes.Status200OK, msg = "All Requests", payload = requests });
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }

        [HttpGet("report")]
        [Authorize]
        public ActionResult<IEnumerable<ServiceReport>> GetReports()
        {
            try
            {
                _log4net.Info("GetReports Method Called");
                IEnumerable<ServiceReport> reports = _repository.GetReports();
                return Ok(new { status = StatusCodes.Status200OK, msg = "All Reports", payload = reports });
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ServiceRequest> GetRequestById(int id)
        {
            try
            {
                _log4net.Info("GetRequestById Method Called");
                ServiceRequest request = _repository.GetRequestById(id);
                if (request == null)
                {
                    return NotFound();
                }
                return Ok(new { status = StatusCodes.Status200OK, msg = "Request Found", payload = request });
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }
        [HttpGet("report/{id}")]
        [Authorize]
        public ActionResult<ServiceReport> GetReporttById(int id)
        {
            try
            {
                _log4net.Info("GetReporttById Method Called");
                ServiceReport report = _repository.GetReportById(id);
                if (report == null)
                {
                    return NotFound();
                }
                return Ok(new { status = StatusCodes.Status200OK, msg = "Report Found", payload = report });
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult<ServiceRequest> AddRequest(ServiceRequest serviceRequest)
        {
            try
            {
                _log4net.Info("AddRequest Method Called");
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                _repository.AddRequest(serviceRequest);
                return CreatedAtAction("AddRequest", new { status = StatusCodes.Status201Created, msg = "Request Added", payload = serviceRequest });
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }

        [HttpPost("report")]
        [Authorize]
        public ActionResult<ServiceReport> AddReport(ServiceReport serviceReport)
        {
            try
            {
                _log4net.Info("AddReport Method Called");
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                _repository.AddReport(serviceReport);
                return CreatedAtAction("AddReport", new { status = StatusCodes.Status201Created, msg = "Request Added", payload = serviceReport }); ;
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<ServiceRequest> UpdateRequest(int id, ServiceRequest serviceRequest)
        {
            try
            {
                _log4net.Info("UpdateRequest Method Called");
                if ((!ModelState.IsValid) || (id != serviceRequest.Id))
                {
                    return BadRequest();
                }
                serviceRequest = _repository.UpdateRequest(id, serviceRequest);
                return CreatedAtAction("UpdateRequest", new { status = StatusCodes.Status202Accepted, msg = "Request Updated", payload = serviceRequest });
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }
        [HttpPut("report/{id}")]
        [Authorize]
        public ActionResult<ServiceReport> UpdateReport(int id, ServiceReport serviceReport)
        {
            try
            {
                _log4net.Info("UpdateReport Method Called");
                if ((!ModelState.IsValid) || (id != serviceReport.Id))
                {
                    return BadRequest();
                }
                serviceReport = _repository.UpdateReport(id, serviceReport);
                return CreatedAtAction("UpdateReport", new { status = StatusCodes.Status202Accepted, msg = "Request Updated", payload = serviceReport });
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<ServiceRequest> DeleteRequest(int id)
        {
            try
            {
                _log4net.Info("DeleteRequest Method Called");
                ServiceRequest serviceRequest = _repository.DeleteRequest(id);
                if (serviceRequest == null)
                {
                    return NotFound();
                }
                return Ok(new { status = StatusCodes.Status204NoContent, msg = "Deleted Successfully", payload = serviceRequest });
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }

        [HttpDelete("report/{id}")]
        [Authorize]
        public ActionResult<ServiceReport> DeleteReport(int id)
        {
            try
            {
                _log4net.Info("DeleteReport Method Called");
                ServiceReport serviceReport = _repository.DeleteReport(id);
                if (serviceReport == null)
                {
                    return NotFound();
                }
                return Ok(new { status = StatusCodes.Status204NoContent, msg = "Deleted Successfully", payload = serviceReport });
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }

        [HttpGet("report/user/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<ServiceReport>> GetReportsByUserId(int id)
        {
            try
            {
                _log4net.Info("GetReportsByUserId Method Called");
                var reports = _repository.GetReportsByUserId(id);
                return Ok(new { status = StatusCodes.Status200OK, msg = "All Reports", payload = reports });
            }
            catch
            {
                _log4net.Error("Database Error");
                return StatusCode(500);
            }
        }
    }
}
