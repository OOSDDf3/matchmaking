using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApplication
{
    class GenderOptions : ObservableCollection<string>
    {
        public GenderOptions() 
        {
            Add("Male");        
            Add("Female");        
            Add("Other");        
        }
    }
}
