import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { DogFormComponent } from './dog-form/dog-form.component';
import { CatFormComponent } from './cat-form/cat-form.component';
import { FormsComponent } from './forms.component';
import { FormsRoutingModule } from './forms-routing.module';
import { FormHomeComponent } from './form-home/form-home.component';
import { CoreModule } from 'src/app/core/core.module';
import { MaterialThemeModule } from 'src/app/core/modules/material-theme/material-theme.module';



@NgModule({
  declarations: [
    DogFormComponent,
    CatFormComponent,
    FormsComponent,
    FormHomeComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    CoreModule,
    MaterialThemeModule,
    FormsRoutingModule
  ],
  bootstrap: [FormsComponent]
})
export class PetFormsModule { }
