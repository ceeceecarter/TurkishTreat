import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Store } from "../services/store.service";

@Component({
    selector: "checkout",
    templateUrl: "checkout.component.html",
    styleUrls: ['checkout.component.css']
})
export class Checkout {

    public errorMessage = "";

    constructor(public data: Store, private router: Router) {
    }

    onCheckout() {
        this.errorMessage = "";
        this.data.checkout()
            .subscribe(() => {
                this.router.navigate(["/"]);
            },
                error => {
                    this.errorMessage = `Failed to checkout: ${error}`;
                });
    }
}