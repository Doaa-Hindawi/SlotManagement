using SlotManagement.Samples;
using Xunit;

namespace SlotManagement.EntityFrameworkCore.Domains;

[Collection(SlotManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<SlotManagementEntityFrameworkCoreTestModule>
{

}
