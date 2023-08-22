using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Church_Visitors.Interfaces;
using Church_Visitors.Models;
using Church_Visitors.Data;
using MongoDB.Driver;

namespace Church_Visitors.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IMongoCollection<Visitor> _visitors;

        public VisitorService(DataContext dataContext, string collectionName)
        {
            _visitors = dataContext.GetCollection<Visitor>(collectionName);
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitorsAsync() =>
            await _visitors.Find(visitor => true).ToListAsync();

        public async Task<IEnumerable<Visitor>> GetVisitorsByDateEnteredAsync(DateTime dateEntered) =>
            await _visitors.Find(visitor => visitor.DateEntered.Date == dateEntered.Date).ToListAsync();

        public async Task<IEnumerable<Visitor>> GetVisitorsByTodaysDateAsync()
        {
            var today = DateTime.Today;
            return await _visitors.Find(visitor => visitor.DateEntered.Date == today).ToListAsync();
        }

        public async Task<Visitor> GetVisitorByIdAsync(string id) =>
            await _visitors.Find(visitor => visitor.Id == id).FirstOrDefaultAsync();

        public async Task CreateVisitorAsync(Visitor visitor) =>
            await _visitors.InsertOneAsync(visitor);

        public async Task UpdateVisitorAsync(Visitor visitor) =>
            await _visitors.ReplaceOneAsync(v => v.Id == visitor.Id, visitor);

        public async Task DeleteVisitorAsync(string id) =>
            await _visitors.DeleteOneAsync(v => v.Id == id);

        public async Task<IEnumerable<Visitor>> SearchVisitorsAsync(string searchText)
        {
            var filter = Builders<Visitor>.Filter.Regex(v => v.FullName, searchText) |
                         Builders<Visitor>.Filter.Regex(v => v.GuestOf, searchText) |
                         Builders<Visitor>.Filter.Regex(v => v.OtherRemarks, searchText);

            return await _visitors.Find(filter).ToListAsync();
        }
    }
}
