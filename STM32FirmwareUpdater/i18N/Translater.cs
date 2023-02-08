using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace STM32FirmwareUpdater.i18N
{
    class Translater : ITranslater
    {
        public string Trans(string key)
        {
            if (string.IsNullOrEmpty(key)) return string.Empty;
            if (Application.Current.Resources == null)
                return key;
            var value = Application.Current.Resources[key];
            if (value != null && value is string)
                return value.ToString();
            return key;
        }

        public string Trans(string key, params object[] args)
        {
            return string.Format(Trans(key), args);
        }
    }
}
