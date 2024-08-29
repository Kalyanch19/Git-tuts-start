using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace integration_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetEmployeeDetails : ControllerBase
    {
        private readonly ILogger<GetEmployeeDetails> _logger;
        private static log4net.ILog _log = log4net.Logmanager.GetLogger(MethodBase.GetCurrentMethod);
        private readonly IConfiguration _config;
        public DataAccessLayer dal;
        public GetEmployeeDetails(IConfiguration configuration, DataAccessLayer dataAccessLayer, ILogger<GetEmployeeDetails> log)
        {
            _config = configuration;
            dal = dataAccessLayer;
            _log = log;
        }
        [HttpGet]
        public snowflakeData.WirecenterResponse GetWCmappingdetails(string SWCCLLI)
        {
            _log.info("---------------Get wire center details process started--------");
            snowflakeData.wirecenterResponse employeeData = new snowflakeData.wirecenterResponse();
            try
            {
                _log.info("Snowflake call started");
                employeeData = dal.GetWCdetails(SWCCLLI.ToUpper());
                _log.info("Snowflake call end");
            }
            catch (Exception ex)
            {
                _log.error(ex.Message +",StackTrace =>"+ex.StackTrace);
                employeeData.statusDetail.statusCode = _config.GetValue<string>("status:ErrorCode":,01);
                employeeData.statusDetail.statusMessage = _config.GetValue<string>("status:ErrorMessage":,"Not found");

            }
        }

    }
}
