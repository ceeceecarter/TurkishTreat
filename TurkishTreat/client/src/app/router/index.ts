import { RouterModule } from '@angular/router';
import { Checkout } from "../pages/checkout.component";
import { ShopPage } from "../pages/shopPage.component";
import { LoginPage } from "../pages/loginPage.component";
import { AuthActivator } from "../services/authActivator.service";


const routes = [
    { path: "", component: ShopPage },
    { path: "checkout", component: Checkout, canActivate: [AuthActivator] },
    { path: "login", component: LoginPage },
    { path: "**", redirectTo: "/"} //fallback route - if none of the other route work this will redirect to main
];

const router = RouterModule.forRoot(routes,
    {
        useHash : false
    });

export default router;