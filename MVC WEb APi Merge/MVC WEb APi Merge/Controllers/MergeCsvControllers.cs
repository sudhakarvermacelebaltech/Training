using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_WEb_APi_Merge.Controllers
{


    public class MergeCsvController : ApiController
    {
        // GET: api/<ValuesController>

        
        public async Task<OkObjectResult> PostAsync(HttpRequestMessage httpRequest)
        {
            try
            {

                var Request = await httpRequest.Content.ReadAsStreamAsync();
                string requestBody = await new StreamReader(Request).ReadToEndAsync();
                dynamic input = JsonConvert.DeserializeObject(requestBody);
                dynamic files = input.file;

                //  byte[] cont = cnt["$content"];


                bool ext = true;
                foreach (var file in files)
                {
                    if (file["$content-type"] != "text/csv")
                    {
                        ext = false;
                    }
                }

                if (ext)
                {
                    byte[][] bytesarray = new byte[files.Count][];
                    int j = 0;
                    foreach (var file in files)
                    {
                        bytesarray[j] = file["$content"];
                        j++;
                    }

                    byte[] rv = new byte[bytesarray.Sum(a => a.Length)];
                    int offset = 0;
                    foreach (byte[] array in bytesarray)
                    {
                        System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                        offset += array.Length;
                    }

                    if (rv != null)
                    {
                        //System.IO.File.WriteAllBytes(@"C:\Users\Sahil\Desktop\hello.csv", rv);
                        return new OkObjectResult(rv);
                    }
                    else
                    {
                        return new OkObjectResult("error");
                    }

                }
                return new OkObjectResult("unsupported file formate");
            }
            catch (Exception ex)
            {

                return new OkObjectResult(ex.Message);
            }

        }
    }
}
