using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MVC_WEb_APi_Merge.Controllers
{


    public class MergeWordController : ApiController
    {
        
        //public async Task<OkObjectResult> PostAsync(HttpRequestMessage httpRequest)
        //{
        //    try
        //    {
        //        var Request = await httpRequest.Content.ReadAsStreamAsync();
        //        string requestBody = await new StreamReader(Request).ReadToEndAsync();
        //        dynamic input = JsonConvert.DeserializeObject(requestBody);
        //        dynamic files = input.file;

        //        //  byte[] cont = cnt["$content"];


        //        bool ext = true;
        //        foreach (var file in files)
        //        {
        //            if (file["$content-type"] != "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
        //            {
        //                ext = false;
        //            }
        //        }

        //        if (ext)
        //        {
        //            byte[][] bytesarray = new byte[files.Count][];
        //            int j = 0;
        //            foreach (var file in files)
        //            {
        //                bytesarray[j] = file["$content"];
        //                j++;
        //            }

        //            List<Source> documentBuilderSources = new List<Source>();
        //            foreach (byte[] documentByteArray in bytesarray)
        //            {
        //                documentBuilderSources.Add(new Source(new WmlDocument(string.Empty, documentByteArray), false));
        //            }

        //            WmlDocument mergedDocument = DocumentBuilder.BuildDocument(documentBuilderSources);
        //            var st = mergedDocument.DocumentByteArray;

        //            if (st != null)
        //            {

        //                return new OkObjectResult(st);
        //            }
        //            else
        //            {
        //                return new OkObjectResult("fail");
        //            }
        //        }

        //        return new OkObjectResult("Unsupported File Format");

        //    }
        //    catch (Exception ex)
        //    {
        //        return new OkObjectResult(ex.Message);
        //    }

        //}
    }
}
