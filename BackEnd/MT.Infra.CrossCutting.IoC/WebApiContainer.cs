using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Infra.CrossCutting.IoC
{
    internal class WebApiContainer
    {
        public static void IniciarContainer(ref Container container)
        {
            //container.Options.PropertySelectionBehavior = new ImportPropertySelectionBehavior();
            container.Options.DefaultScopedLifestyle = new SimpleInjector.Lifestyles.AsyncScopedLifestyle();
            container.Options.DefaultLifestyle = Lifestyle.Scoped;

        }

    }
}
