import { Component, OnInit } from '@angular/core';
import { AppRoutingInterface } from 'src/app/core/models/routing-configuration/app-routing-interface';
import { RouteLink } from '../../core/models/route-link/route-link';

@Component({
  selector: 'app-form-home',
  templateUrl: './form-home.component.html',
  styleUrls: ['./form-home.component.scss']
})
export class FormHomeComponent implements OnInit {

  availableForms: RouteLink[];
  private FormRoutes: any;

  constructor() { }

  ngOnInit(): void {
    this.FormRoutes = [{ name: "Dog", path: 'dog' }, { name: "Cat", path: 'cat' }];
    this.availableForms = [];
    this.buildButtons();
  }
  private buildButtons() {
    this.FormRoutes.forEach(route => {
      if (route.path !== "") {
        this.availableForms.push({ name: route.name, path: `${route.path}` });
      }
    });
  }
}
