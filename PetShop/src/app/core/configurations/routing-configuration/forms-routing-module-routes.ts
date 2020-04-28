import { AppRoutingInterface } from '../../models/routing-configuration/app-routing-interface';
import { RouteType } from '../../enums/routing-configuration/route-type.enum';
import { FormHomeComponent } from '../../../forms/form-home/form-home.component';
import { DogFormComponent } from '../../../forms/dog-form/dog-form.component';
import { CatFormComponent } from '../../../forms/cat-form/cat-form.component';
import { FormsComponent } from '../../../forms/forms.component';

export const FormRoutes: AppRoutingInterface[] = [
  {
    name: 'Forms Home',
    type: RouteType.CLASSIC,
    path: '',
    component: FormsComponent
  },
  {
    name: 'Forms List',
    type: RouteType.SUB,
    path: '',
    parent: 'Forms Home',
    outlet: 'forms',
    component: FormHomeComponent
  },
  {
    name: 'Dog Form',
    type: RouteType.SUB,
    path: 'dog',
    parent: 'Forms Home',
    outlet: 'forms',
    component: DogFormComponent
  },
  {
    name: 'Cat Form',
    type: RouteType.SUB,
    path: 'cat',
    parent: 'Forms Home',
    outlet: 'forms',
    component: CatFormComponent
  }
];
