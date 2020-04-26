import { NgModule, Component } from '@angular/core';
import { Route, Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';

export class extendedRoute implements Route{
  display: string;
  path: string;
  redirectTo?: string;
  pathMatch?: string;
  component?: any;
  loadChildren?: any;
}

const routes: extendedRoute[] = [
  { path: "", redirectTo: "home", pathMatch: "full", display: "Home" },
  { component: HomeComponent, path: "home", display: "Home" },
  { path: "form", loadChildren: () => import("./forms/pet-forms.module").then(m => m.PetFormsModule), display: "Forms" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
