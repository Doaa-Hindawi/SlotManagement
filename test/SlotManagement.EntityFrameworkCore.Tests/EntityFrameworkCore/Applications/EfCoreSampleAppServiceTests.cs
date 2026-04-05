using SlotManagement.Samples;
using Xunit;

namespace SlotManagement.EntityFrameworkCore.Applications;

[Collection(SlotManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<SlotManagementEntityFrameworkCoreTestModule>
{

}
