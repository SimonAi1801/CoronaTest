﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronaTest.Core.Contracts
{
    public interface IUnitOfWork
    {
        ICampaignRepository Campaigns { get; }
        ITestCenterRepository TestCenters { get; }
        IParticipantRepository Participants { get; }
        IExaminationRepository Examinations { get; }
        Task<int> SaveChangesAsync();
        Task DeleteDatabaseAsync();
        Task MigrateDatabaseAsync();
    }
}
