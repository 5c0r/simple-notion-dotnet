using Notion.Net.Service;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Notion.Net.Models
{
    public class ParentMetadata
    {
        public string Type { get; set; }

        [JsonPropertyName("database_id")]
        public Guid DatabaseId { get; set; }
    }

    public sealed class NotionObjectTypes
    {
        public static readonly string Database = "database";
        public static readonly string Page = "page";
        public static readonly string List = "list";
        public static readonly string Text = "text";
    }

    public sealed class NotionPropertyTypes
    {
        public static readonly string Test = "test";
    }

    public class NotionObject
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("object")]
        public string Type { get; set; }
        [JsonPropertyName("created_time")]
        public DateTimeOffset CreatedTime { get; set; }
        [JsonPropertyName("last_edited_time")]
        public DateTimeOffset LastEditedTime { get; set; }

        //[JsonConverter(typeof(NotionPropertyConverter))]
        [JsonPropertyName("properties")]
        public Dictionary<string, object> Properties { get; set; }
    }

    public class NotionPage : NotionObject
    {
        [JsonPropertyName("archived")]
        public bool Archived { get; set; }
        [JsonPropertyName("parent")]
        public ParentMetadata Parent { get; set; }
    }

    public class NotionDatabase : NotionObject
    {

    }

    public class NotionList : NotionObject
    {
        [JsonPropertyName("results")]

        public IEnumerable<NotionObject> Results { get; set; }
    }

    public class BaseProperty
    {
        // TODO: Very questionable decision from Notion
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }

    }

}
