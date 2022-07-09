import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/users/user.model';
import { BookingService } from '../../booking.service';
import { ResponseObj, UserReport } from '../../user-request';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  id:number;
  report:UserReport;

  constructor(public bookingService: BookingService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params["reportId"];
    this.bookingService.getReportById(this.id).subscribe(
      (data:ResponseObj) => {
        this.report = data.payload;
      }
    )
  }

}
