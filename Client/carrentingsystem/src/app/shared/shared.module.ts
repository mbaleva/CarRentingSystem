import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';


@NgModule({
    imports: [
        RouterModule,
        CommonModule
    ],
    declarations: [NavbarComponent],
    exports: [NavbarComponent]
})
export class SharedModule { }