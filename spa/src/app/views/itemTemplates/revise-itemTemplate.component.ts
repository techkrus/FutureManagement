import { Component, OnInit } from '@angular/core';
import { ItemTemplateService } from '../../_services/itemTemplate.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemTemplate } from '../../_models/ItemTemplate';
import { ItemPropertyName } from '../../_models/ItemPropertyName';
import { Observable } from 'rxjs';
import { DetailFile } from '../../_models/DetailFile';
import { FileUploadService } from '../../_services/fileUpload.service';
import { ItemTemplatePart } from '../../_models/ItemTemplatePart';
import { AlertifyService } from '../../_services/alertify.service';
import { UnitType } from '../../_models/UnitType';
import { ItemTemplateCategory } from '../../_models/ItemTemplateCategory';
import { UnitTypeService } from '../../_services/unitType.service';
import { CategoryService } from '../../_services/category.service';
import { TemplatePropertyService } from '../../_services/templateProperty.service';

/**
 *Component that is used to Revise an ItemTemplate
 *
 * @export
 * @class ReviseItemTemplateComponent
 * @implements {OnInit}
 */
@Component({
  templateUrl: './revise-itemTemplate.component.html'
})

export class ReviseItemTemplateComponent implements OnInit {
  isDataAvailable = false;
  templateToRevise: ItemTemplate = {} as ItemTemplate;
  templateToCopy: ItemTemplate = {} as ItemTemplate;
  unitType: UnitType;
  unitTypeList: UnitType[] = [] as UnitType[];
  properties: ItemPropertyName[];
  propertiesToAdd: ItemPropertyName[] = [] as ItemPropertyName[];
  templates: Observable<ItemTemplate[]>;
  selectedTemplates: ItemTemplate[] = [] as ItemTemplate[];
  templateToConvertFromPart: ItemTemplate;
  partAmounts: number[] = [];
  fileService: FileUploadService;
  uploader: FileUploadService;
  propertiesToCheck: ItemPropertyName[] = [];
  templatePartsToAdd: ItemTemplatePart[] = [] as ItemTemplatePart[];
  filesFromRevision: DetailFile[];
  itemsPerPage = 15;
  currentPage = 1;
  propFilter: string;
  categoryList: ItemTemplateCategory[] = [] as ItemTemplateCategory[];
  category: ItemTemplateCategory;
  categoryToAddToDb: ItemTemplateCategory = {} as ItemTemplateCategory;

  constructor(private templateService: ItemTemplateService,
              private route: ActivatedRoute,
              private router: Router,
              private uploaderParameter: FileUploadService,
              private alertify: AlertifyService,
              private unitTypeService: UnitTypeService,
              private categoryService: CategoryService,
              private templatePropertyService: TemplatePropertyService) {
                this.uploader = uploaderParameter;
                this.uploader.clearQueue();
              }

  /**
   * method that is calle when the compononent is initialised.
   * loads all the possible ItemTemplates and properties that can be added to a ItemTemplate
   *
   * @memberof ReviseItemTemplateComponent
   */
  async ngOnInit() {
    await this.loadTemplateOnInIt();
    await this.getTemplateProperties();
    await this.getTemplates();
    await this.populateSelect();
    await this.getUnitTypes();
    await this.getTemplateCategories();
  }

  /**
   *A template is copied through the route. Copied template is stored, and revision props are initialized.
   *
   * @memberof ReviseItemTemplateComponent
   */
  async loadTemplateOnInIt() {
    await this.templateService.getItemTemplateAsync(+this.route.snapshot.params['id'])
      .then(itemTemplate => {
        this.templateToCopy = itemTemplate;
        this.templateToRevise.name = itemTemplate.name;
        this.templateToRevise.description = itemTemplate.description;
        this.filesFromRevision = itemTemplate.files;
        this.propertiesToCheck = itemTemplate.templateProperties;
        this.propertiesToAdd = itemTemplate.templateProperties;
        this.isDataAvailable = true;
        this.templateToRevise.fileNames = [];
        this.templateToRevise.files = [];
        this.templateToRevise.lowerLimit = itemTemplate.lowerLimit;
        this.category = itemTemplate.category;
        this.unitType = itemTemplate.unitType;
    });
  }


  /**
   *Loads all the properties
   *
   * @memberof ReviseItemTemplateComponent
   */
  async getTemplateProperties() {
    await this.templatePropertyService.getAll().subscribe(properties => {
      this.properties = properties;
    });
  }

  async getTemplateCategories() {
    await this.categoryService.getAll().subscribe(categories => {
      this.categoryList = categories;
    });
  }

