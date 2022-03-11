using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Haldan.Wpf.Helper
{
    [Serializable]
    public class InvalidVisionModel: Exception
    {
      //  public Visibility DisplayErrors { get; set; }
        public InvalidVisionModel(String message) : base(message)
        {

        }
    }
}
