import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Http } from '../../../node_modules/@angular/http';
import { ItemTemplate } from '../_models/ItemTemplate';
import { Observable } from '../../../node_modules/rxjs';
import { ItemProperty } from '../_models/ItemProperty';

@Injectable()
export class ItemTemplateService {
    baseUrl = environment.apiUrl;

    constructor(private http: Http) {}

    getItemTemplates(): Observable<ItemTemplate[]> {
        return this.http.get(this.baseUrl + 'ItemTemplate/getAll')
            .map(response => <ItemTemplate[]>response.json());
    }

    getItemTemplate(id: number): Observable<ItemTemplate> {
        return this.http.get(this.baseUrl + 'ItemTemplate/ItemTemplate/get/' + id)
            .map(response => <ItemTemplate>response.json());
    }

    addTemplateProperty(property: ItemProperty): Observable<ItemProperty> {
        return this.http.post(this.baseUrl + 'ItemTemplate/addProperty', property)
        .map(response => <ItemTemplate>response.json());
    }

    deleteItemTemplate(id) {
        return this.http.post(this.baseUrl + 'ItemTemplate/delete/' + id, {})
        .map(response => {});
    }


}
