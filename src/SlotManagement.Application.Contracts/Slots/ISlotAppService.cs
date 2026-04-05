using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SlotManagement.Slots
{
    public interface ISlotAppService : IApplicationService
    {
        Task<GenerateSlotsResultDto> GenerateAsync(GenerateSlotsInput input);
        Task<List<SlotDto>> GetNextAsync(string timeZone, int count = 20);
    }
}
