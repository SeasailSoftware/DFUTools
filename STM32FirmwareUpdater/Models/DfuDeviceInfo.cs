using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STM32FirmwareUpdater.Models
{
    /// <summary>
    /// DFU设备
    /// </summary>
    public class DfuDeviceInfo
    {
        public int ID { get; set; }
        public string? Description { get; set; }
        public string? Path { get; set; }
    }
}
