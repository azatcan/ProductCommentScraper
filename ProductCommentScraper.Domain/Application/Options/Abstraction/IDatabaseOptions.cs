using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Domain.Application.Options.Abstraction
{
    public interface IDatabaseOptions
    {
        public string ProductCollectionName { get; set; }
        public string ProductFeatureCollectionName { get; set; }
        public string ReviewCollectionName { get; set; }
        public string SellSourceCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
