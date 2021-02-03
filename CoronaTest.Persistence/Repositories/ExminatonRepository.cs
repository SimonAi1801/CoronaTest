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

        public async Task<Examination[]> GetByCampaignTestCenter(Campaign campaign, TestCenter testCenter)
            => await _dbContext
                .Examinations
                .Where(_ => _.Campaign == campaign && _.TestCenter == testCenter)
                .OrderBy(_ => _.ExaminationAt)
                .ToArrayAsync();

        public async Task<Examination> GetByIdAsync(int id)
            => await _dbContext
                .Examinations
                .Include(_ => _.TestCenter)
                .Include(_ => _.Campaign)
                .Include(_ => _.Participant)
                .SingleOrDefaultAsync(_ => _.Id == id);

        public async Task<Examination> GetByIdentifierAsync(string identifier)
            => await _dbContext
                .Examinations
                .Include(_ => _.Participant)
                .SingleOrDefaultAsync(_ => _.Identifier == identifier);

        public async Task<Examination[]> GetByParticipantIdAsync(int participantId)
            => await _dbContext
                .Examinations
                .Include(_ => _.Campaign)
                .Include(_ => _.TestCenter)
                .Where(_ => _.Participant.Id == participantId)
                .OrderBy(_ => _.ExaminationAt)
                .ToArrayAsync();

        public async Task<ExaminationDto[]> GetExaminationsWithFilterAsync(DateTime? from = null, DateTime? to = null)
        {
            var query = _dbContext
                .Examinations
                .Include(_ => _.Participant)
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
                .Select(_ => new ExaminationDto(_))
                .ToArrayAsync();
        }

        public void Remove(Examination examination)
            => _dbContext
                .Examinations
                .Remove(examination);
    }
}
