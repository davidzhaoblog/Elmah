using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Resx
{
    public static class ResourceFileFactory
    {
        public static Elmah.Resx.UIStringResourcePerApp GetUIStringResourcePerAppInstance()
        {
            return new Elmah.Resx.UIStringResourcePerApp();
        }

        public static Elmah.Resx.UIStringResourcePerEntity GetUIStringResourcePerEntityInstance()
        {
            return new Elmah.Resx.UIStringResourcePerEntity();
        }
    }
}

