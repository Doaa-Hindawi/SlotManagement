using Volo.Abp.Modularity;

namespace SlotManagement;

[DependsOn(
    typeof(SlotManagementApplicationModule),
    typeof(SlotManagementDomainTestModule)
)]
public class SlotManagementApplicationTestModule : AbpModule
{

}
