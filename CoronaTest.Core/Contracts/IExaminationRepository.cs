﻿using CoronaTest.Core.DTOs;
using CoronaTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronaTest.Core.Contracts
{
    public interface IExaminationRepository
    {
        Task AddAsync(Examination examination);
        Task AddRangeAsync(Examination[] examinations);
        Task<Examination[]> GetByCampaignTestCenter(Campaign campaign, TestCenter testCenter);
        Task<Examination[]> GetByParticipantIdAsync(int participantId);
        Task<Examination> GetByIdAsync(int id);
        void Remove(Examination examination);
        Task<ExaminationDto[]> GetExaminationsWithFilterAsync(DateTime? from = null, DateTime? to = null);
        Task<Examination> GetByIdentifierAsync(string identifier);
    }
}
