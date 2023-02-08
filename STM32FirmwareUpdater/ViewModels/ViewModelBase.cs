using Caliburn.Micro;
using STM32FirmwareUpdater.i18N;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STM32FirmwareUpdater.ViewModels
{
    internal class ViewModelBase : Screen
    {
        public ITranslater Translater => IoC.Get<ITranslater>();
    }
}
