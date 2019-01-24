using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotyApp.ViewModels
{
    public class RadioVM
    {
        public int CurrentNumberOfListeners { get; set; }
        public IList<string> Albums { get; set; }
        public IList<string> Artists { get; set; }
    }
}
