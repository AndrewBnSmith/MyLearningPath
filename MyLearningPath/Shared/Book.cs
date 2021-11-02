using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearningPath.Shared
{
   public class Book
    {
        public int Id { get; set; } = 0;

        public string Title { get; set; }

        public string Author { get; set; }

        public BookType BookType { get; set; }

        public int BookTypeId { get; set; }



    }
}
