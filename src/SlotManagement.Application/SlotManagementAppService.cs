using SlotManagement.Localization;
using Volo.Abp.Application.Services;

namespace SlotManagement;

/* Inherit your application services from this class.
 */
public abstract class SlotManagementAppService : ApplicationService
{
    protected SlotManagementAppService()
    {
        LocalizationResource = typeof(SlotManagementResource);
    }
}
