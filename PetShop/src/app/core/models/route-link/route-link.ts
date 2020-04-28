export interface RouteLink {
  path: string;
  name: string;
  children?: RouteLink[];
}
