import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingService } from '../../booking.service';
import { ResponseObj, UserReport, UserRequest } from '../../user-request';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  id: number;
  report: UserReport;
  form: FormGroup = new FormGroup({
    id:new FormControl('', Validators.required),
    reportDate: new FormControl('', Validators.required),
    serviceType: new FormControl('', Validators.required),
    actionTaken: new FormControl('', Validators.required),
    diagnosisDetails: new FormControl('', Validators.required),
    isPaid: new FormControl('', Validators.required),
    visitFees: new FormControl('', Validators.required),
    repairDetails: new FormControl('', Validators.required),
    serviceRequestId: new FormControl('', Validators.required)
  });;

  constructor(public bookingService: BookingService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['reportId'];
    this.bookingService.getReportById(this.id).subscribe((data: ResponseObj)=>{
      this.report = data.payload
    });
  }

  get f(){
    return this.form.controls;
  }

  submit(){
    console.log(this.form.value);
    this.bookingService.updateReport(this.id, this.form.value).subscribe(res => {
         console.log('Report updated successfully!');
         this.router.navigateByUrl('report/index');
    });
  }

}
