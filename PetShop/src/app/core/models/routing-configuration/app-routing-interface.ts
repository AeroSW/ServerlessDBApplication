import { RouteType } from '../../enums/routing-configuration/route-type.enum';
import { Routes } from '@angular/router';

export interface AppRoutingInterface {
  name: string;
  path: string;
  type: RouteType;
  parent?: string;
  outlet?: string;
  redirectTo?: string;
  pathMatch?: string;
  component?: any;
  loadChildren?: any;
  children?: AppRoutingInterface[];
}

export function buildRoutes(routes: AppRoutingInterface[]): Routes {
  let r: Routes = [];
  routes.forEach((route) => {
    let temp = { path: route.path };
    if (route.type === RouteType.REDIRECTION) {
      temp["redirectTo"] = (route.redirectTo) ? route.redirectTo : null;
      temp["pathMatch"] = (route.pathMatch) ? route.pathMatch : null;
      r.push(temp);
    }
    else if (route.type === RouteType.CLASSIC) {
      temp["component"] = route.component;
      r.push(temp);
    }
    else if (route.type === RouteType.MAPPING || route.type === RouteType.MODULE) {
      temp["loadChildren"] = route.loadChildren;
      r.push(temp);
    }
    else if (route.type === RouteType.SUB) {
      let cList = routes.filter(rT => rT.name === route.parent);
      if (cList && cList.length > 0) {
        let c = cList[0].component;
        let rList = r.filter(rT => rT.component === c);
        if (rList && rList.length > 0) {
          temp['path'] = temp.path;
          temp['component'] = route.component;
          if (rList[0].children == null) rList[0].children = [];
          rList[0].children.push(temp);
        }
      }
    }
  });
  return r;
}
