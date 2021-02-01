using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Elmah.AspNetMvcCoreViewModel
{
    /// <summary>
    /// </summary>
    public partial class DashboardVM : Elmah.ViewModelData.DashboardVM
    {
        private IServiceProvider ServiceProvider { get; set; }
        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public DashboardVM()
        {
        }

        public DashboardVM(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public async Task LoadData(
            long entityID
            )
        {
            var tasks = new List<Task>();

            tasks.Add(Task.Run((Func<Task>)(async () =>
            {
                using (var scope = this.ServiceProvider.CreateScope())
                {
                    this.SomeData = await Task.FromResult("Welcome");
                }
            })));

            /*
            tasks.Add(Task.Run(async () =>
            {
                using (var scope = this.ServiceProvider.CreateScope())
                {
                    var request = new ??();
                    request.??.SlaveEntityID.NullableValueToCompare = ??;
                    var thisService = scope.ServiceProvider.GetRequiredService<??>();
                    this.MembershipResponse = await thisService.Get??(request, new Framework.Queries.QueryPagingSetting(1, 20), new Framework.Queries.QueryOrderBySettingCollection("ModifiedDate~DESC"));
                }
            }));
            */

            //tasks.Add(Task.Run(async () =>
            //{
            //    using (var scope = this.ServiceProvider.CreateScope())
            //    {
            //    }
            //}));

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

