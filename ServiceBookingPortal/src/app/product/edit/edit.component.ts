import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../product';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { ResponseObject } from 'src/app/response';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  id: number;
  product: Product;
  form: FormGroup;

  constructor(
    public productService: ProductService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['productId'];
    this.productService.find(this.id).subscribe((data: ResponseObject)=>{
      this.product = data.payload;
    });

    this.form = new FormGroup({
      id: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required]),
      make: new FormControl('', Validators.required),
      model: new FormControl('', Validators.required),
      cost: new FormControl('', Validators.required),
      createdDate: new FormControl('', Validators.required),
    });
  }

  get f(){
    return this.form.controls;
  }

  submit(){
    console.log(this.form.value);
    this.productService.update(this.id, this.form.value).subscribe(res => {
      this.router.navigateByUrl('product/index');
      this.toastr.success('Product updated successfully!');
    })
  }

}
