import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DealersRoutingModule } from './dealers-routing.module';
import { CreateComponent } from './create/create.component';
import { ProfileComponent } from './profile/profile.component';
import { EditComponent } from './edit/edit.component';



@NgModule({
  declarations: [CreateComponent, ProfileComponent, EditComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DealersRoutingModule
  ],
  exports: [CreateComponent]
})
export class DealersModule { }
