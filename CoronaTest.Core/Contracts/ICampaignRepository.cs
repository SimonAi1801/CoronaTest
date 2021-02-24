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
        Task<Campaign[]> GetAllAsync();
        Task<Campaign> GetByIdAsync(int id);
        void Update(Campaign modifiedCampaign);
        void Remove(Campaign campaign);
    }
}
