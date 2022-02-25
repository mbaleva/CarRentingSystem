import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AllComponent } from './all/all.component';
import { ByIdComponent } from './byid/byid.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
    {
        path: 'create', component: CreateComponent
    },
    {
        path: 'all', component: AllComponent
    },
    {
        path: 'byid/:id', component: ByIdComponent
    },
    {
        path: 'edit/:id', component: EditComponent
    },
];


@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})

export class CarsRoutingModule { }