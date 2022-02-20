import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { ById } from './byid/byid.component';
import { CarsRoutingModule } from './cars-routing.module';
import { SharedModule } from '../shared/shared.module';
import { SearchComponent } from './search/search.component';
import { SortComponent } from './sort/sort.component';

@NgModule({
  declarations: [ListComponent, CreateComponent, EditComponent, ById, SearchComponent, SortComponent],
  imports: [
    CommonModule,
    SharedModule,
    CarsRoutingModule,
  ],
  exports: [ListComponent, CreateComponent, EditComponent, ById, SearchComponent]
})
export class CarsModule { }
