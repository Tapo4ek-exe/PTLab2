import { Product } from "./Product";

export type Purchase = {
    id: string;
    date: string;
    address: string;
    product: Product;
    usedPrice: number;
}