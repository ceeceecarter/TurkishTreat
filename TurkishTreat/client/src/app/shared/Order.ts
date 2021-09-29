export class OrderItem {
    id!: number;
    quantity!: number;
    unitPrice!: number;
    productId!: number;
    productCategory!: string;
    productSize!: string;
    productTitle!: string;
    productArtist!: string;
    productArtId!: number;
}

export class Order {
    orderId!: number;
    orderDate: Date = new Date();
    orderNumber: string = Math.random().toString(36).substr(2,5);
    items: OrderItem[] = [];

    get subtotal(): number {
        const result = this.items.reduce(
            (total, value) => { return total + (value.unitPrice * value.quantity) },
            0);
        return result;
    }
}