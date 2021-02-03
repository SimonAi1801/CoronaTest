﻿using CoronaTest.Core.Contracts;
using CoronaTest.Core.DTOs;
using CoronaTest.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaTest.Persistence.Repositories
{
    public class TestCenterRepository : ITestCenterRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TestCenterRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TestCenter testCenter)
            => await _dbContext
                .TestCenters
                .AddAsync(testCenter);

        public async Task AddRangeAsync(TestCenter[] testCenters)
            => await _dbContext
                .TestCenters
                .AddRangeAsync(testCenters);

        public async Task<IEnumerable<SlotDto>> GetAllSlotsByCampaignIdAsync(int campaignId, int testCenterId)
        {
            int slotDuration = 15;
            DateTime startTime = DateTime.Today.AddHours(8);
            DateTime endTime = DateTime.Today.AddHours(16);

            List<SlotDto> allSlots = new List<SlotDto>();

            var campaign = await _dbContext
                                    .Campaigns
                                    .SingleAsync(_ => _.Id == campaignId);

            var testCenter = await GetByIdAsync(testCenterId);
            var examinations = await _dbContext
                                        .Examinations
                                        .Where(_ => _.Campaign.Id == campaignId && _.TestCenter.Id == testCenterId)
                                        .ToArrayAsync();

            if (campaign == null || testCenter == null)
            {
                return allSlots;
            }

            DateTime runDate = campaign.From;
            if (runDate < DateTime.Now)
            {
                runDate = DateTime.Now;
            }
            DateTime endDate = campaign.To;

            while (runDate.Date < endDate.Date)
            {
                if (startTime.TimeOfDay <= runDate.TimeOfDay && runDate.TimeOfDay < endTime.TimeOfDay)
                {
                    allSlots.Add(new SlotDto
                    {
                        Time = runDate,
                        SlotsAvailable = testCenter.SlotCapacity - examinations
                                                                        .Where(_ => _.ExaminationAt == runDate)
                                                                        .ToList()
                                                                        .Count
                    });
                }
                runDate = runDate.AddMinutes(slotDuration);
            }

            return allSlots;
        }

        public async Task<TestCenter[]> GetByCampaignIdAsync(int campaignId)
        {
            var campaign = await _dbContext
                .Campaigns
                .Include(_ => _.AvailableTestCenters)
                .SingleAsync(c => c.Id == campaignId);

            return campaign.AvailableTestCenters
                .OrderBy(_ => _.Name)
                .ToArray();
        }

        public async Task<TestCenter> GetByIdAsync(int id)
            => await _dbContext
                .TestCenters
                .SingleOrDefaultAsync(_ => _.Id == id);

        public async Task<int> GetCountAsync()
            => await _dbContext
                .TestCenters
                .CountAsync();
    }
}
