import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { extendedRoute } from 'src/app/app-routing.module';

class RouteLinks {
  path: string;
  name: string;
}

@Component({
  selector: 'pets-header',
  templateUrl: './pets-header.component.html',
  styleUrls: ['./pets-header.component.scss']
})
export class PetsHeaderComponent implements OnInit {

  routeLinks: RouteLinks[];

  constructor(private _route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.routeLinks = [];
    this.buildRoutes();
  }
  private buildRoutes(): void {
    console.log(this.router);
    this.router.config.forEach((route: extendedRoute) => {
      if (route.path !== "") {
        console.log(route);
        this.routeLinks.push({
          name: route.display,
          path: `/${route.path}`
        });
      }
    });
  }
}