  /**
   *gets all the templates
   *
   * @memberof ReviseItemTemplateComponent
   */
  async getTemplates() {
    this.templates = await this.templateService.getAll();
  }

  async getUnitTypes() {
    await this.unitTypeService.getAll().subscribe(unitTypes => {
      this.unitTypeList = unitTypes;
    });
  }

  /**
   *
   *
   * @param {*} prop
   * @param {*} event
   * @memberof ReviseItemTemplateComponent
   */
  onCheckboxChange(prop, event) {
    if (event.target.checked) {
      this.propertiesToAdd.push(prop);
    } else {
      for (let i = 0; i < this.properties.length; i++) {
        if (this.propertiesToAdd[i].id === prop.id) {
          this.propertiesToAdd.splice(i, 1);
          break;
        }
      }
    }
  }


  /**
   *Removes a file from the list of files that the ItemTemplate inherited from the ItemTemplate it's revised from
   *
   * @param {DetailFile} file The file to be removed
   * @memberof ReviseItemTemplateComponent
   */
  removeFileToRevice (file: DetailFile) {
    this.filesFromRevision.splice(this.filesFromRevision.indexOf(file), 1);
  }

  /**
   *ngSelect component needs to be filled with the templates from the copied template.
   *selectedTemplates is the model used by ngSelect, so parts are pushed to this.
   *
   * @memberof ReviseItemTemplateComponent
   */
  async populateSelect() {
    if (this.isDataAvailable) {
      for (let i = 0; i < this.templateToCopy.parts.length; i++) {
        this.selectedTemplates.push(this.templateToCopy.parts[i].part);
        this.partAmounts.push(this.templateToCopy.parts[i].amount);
      }
    }
  }

  /**
   *Starts to download a file in the users browser that is on the ItemTemplate
   *
   * @param {DetailFile} fileDetails  file to download
   * @memberof ReviseItemTemplateComponent
   */
  downloadFile(fileDetails: DetailFile) {
    this.uploader.download(fileDetails, 'Template');
  }

  /**
   *The list of properties on the template is compared to all properties so the checkbox can be checked.
   *
   * @param {*} id id of the property that should be checked
   * @returns a boolean that means if it succeded or not
   * @memberof ReviseItemTemplateComponent
   */
  checkBox(id) {
    for (const propToCheck of this.propertiesToCheck) {
      if (propToCheck.id === id) {
        return true;
      }
    }
    return false;
  }

  /**
   * Compares the value of unitType on the object to the possibilities.
   * If equal, it is used as default
   * @param {*} u1
   * @param {*} u2
   * @returns {boolean}
   * @memberof ReviseItemTemplateComponent
   */
  compareDefaultForSelect(u1: any, u2: any): boolean {
    return u1 && u2 ? u1.id === u2.id : u1 === u2;
  }

  /**
   * Adds the revised ItemTemplate
   * This method makes sure that all the properties and ItemTemplates that are a a part of this ItemTemplate are added to it
   * It also adds all the files to the ItemTemplate
   * Finally it sends the revised ItemTemplate to the controller so that it can be added to the DB
   *
   * @memberof ReviseItemTemplateComponent
   */
  async addRevision() {
    // selectedTemplates and amounts are pushed to a new array that is formatted correctly for the database.
    for (let i = 0; i < this.selectedTemplates.length; i++) {
      this.templatePartsToAdd.push({
        part: this.selectedTemplates[i],
        templateId: this.selectedTemplates[i].id,
        amount: this.partAmounts[i],
      });
    }
    if (this.uploader.queuedFiles.length > 0) {
      const fileArray = await this.uploader.upload('ItemTemplateFiles');
      this.templateToRevise.files = fileArray;
      for (const file of this.uploader.queuedFiles) {
        this.templateToRevise.fileNames.push(file.name);
      }
    } else {
      this.templateToRevise.files = [] as number[];
    }

    for (const file of this.filesFromRevision) {
      this.templateToRevise.files.push(file.fileDataId);
      this.templateToRevise.fileNames.push(file.fileName);
  }

    this.templateToRevise.templateProperties = this.propertiesToAdd;
    this.templateToRevise.revisionedFrom = this.templateToCopy;
    this.templateToRevise.unitType = this.unitType;
    this.templateToRevise.templateProperties = this.propertiesToAdd;
    this.templateToRevise.parts = this.templatePartsToAdd;
    this.templateToRevise.category = this.category;

    this.templateService.addTemplate(this.templateToRevise).subscribe(data => {
      this.alertify.success('Tilføjede revidering af skabelon');
    }, error => {
      this.alertify.error('Kunne ikke tilføje revidering');
    }, () => {
      this.router.navigate(['itemTemplates/view']);
    });
  }
}
