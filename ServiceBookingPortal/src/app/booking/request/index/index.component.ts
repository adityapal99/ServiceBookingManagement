import { Component, OnInit } from '@angular/core';
import { BookingService } from '../../booking.service';
import { ResponseObj, UserRequest } from '../../user-request';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  requests:UserRequest[] = [];

  constructor(private bookingService:BookingService) { }

  ngOnInit(): void {
    this.bookingService.getRequests().subscribe(
      (data:ResponseObj) => {
        this.requests = data.payload;
        console.log(this.requests);
      }
    );
  }


  deleteRequest(id:number){
    this.bookingService.deleteRequest(id).subscribe(res => {
         this.requests = this.requests.filter(item => item.id !== id);
         console.log('Product deleted successfully!');
    })
  }

}
