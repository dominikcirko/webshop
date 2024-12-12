using System.Collections.Generic;
using System.Threading.Tasks;
using webshopAPI.Models;
using webshopAPI.Services.Interfaces;
using webshopAPI.DataAccess.Repositories.Interfaces;

namespace webshopAPI.Services.Implementation
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _tagRepository.GetAllAsync();
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _tagRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Tag tag)
        {
            await _tagRepository.AddAsync(tag);
        }

        public async Task UpdateAsync(Tag tag)
        {
            await _tagRepository.UpdateAsync(tag);
        }

        public async Task DeleteAsync(int id)
        {
            await _tagRepository.DeleteAsync(id);
        }

        public async Task<Tag> GetByNameAsync(string name)
        {
            return await _tagRepository.GetByNameAsync(name);
        }

        public async Task<IEnumerable<Tag>> GetTagsByItemIdAsync(int itemId)
        {
            return await _tagRepository.GetTagsByItemIdAsync(itemId);
        }
    }
}
