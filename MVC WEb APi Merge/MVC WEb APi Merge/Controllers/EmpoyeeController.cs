using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http; 

namespace MVC_WEb_APi_Merge.Controllers
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class EmployeeService
    {
        public static List<EmployeeDto> employees = new List<EmployeeDto>() { new EmployeeDto(){
        Id=1,
        Name="test"} };
    }
    public class WebhookConfig
    {
        public static string CallBackUrl { get; set; }
      

    }
    public class WebhookRegDto
    {
        public string CallBackUrl { get; set; }
    }
    public class EmpoyeeController : ApiController
    {
       
        [HttpGet]
        [Route("api/Employee/getUrl")]
        public string getUrl(string url)
        {
            return WebhookConfig.CallBackUrl;
        }

        // GET api/<controller>
        [HttpGet]
        [Route("api/Employee/GetEmployee")]
        public IEnumerable<EmployeeDto> Get()
        {
            return EmployeeService.employees;
        }

        [HttpGet]
        [Route("api/Employee/GetEmployee")]
        public EmployeeDto Get(int id)
        {

            return EmployeeService.employees.Where(it => it.Id == id).FirstOrDefault();
        }
        private  void SentNotifications()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), WebhookConfig.CallBackUrl))
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(EmployeeService.employees));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = httpClient.SendAsync(request);
                }
            }

        }
        // POST api/<controller>
        [HttpPost]
        [Route("api/Employee/RegisterWebhook")]
        public void Post([FromBody] WebhookRegDto callUrl)
        {
            WebhookConfig.CallBackUrl = callUrl?.CallBackUrl;
        }
        
        [HttpPost]
        [Route("api/Employee/AddEmployee")]
        public void AddEmployee([FromBody] EmployeeDto employee)
        {
            EmployeeService.employees.Add(employee);
            try
            {
                SentNotifications();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
     
       
         
    }
}