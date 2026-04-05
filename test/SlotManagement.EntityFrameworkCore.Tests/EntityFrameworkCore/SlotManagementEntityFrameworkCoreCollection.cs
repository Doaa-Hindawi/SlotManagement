using Xunit;

namespace SlotManagement.EntityFrameworkCore;

[CollectionDefinition(SlotManagementTestConsts.CollectionDefinitionName)]
public class SlotManagementEntityFrameworkCoreCollection : ICollectionFixture<SlotManagementEntityFrameworkCoreFixture>
{

}
