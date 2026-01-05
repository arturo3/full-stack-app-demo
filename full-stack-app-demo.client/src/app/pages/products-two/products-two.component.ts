import { Component } from '@angular/core';
import { IProduct } from '../../models/product.model';
import { ProductsService } from '../../services/products.service';

@Component({
  selector: 'app-products-two',
  templateUrl: './products-two.component.html',
  styleUrl: './products-two.component.css',
  standalone: false
})
export class ProductsTwoComponent {
  products: IProduct[] = [];
  hasLoaded = false;
  isLoading = false;
  hasError = false;

  constructor(private productsService: ProductsService) {}

  loadProducts(): void {
    this.hasLoaded = true;
    this.isLoading = true;
    this.hasError = false;

    this.productsService.getAll().subscribe({
      next: (products) => {
        this.products = products;
        this.isLoading = false;
      },
      error: () => {
        this.hasError = true;
        this.isLoading = false;
      }
    });
  }
}
