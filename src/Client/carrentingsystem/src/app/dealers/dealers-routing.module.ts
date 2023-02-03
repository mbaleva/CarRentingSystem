import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { ProfileComponent } from './profile/profile.component';

const routes = [
    {path: 'create', component: CreateComponent},
    {path: 'profile/:id', component: ProfileComponent},
    {path: 'edit/:id', component: EditComponent}
];


@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule]
})

export class DealersRoutingModule { }
