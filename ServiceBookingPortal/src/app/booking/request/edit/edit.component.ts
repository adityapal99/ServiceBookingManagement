import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingService } from '../../booking.service';
import { UserRequest } from '../../user-request';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  id: number;
  request: UserRequest;
  form: FormGroup = new FormGroup({
    productid: new FormControl('', [Validators.required]),
    userid: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    problem: new FormControl('', Validators.required),
    status: new FormControl('', Validators.required),
    requestDate: new FormControl('', Validators.required)
  });;

  constructor(public bookingService: BookingService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
  }

  get f(){
    return this.form.controls;
  }

  submit(){
    console.log(this.form.value);
    this.bookingService.updateRequest(this.id, this.form.value).subscribe(res => {
         console.log('Request updated successfully!');
         this.router.navigateByUrl('request/index');
    })
  }

}
