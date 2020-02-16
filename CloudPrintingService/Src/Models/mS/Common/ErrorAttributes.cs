using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace CloudPrintingService.Models.mS.Common
{

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ErrorAttributes {
    /// <summary>
    /// Gets or Sets Attribute
    /// </summary>
    [DataMember(Name="attribute", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "attribute")]
    public string Attribute { get; set; }

    /// <summary>
    /// Gets or Sets Reason
    /// </summary>
    [DataMember(Name="reason", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "reason")]
    public string Reason { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ErrorAttributes {\n");
      sb.Append("  Attribute: ").Append(Attribute).Append("\n");
      sb.Append("  Reason: ").Append(Reason).Append("\n");
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
