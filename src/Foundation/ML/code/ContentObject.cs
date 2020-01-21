using System;

namespace Hackathon.Boilerplate.Foundation.ML
{
    public class ContentObject
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public double[] TextVector { get; set; }
        public double Distance { get; set; }
        public string[] Tags { get; set; }
    }
}
