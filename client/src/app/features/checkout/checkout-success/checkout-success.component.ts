import { Component, inject } from '@angular/core';
import { SignalrService } from '../../../core/services/signalr.service';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AddressPipe } from '../../../shared/pipes/address.pipe';
import { CurrencyPipe, DatePipe, NgIf } from '@angular/common';
import { PaymentCardPipe } from '../../../shared/pipes/payment-card.pipe';
import { OrderService } from '../../../core/services/order.service';

@Component({
  selector: 'app-checkout-success',
  standalone: true,
  imports: [
    MatButton,
    RouterLink,
    MatProgressSpinnerModule,
    AddressPipe,
    CurrencyPipe,
    PaymentCardPipe,
    NgIf,
    DatePipe
  ],
  templateUrl: './checkout-success.component.html',
  styleUrl: './checkout-success.component.scss'
})
export class CheckoutSuccessComponent {
  signalrService = inject(SignalrService);
  private orderService = inject(OrderService);

  ngOnDestroy(): void {
    this.orderService.orderComplete = false;
    this.signalrService.orderSignal.set(null);
  }
}
