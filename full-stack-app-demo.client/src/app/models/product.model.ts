import { ICategory } from './category.model';

export interface IProduct {
  id: number;
  name: string;
  description?: string;
  price: number;
  stockQuantity: number;
  createdDate: string;
  isActive: boolean;
  categoryId: number;
  category?: ICategory | null;
}
