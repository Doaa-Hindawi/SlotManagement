using Volo.Abp.Modularity;

namespace SlotManagement;

/* Inherit from this class for your domain layer tests. */
public abstract class SlotManagementDomainTestBase<TStartupModule> : SlotManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
