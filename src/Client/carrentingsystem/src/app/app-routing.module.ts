import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsComponent } from './contacts/contacts.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: HomeComponent
  },
  {
    path: 'contacts',
    pathMatch: 'full',
    component: ContactsComponent
  },
  { 
    path: 'auth',
    loadChildren: () => import('./authentication/authentication-routing.module')
                          .then(x => x.AuthenticationRoutingModule)
  },
  { 
    path: 'dealers',
    loadChildren: () => import('./dealers/dealers-routing.module')
                          .then(x => x.DealersRoutingModule)
  },
  { 
    path: 'cars',
    loadChildren: () => import('./cars/cars-routing.module')
                          .then(x => x.CarsRoutingModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
