using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Elmah.AspNetMvcCoreViewModel
{
    public partial class SystemDashboardVM : Elmah.ViewModelData.SystemDashboardVM
    {
        private IServiceProvider ServiceProvider { get; set; }
        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public SystemDashboardVM()
        {
        }

        public SystemDashboardVM(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public async Task LoadData(

            )
        {
            var tasks = new List<Task>();

            if (tasks.Count > 0)
            {
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }
            }
        }
    }
}

