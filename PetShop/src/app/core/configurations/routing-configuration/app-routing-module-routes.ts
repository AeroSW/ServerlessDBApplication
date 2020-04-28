import { AppRoutingInterface } from '../../models/routing-configuration/app-routing-interface';
import { RouteType } from '../../enums/routing-configuration/route-type.enum';
import { HomeComponent } from '../../../home/home.component';
import { FormRoutes } from './forms-routing-module-routes';

export const AppRoutes: AppRoutingInterface[] = [
  {
    name: 'Home Redirection',
    type: RouteType.REDIRECTION,
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    name: 'Home',
    type: RouteType.CLASSIC,
    path: 'home',
    component: HomeComponent
  },
  {
    name: "Forms",
    type: RouteType.MAPPING,
    path: 'form',
    loadChildren: () => import("src/app/forms/pet-forms.module").then(mod => mod.PetFormsModule),
    children: FormRoutes
  }
];
