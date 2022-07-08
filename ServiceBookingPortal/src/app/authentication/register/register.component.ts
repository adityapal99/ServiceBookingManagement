import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { AuthorizationRequest } from '../auth.model';
import { AuthService } from '../auth.service';
import { HttpClientModule } from '@angular/common/http';
import { RegisterService } from './register.service';
import { user } from './register.model';
@Component({
  selector: 'app-login',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  users:user;
  constructor(private userData:RegisterService){}
  addUser(data:user)
  {
    console.warn(data)
    this.userData.addUser(data).subscribe((result)=>{
      console.warn()
    })
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}
