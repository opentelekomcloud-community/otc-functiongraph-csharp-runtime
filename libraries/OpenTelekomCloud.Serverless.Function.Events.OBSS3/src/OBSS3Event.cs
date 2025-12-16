namespace OpenTelekomCloud.Serverless.Function.Events.OBSS3
{

  using System;
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  /// <summary>
  /// <para>
  /// Triggers on OBS can only be used for FunctionGraphs in the main project, not in sub projects!
  /// </para>
  /// <para>
  /// For each OBS bucket, only one FunctionGrap can be triggered (no muliple FunctionGraphs listening on same bucket)
  /// </para>
  /// </summary>
  public class OBSS3Event
  {
    /// <summary>
    /// Gets and sets the records for the S3 event notification
    /// </summary>
    [JsonProperty("Records", NullValueHandling = NullValueHandling.Ignore)]
    public List<S3EventNotificationRecord> Records { get; set; }

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }

  /// <summary>
  /// This class contains the identity information for an S3 bucket.
  /// </summary>
  public class S3BucketEntity
  {
    /// <summary>
    /// bucket arn
    /// </summary>
    [JsonProperty("arn", NullValueHandling = NullValueHandling.Ignore)]
    public string Arn { get; set; }

    /// <summary>
    /// bucket name
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    /// customer-ID-of-the-bucket-owner
    /// </summary>
    [JsonProperty("ownerIdentity", NullValueHandling = NullValueHandling.Ignore)]
    public OwnerIdentityEntity OwnerIdentity { get; set; }
  }

  /// <summary>
  /// This class contains the information for an object in S3.
  /// </summary>
  public class S3ObjectEntity
  {
    /// <summary>
    /// object eTag
    /// </summary>
    [JsonProperty("eTag", NullValueHandling = NullValueHandling.Ignore)]
    public string ETag { get; set; }

    /// <summary>
    /// a string representation of a hexadecimal value used to determine event sequence, only used with PUTs and DELETEs
    /// </summary>
    [JsonProperty("sequencer", NullValueHandling = NullValueHandling.Ignore)]
    public string Sequencer { get; set; }

    /// <summary>
    /// object-key
    /// </summary>
    [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
    public string Key { get; set; }

    [JsonProperty("versionId", NullValueHandling = NullValueHandling.Ignore)]
    public string VersionId { get; set; }

    /// <summary>
    /// object-size in bytes
    /// </summary>
    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    public double Size { get; set; }
  }

  /// <summary>
  /// The class holds the user identity properties.
  /// </summary>
  public class OwnerIdentityEntity
  {
    /// <summary>
    /// customer-ID-of-the-bucket-owner
    /// </summary>
    [JsonProperty("PrincipalId", NullValueHandling = NullValueHandling.Ignore)]
    public string PrincipalId { get; set; }
  }

  /// <summary>
  /// The class holds the event notification.
  /// </summary>
  public class S3EventNotificationRecord
  {
    /// <summary>
    /// event version: 2.0
    /// </summary>
    [JsonProperty("eventVersion", NullValueHandling = NullValueHandling.Ignore)]
    public string EventVersion { get; set; }

    /// <summary>
    /// The time, in ISO-8601 format, for example, 1970-01-01T00:00:00.000Z, when OTC finished processing the request
    /// </summary>
    [JsonProperty("eventTime", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime EventTime { get; set; }

    /// <summary>
    /// Gets and sets the RequestParameters property.
    /// </summary>
    [JsonProperty("requestParameters", NullValueHandling = NullValueHandling.Ignore)]
    public RequestParametersEntity RequestParameters { get; set; }

    /// <summary>
    /// Gets and sets the S3 property.
    /// </summary>
    [JsonProperty("s3", NullValueHandling = NullValueHandling.Ignore)]
    public S3Entity S3 { get; set; }

    /// <summary>
    /// Gets and sets the AwsRegion property.
    /// </summary>
    [JsonProperty("awsRegion", NullValueHandling = NullValueHandling.Ignore)]
    public string AwsRegion { get; set; }

    /// <summary>
    /// event type:
    /// <list type="bullet">
    /// <listheader>
    /// <term>ObjectCreated:Put, ObjectCreated:Post, ObjectCreated:Copy </term>
    /// <description>Operations such as PUT, POST, and COPY can create an object.
    /// With these event types, you can enable notifications when an object is created using a specific API operation.
    /// </description>
    /// </listheader>
    /// <item>
    /// <term>Created:CompleteMultipartUpload</term>
    /// <description>ObjectCreated:CompleteMultipartUpload includes objects that are created using UploadPartCopy for Copy operations.
    /// </description>
    /// </item>
    /// <item>
    /// <term>ObjectRemoved:Delete, ObjectRemoved:DeleteMarkerCreated</term>
    /// <description>By using the ObjectRemoved event types, you can enable notification 
    /// when an object or a batch of objects is removed from a bucket.
    /// You can request notification when an object is deleted or a versioned object is permanently 
    /// deleted by using the s3:ObjectRemoved:Delete event type. 
    /// Alternatively, you can request notification when a delete marker is created for a versioned 
    /// object using s3:ObjectRemoved:DeleteMarkerCreated
    /// These event notifications don't alert you for automatic deletes from lifecycle 
    /// configurations or from failed operations.</description>
    /// </item>
    /// </list>
    /// </summary>
    [JsonProperty("eventName", NullValueHandling = NullValueHandling.Ignore)]
    public string EventName { get; set; }

    [JsonProperty("userIdentity", NullValueHandling = NullValueHandling.Ignore)]
    public UserIdentityEntity UserIdentity { get; set; }

    /// <summary>
    /// Gets and sets the EventSource property.
    /// </summary>
    [JsonProperty("eventSource", NullValueHandling = NullValueHandling.Ignore)]
    public string EventSource { get; set; }

    /// <summary>
    /// Gets and sets the ResponseElements property.
    /// </summary>
    [JsonProperty("responseElements", NullValueHandling = NullValueHandling.Ignore)]
    public ResponseElementsEntity ResponseElements { get; set; }

  }

  /// <summary>
  /// The class holds the request parameters
  /// </summary>
  public class RequestParametersEntity
  {
    /// <summary>
    /// ip-address-where-request-came-from
    /// </summary>
    [JsonProperty("sourceIPAddress", NullValueHandling = NullValueHandling.Ignore)]
    public string SourceIPAddress { get; set; }
  }

  /// <summary>
  /// This class holds the response elements.
  /// </summary>
  public class ResponseElementsEntity
  {
    /// <summary>
    ///  S3 generated request ID
    /// </summary>
    [JsonProperty("x-amz-request-id", NullValueHandling = NullValueHandling.Ignore)]
    public string XAmzRequestId { get; set; }

    /// <summary>
    ///  S3 host that processed the request
    /// </summary>
    [JsonProperty("x-amz-id-2", NullValueHandling = NullValueHandling.Ignore)]
    public string XAmzId2 { get; set; }
  }

  public class S3Entity
  {
    /// <summary>
    /// Schema Version: 1.0
    /// </summary>
    [JsonProperty("s3SchemaVersion", NullValueHandling = NullValueHandling.Ignore)]
    public string S3SchemaVersion { get; set; }

    /// <summary>
    /// Gets and sets the ConfigurationId. This ID can be found in the bucket notification configuration.
    /// </summary>
    [JsonProperty("configurationId", NullValueHandling = NullValueHandling.Ignore)]
    public string ConfigurationId { get; set; }

    /// <summary>
    /// Gets and sets the Object property.
    /// </summary>
    [JsonProperty("object", NullValueHandling = NullValueHandling.Ignore)]
    public S3ObjectEntity Object { get; set; }

    /// <summary>
    /// Gets and sets the Bucket property.
    /// </summary>
    [JsonProperty("bucket", NullValueHandling = NullValueHandling.Ignore)]
    public S3BucketEntity Bucket { get; set; }

  }

  /// <summary>
  /// The class holds the user identity properties.
  /// </summary>
  public class UserIdentityEntity
  {
    /// <summary>
    /// customer-ID-of-the-user-who-caused-the-event
    /// </summary>
    [JsonProperty("principalId", NullValueHandling = NullValueHandling.Ignore)]
    public string PrincipalId { get; set; }
  }
}