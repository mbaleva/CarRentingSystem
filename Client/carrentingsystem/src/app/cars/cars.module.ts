import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CarsRoutingModule } from './cars-routing.module';
import { CreateComponent } from './create/create.component';
import { AllComponent } from './all/all.component';
import { ByIdComponent } from './byid/byid.component';
import { EditComponent } from './edit/edit.component';
import { SearchComponent } from './search/search.component';



@NgModule({
  declarations: [CreateComponent, AllComponent, ByIdComponent, EditComponent, SearchComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CarsRoutingModule
  ],
  exports: [CreateComponent]
})
export class CarsModule { }