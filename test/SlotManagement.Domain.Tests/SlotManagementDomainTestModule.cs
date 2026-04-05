using Volo.Abp.Modularity;

namespace SlotManagement;

[DependsOn(
    typeof(SlotManagementDomainModule),
    typeof(SlotManagementTestBaseModule)
)]
public class SlotManagementDomainTestModule : AbpModule
{

}
