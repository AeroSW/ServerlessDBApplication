import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DogFormComponent } from './dog-form/dog-form.component';
import { CatFormComponent } from './cat-form/cat-form.component';
import { FormRoutes } from 'src/app/core/configurations/routing-configuration/forms-routing-module-routes';
import { buildRoutes } from 'src/app/core/models/routing-configuration/app-routing-interface';

const routes: Routes = buildRoutes(FormRoutes);
console.log("Forms Routing:",routes);

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class FormsRoutingModule { }
