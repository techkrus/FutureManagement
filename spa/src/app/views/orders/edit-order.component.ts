import { OnInit, Component, Pipe, PipeTransform  } from '@angular/core';
import { DatePipe, formatDate } from '@angular/common';
import { OrderService } from '../../_services/order.service';
import { AlertifyService } from '../../_services/alertify.service';
import { Order } from '../../_models/Order';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderStatusEnum } from '../../_enums/OrderStatusEnum.enum';
import { UnitType } from '../../_models/UnitType';
import { UnitTypeService } from '../../_services/unitType.service';
import { stringify } from '@angular/core/src/util';

@Component({
    templateUrl: './edit-order.component.html'
})

export class EditOrderComponent implements OnInit {
    order: Order;
    isDataAvailable: boolean;
    orderStatus: string;
    orderStatusEnum = OrderStatusEnum;
    keys: string[];
    editDate = false;
    deliveryDateAsString: string;
    orderDate: Date;
    dataAvailable = false;
    unitTypes: UnitType[] = [] as UnitType[];
    originalOrder: string;

    constructor(private orderService: OrderService, private alertify: AlertifyService, private route: ActivatedRoute,
        private router: Router, private unitTypeService: UnitTypeService) { }

    async ngOnInit() {
        this.keys = Object.keys(this.orderStatusEnum);
        this.keys = this.keys.slice(this.keys.length / 2 );
        await this.loadOrderOnInIt();
        this.deliveryDateAsString = formatDate(this.order.deliveryDate, 'dd/MM/yyyy', 'en-US');
    }

    async loadOrderOnInIt() {
        await this.orderService.getOrder(+this.route.snapshot.params['id'])
            .then(order => {
                this.order = order;
                this.originalOrder = JSON.stringify(order).toLocaleLowerCase();
                this.orderStatus = OrderStatusEnum[order.status].toString();
            });
        await this.unitTypeService.getAll()
            .subscribe(unitTypes => {
                this.unitTypes = unitTypes;
            });
        this.dataAvailable = true;
    }

    toggleEditDate() {
        this.editDate = !this.editDate;
        this.orderDate = this.order.deliveryDate;
    }

    cancelEditDate() {
        this.order.deliveryDate = this.orderDate;
        this.editDate = !this.editDate;
    }

    saveEditDate() {
        this.deliveryDateAsString = formatDate(this.order.deliveryDate, 'dd/MM/yyyy', 'en-US');
        this.editDate = !this.editDate;
    }


    /**
     * Compares the value of unitType on the object to the possibilities.
     * If equal, it is used as default
     * @param {UnitType} u1
     * @param {UnitType} u2
     * @returns {boolean}
     * @memberof EditOrderComponent
     */
    compareDefaultUnitType(u1: UnitType, u2: UnitType): boolean {
        return u1 && u2 ? u1.id === u2.id : u1 === u2;
    }

    saveOrder() {
        this.order.status = this.orderStatusEnum[this.orderStatus];
        if (this.originalOrder === JSON.stringify(this.order).toLocaleLowerCase()) {
            this.alertify.success('Der var ingen ændringer at gemme');
            this.router.navigate(['orders/details/' + this.order.id]);
        } else {
            this.orderService.editOrder(this.order).subscribe(
                data => {
                    this.alertify.success('Ændringer gemt');
                },
                error => {
                    this.alertify.error('Kunne ikke gemme ændringer');
                },
                () => {
                    this.router.navigate(['orders/details/' + this.order.id]);
                }
            );
        }
    }
}
