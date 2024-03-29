using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using API.Enums;
using Newtonsoft.Json;

namespace API.Models{
    public class ItemTemplate{
        public ItemTemplate(){}
        public ItemTemplate(int id, string name, UnitType unitType, string description, ICollection<TemplatePropertyRelation> properties, 
                            ICollection<ItemTemplatePart> parts, ICollection<ItemTemplatePart> partOf, DateTime created, 
                            ItemTemplate revisionedFrom, ICollection<TemplateFileName> files, 
                            int lowerLimit, ItemTemplateCategory category){
            this.Id = id;
            this.Name = name;
            this.UnitType = unitType;
            this.Description = description;
            this.TemplateProperties = properties;
            this.Parts = parts;
            this.PartOf = partOf;
            this.Files = files;
            this.Created = created;
            this.RevisionedFrom = revisionedFrom;
            this.LowerLimit = lowerLimit;
            this.Category = category;
        }

        public ItemTemplate(string name, UnitType unitType, string description, ICollection<TemplatePropertyRelation> properties, 
                            ICollection<ItemTemplatePart> parts, ICollection<ItemTemplatePart> partOf, 
                            DateTime created, ItemTemplate revisionedFrom, ICollection<TemplateFileName> files, 
                            int lowerLimit, ItemTemplateCategory category){
            this.Name = name;
            this.UnitType = unitType;
            this.Description = description;
            this.TemplateProperties = properties;
            this.Parts = parts;
            this.PartOf = partOf;
            this.Files = files;
            this.Created = created;
            this.RevisionedFrom = revisionedFrom;
            this.LowerLimit = lowerLimit;
            this.Category = category;
        }
        
        [Key]
        public int Id { get;  set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public UnitType UnitType { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public ICollection<TemplatePropertyRelation> TemplateProperties { get; set; }
        public ItemTemplate RevisionedFrom { get; set; }
        public ICollection<ItemTemplatePart> Parts { get; set; }
        public ICollection<ItemTemplatePart> PartOf { get; set; }
        public ICollection<TemplateFileName> Files { get; set; }
        public int LowerLimit { get; set; }
        public ItemTemplateCategory Category { get; set; }
    }
}