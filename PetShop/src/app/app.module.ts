import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PetsHeaderComponent } from './core/components/pets-header/pets-header.component';
import { PetsFooterComponent } from './core/components/pets-footer/pets-footer.component';
import { HomeComponent } from './home/home.component';
import { PetFormsModule } from './forms/pet-forms.module';

@NgModule({
  declarations: [
    AppComponent,
    PetsHeaderComponent,
    PetsFooterComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    //PetFormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
