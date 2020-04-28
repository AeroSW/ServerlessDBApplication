import { NgModule, Component } from '@angular/core';
import { Route, Routes, RouterModule } from '@angular/router';
import { AppRoutes } from 'src/app/core/configurations/routing-configuration/app-routing-module-routes';
import { RouteType } from './core/enums/routing-configuration/route-type.enum';
import { buildRoutes } from './core/models/routing-configuration/app-routing-interface';

const routes: Routes = buildRoutes(AppRoutes);
console.log("App Routing:",routes);

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
