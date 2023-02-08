using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STM32FirmwareUpdater.Models
{
    public class DfuProcessExitedMessage
    {
        public object? Sender { get; set; }

        public DfuProcessExitedMessage(object? sender)
        {
            Sender = sender;
        }
    }
}
