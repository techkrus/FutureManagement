import { Component, OnInit, AfterViewInit } from '@angular/core';
import { OrderService } from '../../_services/order.service';
import { Order } from '../../_models/Order';
import { ActivatedRoute, Router } from '@angular/router';
import { DetailFile } from '../../_models/DetailFile';
import { FileUploadService } from '../../_services/fileUpload.service';
import { OrderStatusEnum } from '../../_enums/OrderStatusEnum.enum';
import { formatDate } from '@angular/common';

@Component({
    templateUrl: './print-order.component.html'
})
export class PrintOrderComponent implements AfterViewInit {
    isDataAvailable = false;
    order: Order;
    files: DetailFile[];
    fileService: FileUploadService;
    orderStatus: String;
    deliveryDateString: String;
    orderDateString: String;

    constructor(
        private orderService: OrderService,
        private route: ActivatedRoute,
        private fileUploadService: FileUploadService,
        private router: Router
    ) {
        this.fileService = fileUploadService;
    }
    
    async ngAfterViewInit() {
        this.loadOrderOnInIt();
    }

    loadOrderOnInIt() {
        this.orderService.getOrder(+this.route.snapshot.params['id'])
            .then(order => {
                this.order = order;
                this.orderStatus = OrderStatusEnum[order.status];
                this.orderDateString = formatDate(order.orderDate, 'dd/MM/yyyy', 'en-US');
                this.deliveryDateString = formatDate(this.order.deliveryDate, 'dd/MM/yyyy', 'en-US');
                this.isDataAvailable = true;
            });
    }
}
