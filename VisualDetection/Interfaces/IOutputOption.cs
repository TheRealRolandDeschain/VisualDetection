using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualDetection.Interfaces
{
    public interface IOutputOption
    {
        string OptionTitle { get; }

        void OnTriggerStatusChanged(object source, EventArgs e);
    }
}
