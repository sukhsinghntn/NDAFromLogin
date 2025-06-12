using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace DynamicFormsApp.Shared.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FormField>? Fields { get; set; }
    }
}
