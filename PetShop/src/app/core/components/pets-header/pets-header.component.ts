import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppRoutes } from '../../configurations/routing-configuration/app-routing-module-routes';
import { AppRoutingInterface } from '../../models/routing-configuration/app-routing-interface';
import { RouteLink } from '../../models/route-link/route-link';
import { RouteType } from '../../enums/routing-configuration/route-type.enum';

@Component({
  selector: 'pets-header',
  templateUrl: './pets-header.component.html',
  styleUrls: ['./pets-header.component.scss']
})
export class PetsHeaderComponent implements OnInit {

  routeLinks: RouteLink[];
  subNavbar: RouteLink[];

  constructor() { }

  ngOnInit(): void {
    this.routeLinks = [];
    this.buildRoutes(AppRoutes);
    console.log("--->", this.routeLinks);
  }
  private buildRoutes(routes: AppRoutingInterface[]): void {
    routes.forEach((route: AppRoutingInterface) => {
      if (route.type !== RouteType.REDIRECTION) {
        let routeLink: RouteLink;
        switch (route.type as RouteType) {
          case RouteType.CLASSIC:
            routeLink = { name: route.name, path: route.path };
            this.routeLinks.push(routeLink);
            break;
          case RouteType.MAPPING:
            routeLink = { name: route.name, path: route.path };
          case RouteType.MODULE:
            this.addChildren(routeLink, route.children);
            this.routeLinks.push(routeLink);
            break;
          default:
        };
      }
    });
  }
  private addChildren(parentLink: RouteLink, children: AppRoutingInterface[]) {
    if (parentLink.children == null) parentLink.children = [];
    children.forEach((route: AppRoutingInterface) => {
      console.log("Is err in this function?");
      if (route.type !== RouteType.REDIRECTION && route.path !== "") {
        let routeLink: RouteLink;
        switch (route.type as RouteType) {
          case RouteType.SUB:
          case RouteType.CLASSIC:
            routeLink = { name: route.name, path: parentLink.path + "/" + route.path };
            parentLink.children.push(routeLink);
            break;
          case RouteType.MAPPING:
            routeLink = { name: route.name, path: parentLink.path + "/" + route.path };
          case RouteType.MODULE:
            this.addChildren(routeLink, route.children);
            parentLink.children.push(routeLink);
            break;
          default:
        };
      }
    });
  }
}
