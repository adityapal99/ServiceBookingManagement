import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BookingService } from '../../booking.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  
  form:FormGroup = new FormGroup({
    reportDate: new FormControl('', Validators.required),
    serviceType: new FormControl('', Validators.required),
    actionTaken: new FormControl('', Validators.required),
    diagnosisDetails: new FormControl('', Validators.required),
    isPaid: new FormControl('', Validators.required),
    visitFees: new FormControl('', Validators.required),
    repairDetails: new FormControl('', Validators.required),
    serviceRequestId: new FormControl('', Validators.required)
  });

  constructor(private bookingService:BookingService,private router: Router) { }

  ngOnInit(): void {
  }

  get f(){
    return this.form.controls;
  }
    
  submit(){
      console.log(this.form.value);
      this.bookingService.addReport(this.form.value).subscribe(res => {
      console.log('Report created successfully!');
      this.router.navigateByUrl('report/index');
    })
  }

}
