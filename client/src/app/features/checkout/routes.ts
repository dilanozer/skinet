import { Route } from "@angular/router";
import { CheckoutComponent } from "./checkout.component";
import { authGuard } from "../../core/guards/auth.guard";
import { CheckoutSuccessComponent } from "./checkout-success/checkout-success.component";
import { orderCompleteGuard } from "../../core/guards/order-complete.guard";

export const checkoutRoutes: Route[] = [
    { path: '', component: CheckoutComponent, canActivate: [authGuard] },
    { path: 'success', component: CheckoutSuccessComponent, canActivate: [authGuard, orderCompleteGuard] },
]