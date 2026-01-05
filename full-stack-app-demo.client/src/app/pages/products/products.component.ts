import { Component } from '@angular/core';
import { IProduct } from '../../models/product.model';
import { ProductsService } from '../../services/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',
  standalone: false
})
export class ProductsComponent {
  products: IProduct[] = [];
  displayedColumns = ['name', 'category', 'price', 'stock'];
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
