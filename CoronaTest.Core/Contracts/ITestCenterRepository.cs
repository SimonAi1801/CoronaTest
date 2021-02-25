using CoronaTest.Core.DTOs;
using CoronaTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronaTest.Core.Contracts
{
    public interface ITestCenterRepository
    {
        Task AddAsync(TestCenter testCenter);
        Task AddRangeAsync(TestCenter[] testCenters);
        Task<int> GetCountAsync();
        Task<TestCenter[]> GetByCampaignIdAsync(int campaignId);
        Task<TestCenter> GetByIdAsync(int id);
        Task<IEnumerable<SlotDto>> GetAllSlotsByCampaignIdAsync(int campaignId, int testCenterId);
        Task<TestCenterDto[]> GetAllAsync();
        Task<TestCenterDto> GetDtoByIdAsync(int id);
        Task<TestCenterDto[]> GetByPostalCodeAsync(string postalCode);
        void Update(TestCenter tesCenter);
        void Remove(TestCenter testCenter);
    }
}
