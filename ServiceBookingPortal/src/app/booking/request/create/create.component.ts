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
    productid: new FormControl('', [Validators.required]),
    userid: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    problem: new FormControl('', Validators.required),
    status: new FormControl('', Validators.required),
    requestDate: new FormControl('', Validators.required)
  });

  constructor(private bookingService:BookingService,private router: Router) { }

  ngOnInit(): void {
  }

  get f(){
    return this.form.controls;
  }
    
  submit(){
      console.log(this.form.value);
      this.bookingService.addRequest(this.form.value).subscribe(res => {
      console.log('Request created successfully!');
      this.router.navigateByUrl('request/index');
    })
  }

}
