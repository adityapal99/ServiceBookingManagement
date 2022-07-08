import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthorizationRequest } from '../auth.model';
import { AuthService } from '../auth.service';

interface RegisterRequestForm {
  name:FormControl<string | null>
  email: FormControl<string | null>,
  password: FormControl<string | null>
  mobile: FormControl<number | null>,
  regdate: FormControl<Date | null>
}

@Component({
  selector: 'app-login',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  currentdate=new Date();
  constructor(private authService: AuthService) { }

  public RegisterRequestForm: FormGroup<RegisterRequestForm> = new FormGroup<RegisterRequestForm>({
    name: new FormControl<string>(''),
    email: new FormControl<string>(''),
    password: new FormControl<string>(''),
    mobile:new FormControl<number>(0),
    regdate:new FormControl<Date>(this.currentdate)
  });


  ngOnInit(): void {
  }


  onRegisterSubmit() {
    const request: AuthorizationRequest = {
      email: this.RegisterRequestForm.value.email ?? "",
      password: this.RegisterRequestForm.value.password ?? ""
    }
    this.authService.login(request);
  }
}
