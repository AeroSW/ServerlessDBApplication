import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DogFormComponent } from './dog-form/dog-form.component';
import { CatFormComponent } from './cat-form/cat-form.component';

const routes: Routes = [
  { component: DogFormComponent, path: 'dog' },
  { component: CatFormComponent, path: 'cat' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class FormsRoutingModule { }
