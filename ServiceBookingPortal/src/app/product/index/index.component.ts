import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../product';
import { ResponseObject } from 'src/app/response';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  products: Product[];

  constructor(public productService: ProductService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.productService.getAll().subscribe((data: ResponseObject)=>{
      this.products = data.payload;
      console.log(this.products);
    })
  }

  deletePost(id:number){
    this.productService.delete(id).subscribe(res => {
         this.products = this.products.filter(item => item.id !== id);
         this.toastr.success('Product deleted successfully!');
    })
  }

}
