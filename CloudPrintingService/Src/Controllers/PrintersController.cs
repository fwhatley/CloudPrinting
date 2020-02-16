using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using FileHubBackendV2.Models;
using FileHubBackendV2.Models.mS.Common;
using FileHubBackendV2.Utils;
using Microsoft.AspNetCore.Mvc;
using StarMicronics.CloudPrnt;
using StarMicronics.CloudPrnt.CpMessage;

namespace FileHubBackendV2.Controllers
{

[Route("api/printers")]
    [ApiController]
    public class PrintersController : ControllerBase
    {

        // 400 - query param mac required
        // 400 - query param type req
        // GET api/printers
        [HttpGet]
        public void GetPrintJob()
        {
            var macAddress = HttpContext.Request.Query["mac"].FirstOrDefault();
            var printJobQueueItem = new PrintJobQueueItem();
            printJobQueueItem.receipt = "this is smaple receip";
            
            const string defaultPrintMediaType = "application/vnd.star.linematrix";
            var outputFormat = HttpContext.Request.Query["type"].FirstOrDefault() ?? defaultPrintMediaType;
            HttpContext.Response.ContentType = outputFormat;

            if (string.IsNullOrEmpty(macAddress)) 
            {
                var err = GetErrorParamIsMissing(macAddress, "GetPrintJob");
                HttpContext.Response.StatusCode = 400;
                Document.Convert(GetErrorObjToByeArray(err), "text/vnd.star.markup", HttpContext.Response.Body, outputFormat, null);
            }
            else if (printJobQueueItem == null)
            {
                var err = ResourceNotFound(macAddress, "GetPrintJob");
                HttpContext.Response.StatusCode = 404;
                Document.Convert(GetErrorObjToByeArray(err), "text/vnd.star.markup", HttpContext.Response.Body, outputFormat, null);
            }
            else
            {
                // set the response media type, and output the converted job to the response body
                var docBytes = printJobQueueItem.GetReceiptStarMarkupDoc();
                Document.Convert(docBytes, "text/vnd.star.markup", HttpContext.Response.Body, outputFormat, null);
            }
        }

        // 400 - pollRequest is required
        // POST api/printers
        // HandleCloudPRNTPoll
        // https://star-m.jp/products/s_print/CloudPRNTSDK/Documentation/en/articles/api/usage.html
        [HttpPost]
        public IActionResult ProcessPrinterStatus([FromBody] PollRequest pollRequest)
        {
            if (pollRequest == null)
                return BadRequest("sadfasfasasdsdafasdf");

            // todo: if job is in progress, skip checking db to save time and return
            var pollResponse = new PollResponse {mediaTypes = new List<string>()};
            pollResponse.mediaTypes.AddRange(Document.GetOutputTypesFromType("text/vnd.star.markup"));
            pollResponse.jobReady = true;
            return Ok(pollResponse.ToJson());
        }

        // 400 - printjobQueueIsRequired
        // POST api/printers/enqueuePrintJob
        [HttpPost("enqueuePrintJob")]
        public IActionResult EnqueuePrintJob([FromBody] PrintJobQueueItem printJobQueueItem)
        {
            if (printJobQueueItem == null)
                return BadRequest(GetErrorPayloadIsMissing("printJobQueueItem", "EnqueuePrintJob"));
            
            // var addedItem = _printersService.EnqueuePrintJob(printJobQueueItem);
            return Ok();
        }

        // 400 - mac is required
        // DELETE api/printers
        [HttpDelete()]
        public IActionResult DeletePrintJobQueueItem()
        {
            var macAddress = HttpContext.Request.Query["mac"].FirstOrDefault();
            if (string.IsNullOrEmpty(macAddress))
                return BadRequest(GetErrorParamIsMissing("mac", "DeletePrintJobQueueItem"));
            
            return Ok();
        }

        private Error GetErrorParamIsMissing(string paramName, string methodName)
        {
            // ref: https://www.exceptionnotfound.net/the-asp-net-web-api-exception-handling-pipeline-a-guided-tour/
            var err = new Error(ErrorStatusCode._1234123, 
                $"Required parameter {paramName} is missing.", 
                methodName, "CloudPrintService", 400, new List<ErrorAttributes>());
            return err;
        }
        
        private Error GetErrorPayloadIsMissing(string payloadName, string methodName)
        {
            var err = new Error(ErrorStatusCode._1234123, 
                $"Required payload {payloadName} is missing.", 
                methodName, "CloudPrintService", 400, new List<ErrorAttributes>());
            return err;
        }
        
        private Error ResourceNotFound(string text, string methoName)
        {
            var err = new Error(ErrorStatusCode._1234123, 
                $"Resource not found for {text}", 
                methoName, "CloudPrintService", 404, new List<ErrorAttributes>());
            return err;
        }
        
        private static byte[] GetErrorObjToByeArray(Error err)
        {
            var ms = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(Error));
            ser.WriteObject(ms, err);
            byte[] body = ms.ToArray();
            ms.Close();
            return body;
        }
    }
}