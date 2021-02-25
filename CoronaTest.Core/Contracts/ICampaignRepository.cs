using CoronaTest.Core.DTOs;
using CoronaTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronaTest.Core.Contracts
{
    public interface ICampaignRepository
    {
        Task AddAsync(Campaign campaign);
        Task AddRangeAsync(Campaign[] campaigns);
        Task<int> GetCountAsync();
        Task<CampaignDto[]> GetAllAsync();
        Task<CampaignDto> GetDtoByIdAsync(int id);
        void Update(Campaign modifiedCampaign);
        void Remove(Campaign campaign);
        Task<Campaign> GetByIdAsync(int id);
    }
}
