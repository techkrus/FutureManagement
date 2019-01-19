import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../_services/order.service';
import { Order } from '../../_models/Order';
import { ActivatedRoute, Router } from '@angular/router';
import { DetailFile } from '../../_models/DetailFile';
import { FileUploadService } from '../../_services/fileUpload.service';
import { OrderStatusEnum } from '../../_enums/OrderStatusEnum.enum';

@Component({
  templateUrl: './details-order.component.html'
})
export class DetailsOrderComponent implements OnInit {
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

  async ngOnInit() {
    await this.loadOrderOnInIt();
  }

  async loadOrderOnInIt() {
    await this.orderService.getOrder(+this.route.snapshot.params['id'])
      .then(order => {
        this.order = order;
        this.orderStatus = OrderStatusEnum[order.status];
        this.orderDateString = order.orderDate.toString().split("T", 1)[0];
        this.deliveryDateString = order.deliveryDate.toString().split("T", 1)[0];
        this.isDataAvailable = true;
      });
  }

  goToItemTemplateDetail(templateId: number) {
    this.router.navigateByUrl('itemTemplates/details/' + templateId);
  }

  goToEditPage(orderId: number) {
    console.log(orderId);
    this.router.navigateByUrl('orders/edit/' + orderId);
  }

  downloadFile(fileDetails: DetailFile) {
    this.fileService.download(fileDetails, 'Order');
  }
}
