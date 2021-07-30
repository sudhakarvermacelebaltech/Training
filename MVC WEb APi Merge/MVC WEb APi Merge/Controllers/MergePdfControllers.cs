using iTextSharp.text.pdf;
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

namespace MVC_WEb_APi_Merge.Controllers
{

    public class MergePdfController : ApiController
    {
        
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
                    if (file["$content-type"] != "application/pdf")
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

                    using (var ms = new MemoryStream())
                    {
                        var outputDocument = new iTextSharp.text.Document();
                        var writer = new PdfCopy(outputDocument, ms);
                        outputDocument.Open();

                        foreach (var file in bytesarray)
                        {
                            var reader = new PdfReader(file);
                            for (var i = 1; i <= reader.NumberOfPages; i++)
                            {
                                writer.AddPage(writer.GetImportedPage(reader, i));
                            }
                            writer.FreeReader(reader);
                            reader.Close();
                        }

                        writer.Close();
                        outputDocument.Close();
                        var allPagesContent = ms.GetBuffer();
                        ms.Flush();

                        if (allPagesContent != null)
                        {
                            return new OkObjectResult(allPagesContent);
                        }
                        else
                        {
                            return new OkObjectResult("fail");
                        }
                    }

                }
                return new OkObjectResult("Unsupported File Format");

            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex.Message);
            }

        }
    }
}
