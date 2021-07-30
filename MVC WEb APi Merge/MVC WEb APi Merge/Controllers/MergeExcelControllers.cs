using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MVC_WEb_APi_Merge.Controllers
{

    public class MergeExcelController : ApiController
    {
        
        public async Task<OkObjectResult> PostAsync(HttpRequestMessage httpRequest)
        {
            try
            {
                var Request = await httpRequest.Content.ReadAsStreamAsync();
                string requestBody = await new StreamReader(Request).ReadToEndAsync();
                dynamic input = JsonConvert.DeserializeObject(requestBody);
                dynamic files = input.file;
                bool ext = true;
                foreach (var file in files)
                {
                    if (files.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
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

                    DataSet ds = new DataSet();
                    foreach (var postedFile in bytesarray)
                    {

                        MemoryStream ms = new MemoryStream(postedFile);
                        var stream = new StreamReader(ms);

                        using (XLWorkbook workBook = new XLWorkbook(ms))
                        {
                            IXLWorksheet workSheet = workBook.Worksheet(1);


                            DataTable dt = new DataTable();
                            bool firstRow = true;
                            foreach (IXLRow row in workSheet.Rows())
                            {
                                if (firstRow)
                                {
                                    foreach (IXLCell cell in row.Cells())
                                    {
                                        dt.Columns.Add(cell.Value.ToString());
                                    }
                                    firstRow = false;
                                }
                                else
                                {
                                    if (row.Cells().Count() > 1)
                                        dt.Rows.Add();
                                    int i = 0;
                                    foreach (IXLCell cell in row.Cells())
                                    {
                                        dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                        i++;
                                    }
                                }
                            }
                            firstRow = true;

                            ds.Tables.Add(dt);
                        }
                    }

                    DataTable dtMerge = ds.Tables[0].Clone();
                    foreach (DataTable table in ds.Tables)
                    {
                        dtMerge.Merge(table);
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dtMerge);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            System.IO.File.WriteAllBytes(@"C:\Users\Sahil\Desktop\hello.xlsx", stream.ToArray());
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex.Message);
            }


        }
    }
}
