import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import {Product, Sample} from '../product';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  form:FormGroup;
  product:Sample;

  constructor(
    public productService: ProductService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl('', [Validators.required]),
      make: new FormControl('', Validators.required),
      model: new FormControl('', Validators.required),
      cost: new FormControl('', Validators.required),
      createdDate: new FormControl('', Validators.required)
    });
  }

  get f(){
    return this.form.controls;
  }

  submit(){
    this.productService.create(this.form.value).subscribe(res => {
      this.toastr.success('Post created successfully!');
      this.router.navigate(['product/index']);
    })
  }

}
