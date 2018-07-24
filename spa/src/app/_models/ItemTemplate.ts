import {ItemProperty} from './ItemProperty';
export enum UnitType {mm = 1, cm, m}

export interface ItemTemplate {
    id: number;
    name: string;
    unitType: UnitType;
    files: string;
    templateProperties: ItemProperty[];
    templateList: ItemTemplate[];
    description: string;
}
