﻿using CoronaTest.Core.Contracts;
using CoronaTest.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronaTest.Persistence.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ParticipantRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Participant participant)
            => await _dbContext
                .Participants
                .AddAsync(participant);

        public async Task AddRangeAsync(Participant[] participants)
            => await _dbContext
                .Participants
                .AddRangeAsync(participants);

        public async Task<Participant[]> GetAllAsync()
        => await _dbContext.Participants.ToArrayAsync();

        public async Task<Participant> GetByIdAsync(int id)
            => await _dbContext
                .Participants
                .SingleOrDefaultAsync(p => p.Id == id);

        public async Task<Participant> GetBySocialSecurityNumberAsync(string socialSecurityNumber)
            => await _dbContext
                .Participants
                .SingleOrDefaultAsync(p => p.SocialSecurityNumber == socialSecurityNumber);
    }
}
