using Microsoft.EntityFrameworkCore;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Timing;

namespace SlotManagement.Slots;

public class SlotsAppService : ApplicationService, ISlotAppService
{
    private readonly IRepository<Slot, Guid> _slotRepository;
    private readonly Volo.Abp.Timing.IClock _clock;

    public SlotsAppService(
        IRepository<Slot, Guid> slotRepository,
        Volo.Abp.Timing.IClock clock)
    {
        _slotRepository = slotRepository;
        _clock = clock;
    }

    public async Task<GenerateSlotsResultDto> GenerateAsync(GenerateSlotsInput input)
    {
        if (!NodaTime.Text.LocalDatePattern.Iso.Parse(input.StartDate).Success ||
            !NodaTime.Text.LocalDatePattern.Iso.Parse(input.EndDate).Success)
        {
            throw new UserFriendlyException("Invalid date format.");
        }

        var startDateResult = NodaTime.Text.LocalDatePattern.Iso.Parse(input.StartDate);
        var endDateResult = NodaTime.Text.LocalDatePattern.Iso.Parse(input.EndDate);
        var startDate = startDateResult.Value;
        var endDate = endDateResult.Value;

        if (startDate > endDate)
        {
            throw new UserFriendlyException("Start date must be before or equal to end date.");
        }

        var tz = DateTimeZoneProviders.Tzdb.GetZoneOrNull(input.TimeZone);
        if (tz == null)
            throw new UserFriendlyException("Invalid time zone identifier.");

        if (input.SlotDuration <= 0)
            throw new UserFriendlyException("Slot duration must be greater than 0.");

        var duration = Duration.FromMinutes(input.SlotDuration);
        var slots = new List<Slot>();

        for (var date = startDate; date <= endDate; date = date.PlusDays(1))
        {
            var startOfDay = date.AtStartOfDayInZone(tz);
            var endOfDay = date.PlusDays(1).AtStartOfDayInZone(tz);

            var current = startOfDay;

            while (true)
            {
                var slotEnd = current.Plus(duration);

                if (ZonedDateTime.Comparer.Instant.Compare(slotEnd, endOfDay) > 0)
                    break;

                slots.Add(new Slot(
                    GuidGenerator.Create(),
                    current.ToInstant(),
                    slotEnd.ToInstant(),
                    input.TimeZone,
                    SlotStatus.Available
                ));

                current = slotEnd;
            }
        }

        if (slots.Any())
        {
            await _slotRepository.InsertManyAsync(slots);
        }

        return new GenerateSlotsResultDto { TotalSlotsCreated = slots.Count };
    }

    public async Task<List<SlotDto>> GetNextAsync(string timeZone, int count = 20)
    {
        var tz = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZone);
        if (tz == null)
            throw new UserFriendlyException("Invalid time zone identifier.");

        var now = Instant.FromDateTimeOffset(_clock.Now);

        var query = await _slotRepository.GetQueryableAsync();
        var slots = await query
            .Where(s => s.Status == SlotStatus.Available && s.StartInstant > now)
            .OrderBy(s => s.StartInstant)
            .Take(count)
            .ToListAsync();

        var dtos = new List<SlotDto>();
        foreach (var slot in slots)
        {
            var startZoned = slot.StartInstant.InZone(tz);
            var endZoned = slot.EndInstant.InZone(tz);

            dtos.Add(new SlotDto
            {
                LocalStartTime = startZoned.ToString("yyyy-MM-dd HH:mm:ss z", CultureInfo.InvariantCulture),
                LocalEndTime = endZoned.ToString("yyyy-MM-dd HH:mm:ss z", CultureInfo.InvariantCulture),
                TimeZone = timeZone,
                IsBookable = true
            });
        }

        return dtos;
    }
}