export class Product {
  Id: string | null;
  Name: string;
  Description: string;
  Price: number;
  Quantity: number;
  CategoryId: number;
  BrandId: number;

  constructor(
    id: string | null,
    name: string,
    description: string,
    price: number,
    quantity: number,
    categoryId: number,
    brandId: number
  ) {
    this.Id = id;
    this.Name = name;
    this.Description = description;
    this.Price = price;
    this.Quantity = quantity;
    this.CategoryId = categoryId;
    this.BrandId = brandId;
  }
}
