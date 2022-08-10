﻿using NzWalks.API.Models.Domain;
namespace NzWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();

        Task<WalkDifficulty> GetAsync(Guid id);

        Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> UpdateAsync(Guid Id, WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> DeleteAsync(Guid id);
    }
}
