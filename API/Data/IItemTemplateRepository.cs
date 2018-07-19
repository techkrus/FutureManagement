using System.Threading.Tasks;
using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Data
{
    public interface IItemTemplateRepository
    {
        Task<ItemTemplate> GetItemTemplate(int id);
        Task<List<ItemTemplate>> GetItemTemplates();
        Task<bool> AddItemTemplate(ItemTemplate template);
        Task<bool> EditItemTemplate(ItemTemplate template);
        Task<bool> DeleteItemTemplate(int template);
    }
}