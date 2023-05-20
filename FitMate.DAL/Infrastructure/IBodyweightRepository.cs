using FitMate.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitMate.Data
{
    public interface IBodyweightRepository
    {
        public Task<BodyweightRecord[]> GetBodyweightRecords(FitnessUser User, bool AscendingOrder = false);
        public Task<BodyweightTarget> GetBodyweightTarget(FitnessUser User);
        public Task StoreBodyweightRecord(BodyweightRecord Record);
        public Task StoreBodyweightRecords(List<BodyweightRecord> Records);
        public Task DeleteExistingRecords(FitnessUser User);
        public Task StoreBodyweightTarget(BodyweightTarget Target);

    }
}
