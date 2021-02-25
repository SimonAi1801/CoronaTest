using CoronaTest.Core.DTOs;
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
        Task<ExaminationDto[]> GetByCampaignAndTestCenter(Campaign campaign, TestCenter testCenter);
        Task<ExaminationDto[]> GetByParticipantIdAsync(int participantId);
        Task<Examination> GetByIdAsync(int id);
        Task<ExaminationDto> GetDtoByIdAsync(int id);
        void Remove(Examination examination);
        Task<ExaminationDto[]> GetExaminationsWithFilterAsync(DateTime? from = null, DateTime? to = null);
        Task<ExaminationDto> GetByIdentifierAsync(string identifier);
        Task<ExaminationDto[]> GetByCamapignIdAsync(int campaignId);
        Task<ExaminationDto[]> GetAllAsync();
        void Update(Examination examination);
    }
}
