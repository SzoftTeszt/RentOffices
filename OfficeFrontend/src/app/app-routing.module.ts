import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { OfficesListsComponent } from './offices-lists/offices-lists.component';

const routes: Routes = [
  {path:"", component:HomeComponent},
  {path:"offices", component:OfficesListsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
