import { Component, OnInit } from '@angular/core';
import { BookingService } from '../../booking.service';
import { ResponseObj, UserReport } from '../../user-request';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  reports:UserReport[] = [];

  constructor(private bookingService:BookingService) { }

  ngOnInit(): void {
    this.bookingService.getReport().subscribe(
      (data:ResponseObj) => {
        this.reports = data.payload;
        console.log(this.reports);
      }
    );
  }


  deleteReport(id:number){
    this.bookingService.deleteReport(id).subscribe(res => {
         this.reports = this.reports.filter(item => item.id !== id);
         console.log('Report deleted successfully!');
    })
  }

}
