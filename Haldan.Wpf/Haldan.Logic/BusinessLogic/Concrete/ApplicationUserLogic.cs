using Entities.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Haldan.Logic.BusinessLogic.Concrete
{
    public class ApplicationUserLogic : IApplicationUserLogic
    {
        private ILog log;
        public ApplicationUserLogic(ILog log)
        {
        this.log = log; 

        }
        public async Task<ResponseRegister> PostRegUser(ApplicationUser requestObj)
        {
            // Initialization.  
            ResponseRegister responseObj = new ResponseRegister();

            try
            {
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    client.BaseAddress = new Uri("http://www.autoediportal.com/AutoEDI/");

                    // Setting content type.  
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = await client.PostAsJsonAsync("Api/v1/TestRegister.php", requestObj).ConfigureAwait(false);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonConvert.DeserializeObject<ResponseRegister>(result);
                        responseObj.message = "success";
                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.
                       responseObj.message = "error";
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj.errors = 602;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseObj;
        }

        public  bool SaveUser(ApplicationUser user)
        {
            try
            {
                //string queryCheck = "SELECT * FROM [Users] WHERE [Users].[Username]= " + user.Username;
                //DAL.executeQuery(queryCheck);

                //if (string.IsNullOrEmpty(queryCheck)) { 
                // Query.  
                string queryInsert = "INSERT INTO [Users] ([FirstName],[LastName], [Username],[AccessLevel],[],[],[])" +
                                " Values ('" + user.FirstName + ",'"+ user.LastName +"',"+ user.Username + "',"+ user.AccessLevel+ "')";

                // Execute.                 
                   DAL.executeQuery(queryInsert);
              
              return true;
            }
            catch (Exception ex)
            {
                this.log.Error(ex.Message, ex);
               
                return false;

               
            }

        }

    }
}
