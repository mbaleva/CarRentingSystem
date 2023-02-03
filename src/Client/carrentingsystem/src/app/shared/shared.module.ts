import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';


@NgModule({
    declarations: [
    NavbarComponent
  ],
    imports: [
        RouterModule,
        CommonModule
    ],
    exports: [NavbarComponent]
})
export class SharedModule { }