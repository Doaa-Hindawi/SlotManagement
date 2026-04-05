using System.Threading.Tasks;

namespace SlotManagement.Data;

public interface ISlotManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
