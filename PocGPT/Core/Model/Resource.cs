using Newtonsoft.Json;

namespace PocGPT.Core.Model
{
    public class Resource
    {
        public int? Total { get; set; }
        public string? ItemType { get; set; }
        public List<Item>? Items { get; set; }
    }
}
