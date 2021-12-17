using System.Collections.Generic;
using System.Threading.Tasks;
using Ireckonu.Assignment.Business.Services.Abstractions;
using Ireckonu.Assignment.Data.Entities;
using Ireckonu.Assignment.Data.Repositories.Abstractions;

namespace Ireckonu.Assignment.Business.Services
{
    public class DataService : IDataService
    {
        private readonly IDbDataRepository _dbDataRepository;
        private readonly IJsonDataRepository _jsonDataRepository;

        public DataService(
            IDbDataRepository dbDataRepository,
            IJsonDataRepository jsonDataRepository)
        {
            _dbDataRepository = dbDataRepository;
            _jsonDataRepository = jsonDataRepository;
        }

        public async Task InsertMultipleRawDataAsync(List<RawData> entityList)
        {
            await _dbDataRepository.InsertMultipleRawDataAsync(entityList);
            await _jsonDataRepository.InsertMultipleRawDataAsync(entityList);
        }
    }
}