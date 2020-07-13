import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { componentRoutes } from './components/component-routes';
import { EtherTransactionsComponent } from './components/ether-transactions/ether-transactions.component';

const routes: Routes = [
  { path: componentRoutes.root, component: EtherTransactionsComponent },

  // Default route
  { path: '**', redirectTo: componentRoutes.root }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
