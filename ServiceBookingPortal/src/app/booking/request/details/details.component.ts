import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookingService } from '../../booking.service';
import { ResponseObj, UserRequest } from '../../user-request';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  id:number;
  request:UserRequest;

  constructor(public bookingService: BookingService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params["requestId"];
    this.bookingService.getRquestById(this.id).subscribe(
      (data:ResponseObj) => {
        this.request = data.payload;
      }
    )
  }

}
