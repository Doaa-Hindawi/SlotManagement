using Volo.Abp.Modularity;

namespace SlotManagement;

public abstract class SlotManagementApplicationTestBase<TStartupModule> : SlotManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
