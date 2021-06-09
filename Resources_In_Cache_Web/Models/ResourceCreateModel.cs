using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resources_In_Cache_Web.Models
{
    public class ResourceCreateModel
    {
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }
    }
}
