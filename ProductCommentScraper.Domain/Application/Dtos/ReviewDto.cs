using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Domain.Application.Dtos
{
    public class ReviewDto
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public int Rating { get; set; }
        public string Site { get; set; } = null!;
    }
}
