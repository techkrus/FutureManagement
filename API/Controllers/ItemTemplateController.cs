using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ItemTemplateController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IItemTemplateRepository _repo;

        public ItemTemplateController(IItemTemplateRepository repo, DataContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("getAll", Name = "GetAllitemTemplates")]
        public async Task<IActionResult> GetItemTemplates(){
            var itemTemplates = await _repo.GetItemTemplates();

            var itemTemplatesToReturn = _mapper.Map<List<ItemTemplateForTableDto>>(itemTemplates);

            return Ok(itemTemplatesToReturn);
        }

        [HttpGet("get/{id}", Name = "GetItemTemplate")]
        public async Task<IActionResult> GetItemTemplate(int id){
            ItemTemplate itemTemplate = await _repo.GetItemTemplate(id);

            ItemTemplateForGetDto itemTemplateToReturn = _mapper.Map<ItemTemplateForGetDto>(itemTemplate);
            itemTemplateToReturn.Parts = _mapper.Map<List<ItemTemplatePartDto>>(itemTemplate.Parts);            
            itemTemplateToReturn.TemplateProperties = _mapper.Map<List<TemplatePropertyForGetDto>>(itemTemplate.TemplateProperties);
            itemTemplateToReturn.PartOf = _mapper.Map<List<ItemTemplatePartOfDto>>(itemTemplate.PartOf);

            return Ok(itemTemplateToReturn);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItemTemplate([FromBody]ItemTemplateForAddDto templateDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var itemTemplateToCreate = new ItemTemplate(
                templateDto.Name,
                templateDto.UnitType,
                templateDto.Description,
                templateDto.TemplateProperties,
                templateDto.Parts,
                templateDto.Files
            );

            await _repo.AddItemTemplate(itemTemplateToCreate);

            return StatusCode(201);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditItemTemplate([FromBody]ItemTemplate template){
            if(template.Id == 0){
                ModelState.AddModelError("Item Template Error","Template id can not be 0.");
            }
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            bool result = await _repo.EditItemTemplate(template);

            return result ? StatusCode(200) : StatusCode(400);
        }
        [HttpPost("delete/{id}", Name = "DeleteItemTemplate")]
        public async Task<IActionResult> DeleteItemTemplate(int id){
            if(id == 0){
                ModelState.AddModelError("Item Template Error","Can not delete template with id 0.");
            }
            
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var template = await _repo.GetItemTemplate(id);

            bool result = await _repo.DeleteItemTemplate(template);

            return StatusCode(200);
        }

        [HttpPost("addProperty", Name = "AddPropertyName")]
        public async Task<IActionResult> AddPropertyName([FromBody]ItemPropertyNameForAddDto propertyDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var itemPropertyName = new ItemPropertyName(
                propertyDto.Name
            );

            await _repo.AddPropertyName(itemPropertyName);

            return StatusCode(201);
        }

        [HttpGet("getPropertyName")]
        public async Task<IActionResult> GetPropertyNames(){
            var propertyNames = await _repo.GetPropertyNames();
            var PropertyNamesToReturn = _mapper.Map<List<TemplatePropertyForGetDto>>(propertyNames);

            return Ok(PropertyNamesToReturn);
        }

        [HttpGet("getPropertyName/{id}", Name = "GetPropertyName")]
        public async Task<IActionResult> GetPropertyName(int id){
            var propertyName = await _repo.GetPropertyName(id);
            var propertyNameToReturn = _mapper.Map<ItemPropertyNameForGetDto>(propertyName);

            return Ok(propertyNameToReturn);
        }

        [HttpPost("activate/{id}", Name = "ActivateItemTemplate")]
        public async Task<IActionResult> ActivateItemTemplate(int id){
            if(id == 0){
                ModelState.AddModelError("Item Template Error","Can not activate template with id 0.");
            }
            
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var template = await _repo.GetItemTemplate(id);

            bool result = await _repo.ActivateItemTemplate(template);

            return StatusCode(200);
        }

        [HttpPost("deactivate/{id}", Name = "DeactivateItemTemplate")]
        public async Task<IActionResult> DeactivateItemTemplate(int id){
            if(id == 0){
                ModelState.AddModelError("Item Template Error","Can not deactivate template with id 0.");
            }
            
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var template = await _repo.GetItemTemplate(id);

            bool result = await _repo.DeactivateItemTemplate(template);

            return StatusCode(200);
        }
    }
}