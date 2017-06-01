using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Infra.CrossCutting.IoC
{
    internal class MvcContainer
    {

        public static void IniciarContainer(ref Container container)
        {
            //container.Options.PropertySelectionBehavior = new ImportPropertySelectionBehavior();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Options.DefaultLifestyle = Lifestyle.Scoped;
        }
    }
}
