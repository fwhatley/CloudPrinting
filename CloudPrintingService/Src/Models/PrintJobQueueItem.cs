using System;
using System.Text;

namespace CloudPrintingService.Models
{
    public class PrintJobQueueItem
    {
        public string MacId { get; set; }
        public DateTime InsertedTimeUtc { get; set; }
        public string receipt { get; set; }
        public string ReceiptId { get; set; }

        public byte[] GetReceiptStarMarkupDoc()
        {
            return Encoding.UTF8.GetBytes("this is a sample receipt");
        }
    }
}
