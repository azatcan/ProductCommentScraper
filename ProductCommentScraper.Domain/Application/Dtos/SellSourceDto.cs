using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Domain.Application.Dtos
{
    public class SellSourceDto
    {
        public string Id { get; set; } = null!;
        public string Site { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string ProductUrl { get; set; } = null!;
    }
}
