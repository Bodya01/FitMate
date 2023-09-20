using FitMate.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitMate.Data
{
    public class BodyweightRepository : IBodyweightRepository
    {
        private FitMateContext dbContext;

        public BodyweightRepository(FitMateContext DBContext)
        {
            this.dbContext = DBContext;
        }

        public async Task StoreBodyweightRecord(BodyweightRecord record)
        {
            dbContext.BodyweightRecords.Add(record);
            await dbContext.SaveChangesAsync();
        }

        public async Task StoreBodyweightRecords(List<BodyweightRecord> records)
        {
            await dbContext.BodyweightRecords.AddRangeAsync(records);
            await dbContext.SaveChangesAsync();
        }

        public async Task<BodyweightRecord[]> GetBodyweightRecords(FitnessUser User, bool ascendingOrder = false)
        {
            if (!ascendingOrder)
            {
                return await dbContext.BodyweightRecords
                    .Where(record => record.User == User)
                    .OrderByDescending(record => record.Date)
                    .ToArrayAsync();
            }
            else
            {
                return await dbContext.BodyweightRecords
                    .Where(record => record.User == User)
                    .OrderBy(record => record.Date)
                    .ToArrayAsync();
            }
        }

        public async Task<BodyweightTarget> GetBodyweightTarget(FitnessUser User)
        {
            var result = await dbContext.BodyweightTargets.FirstOrDefaultAsync(target => target.User == User);
            return result;
        }

        public async Task DeleteExistingRecords(FitnessUser User)
        {
            var existingRecords = await dbContext.BodyweightRecords.Where(record => record.User == User).ToArrayAsync();
            dbContext.BodyweightRecords.RemoveRange(existingRecords);
            await dbContext.SaveChangesAsync();
        }

        public async Task StoreBodyweightTarget(BodyweightTarget Target)
        {
            if (Target.Id == 0)
            {
                await dbContext.BodyweightTargets.AddAsync(Target);
            }
            else
            {
                dbContext.BodyweightTargets.Update(Target);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}