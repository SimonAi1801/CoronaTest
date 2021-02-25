using CoronaTest.Core.Contracts;
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
    public class ExaminationRepository : IExaminationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ExaminationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Examination examination)
            => await _dbContext
                .Examinations
                .AddAsync(examination);

        public async Task AddRangeAsync(Examination[] examinations)
            => await _dbContext
                .Examinations
                .AddRangeAsync(examinations);

        public async Task<ExaminationDto[]> GetAllAsync()
        => await _dbContext.Examinations
                .Include(e => e.Campaign)
                .Include(e => e.Participant)
                .Include(e => e.TestCenter)
                .Select(e => new ExaminationDto
                {
                    Id = e.Id,
                    Campaign = e.Campaign,
                    CampaignId = e.Campaign.Id,
                    ExaminationAt = e.ExaminationAt,
                    Identifier = e.Identifier,
                    Participant = e.Participant,
                    ParticipantId = e.Participant.Id,
                    TestCenter = e.TestCenter,
                    TestCenterId = e.TestCenter.Id,
                    TestResult = e.TestResult,
                    ExaminationState = e.ExaminationState
                })
                .ToArrayAsync();

        public async Task<ExaminationDto[]> GetByCamapignIdAsync(int campaignId)
        => await _dbContext.Examinations
                .Include(e => e.Campaign)
                .Include(e => e.Participant)
                .Include(e => e.TestCenter)
                .Where(e => e.Campaign.Id == campaignId)
                .Select(e => new ExaminationDto
                {
                    Id = e.Id,
                    Campaign = e.Campaign,
                    CampaignId = e.Campaign.Id,
                    ExaminationAt = e.ExaminationAt,
                    Identifier = e.Identifier,
                    Participant = e.Participant,
                    ParticipantId = e.Participant.Id,
                    TestCenter = e.TestCenter,
                    TestCenterId = e.TestCenter.Id,
                    TestResult = e.TestResult,
                    ExaminationState = e.ExaminationState
                })
                .ToArrayAsync();

        public async Task<ExaminationDto[]> GetByCampaignAndTestCenter(Campaign campaign, TestCenter testCenter)
            => await _dbContext
                .Examinations
                .Include(e => e.Campaign)
                .Include(e => e.Participant)
                .Include(e => e.TestCenter)
                .Where(_ => _.Campaign == campaign && _.TestCenter == testCenter)
                .Select(e => new ExaminationDto
                {
                    Id = e.Id,
                    Campaign = e.Campaign,
                    CampaignId = e.Campaign.Id,
                    ExaminationAt = e.ExaminationAt,
                    Identifier = e.Identifier,
                    Participant = e.Participant,
                    ParticipantId = e.Participant.Id,
                    TestCenter = e.TestCenter,
                    TestCenterId = e.TestCenter.Id,
                    TestResult = e.TestResult,
                    ExaminationState = e.ExaminationState
                })
                .OrderBy(_ => _.ExaminationAt)
                .ToArrayAsync();

        public async Task<ExaminationDto> GetDtoByIdAsync(int id)
            => await _dbContext
                .Examinations
                .Include(_ => _.TestCenter)
                .Include(_ => _.Campaign)
                .Include(_ => _.Participant)
                .Select(e => new ExaminationDto
                {
                    Id = e.Id,
                    Campaign = e.Campaign,
                    CampaignId = e.Campaign.Id,
                    ExaminationAt = e.ExaminationAt,
                    Identifier = e.Identifier,
                    Participant = e.Participant,
                    ParticipantId = e.Participant.Id,
                    TestCenter = e.TestCenter,
                    TestCenterId = e.TestCenter.Id,
                    TestResult = e.TestResult,
                    ExaminationState = e.ExaminationState
                })
                .SingleOrDefaultAsync(_ => _.Id == id);

        public async Task<ExaminationDto> GetByIdentifierAsync(string identifier)
            => await _dbContext
                .Examinations
                .Include(_ => _.TestCenter)
                .Include(_ => _.Campaign)
                .Include(_ => _.Participant)
                .Select(e => new ExaminationDto
                {
                    Id = e.Id,
                    Campaign = e.Campaign,
                    CampaignId = e.Campaign.Id,
                    ExaminationAt = e.ExaminationAt,
                    Identifier = e.Identifier,
                    Participant = e.Participant,
                    ParticipantId = e.Participant.Id,
                    TestCenter = e.TestCenter,
                    TestCenterId = e.TestCenter.Id,
                    TestResult = e.TestResult,
                    ExaminationState = e.ExaminationState
                })
                .SingleOrDefaultAsync(_ => _.Identifier == identifier);

        public async Task<ExaminationDto[]> GetByParticipantIdAsync(int participantId)
            => await _dbContext
                .Examinations
                .Include(_ => _.TestCenter)
                .Include(_ => _.Campaign)
                .Include(_ => _.Participant)
                .Where(_ => _.Participant.Id == participantId)
                .Select(e => new ExaminationDto
                {
                    Id = e.Id,
                    Campaign = e.Campaign,
                    CampaignId = e.Campaign.Id,
                    ExaminationAt = e.ExaminationAt,
                    Identifier = e.Identifier,
                    Participant = e.Participant,
                    ParticipantId = e.Participant.Id,
                    TestCenter = e.TestCenter,
                    TestCenterId = e.TestCenter.Id,
                    TestResult = e.TestResult,
                    ExaminationState = e.ExaminationState
                })
                .OrderBy(_ => _.ExaminationAt)
                .ToArrayAsync();

        public async Task<ExaminationDto[]> GetExaminationsWithFilterAsync(DateTime? from = null, DateTime? to = null)
        {
            var query = _dbContext
                .Examinations
                .Include(_ => _.Participant)
                .Include(_ => _.TestCenter)
                .Include(_ => _.Campaign)
                .AsQueryable();

            if (from != null)
            {
                query = query.Where(_ => _.ExaminationAt.Date >= from.Value.Date);
            }
            if (to != null)
            {
                query = query.Where(_ => _.ExaminationAt.Date <= to.Value.Date);
            }

            return await query
                .OrderBy(_ => _.ExaminationAt)
                .Select(e => new ExaminationDto
                {
                    Campaign = e.Campaign,
                    ExaminationAt = e.ExaminationAt,
                    CampaignId = e.Campaign.Id,
                    Identifier = e.Identifier,
                    Participant = e.Participant,
                    ParticipantId = e.Participant.Id,
                    TestCenter = e.TestCenter,
                    TestCenterId = e.TestCenter.Id,
                    TestResult = e.TestResult,
                    ExaminationState = e.ExaminationState
                }).ToArrayAsync();
        }

        public void Remove(Examination examination)
            => _dbContext
                .Examinations
                .Remove(examination);

        public void Update(Examination examination)
        => _dbContext.Examinations.Update(examination);

        public async Task<Examination> GetByIdAsync(int id)
        => await _dbContext.Examinations.SingleOrDefaultAsync(e => e.Id == id);
    }
}
