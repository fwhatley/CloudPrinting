using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace CloudPrintingService.Models.mS.Common
{

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class Error2 {
    /// <summary>
    /// the code will be mapped to a specific message per microservice. The first 4 digits represent the microservice. Then the last 3 are for a specific error message. The codes will be stored and available to lookup in the wiki
    /// </summary>
    /// <value>the code will be mapped to a specific message per microservice. The first 4 digits represent the microservice. Then the last 3 are for a specific error message. The codes will be stored and available to lookup in the wiki</value>
    [DataMember(Name="code", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "code")]
    public int? Code { get; set; }

    /// <summary>
    /// this will be the technical message that will be passed along
    /// </summary>
    /// <value>this will be the technical message that will be passed along</value>
    [DataMember(Name="message", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }

    /// <summary>
    /// this will be the name of the call that you are making on the vendor's API
    /// </summary>
    /// <value>this will be the name of the call that you are making on the vendor's API</value>
    [DataMember(Name="method", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "method")]
    public string Method { get; set; }

    /// <summary>
    /// which vendor are we reaching out to with the call - SharedServices, iQMetrix, DAO, Cassandra
    /// </summary>
    /// <value>which vendor are we reaching out to with the call - SharedServices, iQMetrix, DAO, Cassandra</value>
    [DataMember(Name="vendor", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "vendor")]
    public string Vendor { get; set; }

    /// <summary>
    /// the http status code that came with the error
    /// </summary>
    /// <value>the http status code that came with the error</value>
    [DataMember(Name="status", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "status")]
    public int? Status { get; set; }

    /// <summary>
    /// array of data attributes and reasons they are invalid
    /// </summary>
    /// <value>array of data attributes and reasons they are invalid</value>
    [DataMember(Name="attributes", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "attributes")]
    public List<ErrorAttributes> Attributes { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Error2 {\n");
      sb.Append("  Code: ").Append(Code).Append("\n");
      sb.Append("  Message: ").Append(Message).Append("\n");
      sb.Append("  Method: ").Append(Method).Append("\n");
      sb.Append("  Vendor: ").Append(Vendor).Append("\n");
      sb.Append("  Status: ").Append(Status).Append("\n");
      sb.Append("  Attributes: ").Append(Attributes).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
