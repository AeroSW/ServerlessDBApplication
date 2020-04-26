import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { DogFormComponent } from './dog-form/dog-form.component';
import { CatFormComponent } from './cat-form/cat-form.component';
import { FormsComponent } from './forms.component';
import { FormsRoutingModule } from './forms-routing.module';



@NgModule({
  declarations: [
    DogFormComponent,
    CatFormComponent,
    FormsComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    FormsRoutingModule
  ],
  bootstrap: [FormsComponent]
})
export class PetFormsModule { }
