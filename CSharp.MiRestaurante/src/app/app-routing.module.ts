import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//Security
import { LoginComponent } from './Security/login/login.component';

import { HomeComponent } from './home/home.component';
//Catalogos
import { LocacionesComponent } from './Catalogos/locaciones/locaciones.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'catalogos-localciones', component: LocacionesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
